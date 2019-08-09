using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Timers;
using System.IO;
using System.Security.Cryptography;

namespace DemoCS
{
    public partial class CLaa_IoT : Form
    {
        [DllImport("Aes128_CMac_Dll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl,EntryPoint = "challenge_identification")]
        static extern void challenge_identification(byte[] appkey, UInt64 appeui, UInt32 appnonce,ref byte challenge);

        private delegate void WriteTestLogDelegate(string msg, Color col);

        string m_strResultPath = @"C:\0283000116\InBound_Test";

        List<TestDevice> CheckDeviceList = new List<TestDevice>();
        //mbedtls_aes_context aes;

        string m_LogRecord = string.Empty;
        string m_JoinDeviceType = "join";
        string m_UpdataDeviceType = "updata";

        AsynchronousClient CsClient = null;

        System.Timers.Timer heartCheckTimer = new System.Timers.Timer();
        System.Timers.Timer m_WaitTimer = new System.Timers.Timer();
        bool m_bRunningTimer = false;

        int ToSendNumber = 0;
        int ToSendTime = -1;

        int iRecvHeartBeatNumber = 0;
        UInt64 CmdSeq = 1;
        enum CMD { SENDTO, BUFFQUE, CLASSQUE, BCAST, DEV_STATE };
        CMD selectcmd = CMD.SENDTO;

        enum SENDTYPE { NONE, TIMER, NUMBER, ALL };
        SENDTYPE sendType = SENDTYPE.NONE;

        CDevJoinObject devJoinData = new CDevJoinObject();
        CUpdataObject updataData = new CUpdataObject();

        bool m_bInitiativeDisconnect = false;
        private static object UsingInitiativeDisconnecLocker = new object();

        Thread sendThread;
        Thread ReConnectThread;

        bool m_bConnectMspSuccess = false;
        AutoResetEvent m_WaitConnectEvent = new AutoResetEvent(false);

        public CLaa_IoT()
        {
            InitializeComponent();
        }
        private bool checkEUI(string strEUI)
        {
            if (string.IsNullOrWhiteSpace(strEUI))
            {
                return false;
            }
            string tmpEUI = strEUI.Trim();
            if (16 != tmpEUI.Length)  //长度为固定的16位
            {
                return false;
            }

            string pattern = @"^[a-fA-F0-9]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(tmpEUI);
        }

        private bool checkIP(string strIP)
        {
            if (string.IsNullOrWhiteSpace(strIP))
            {
                return false;
            }
            string tmpIP = strIP.Trim();
            Regex regex = new Regex(@"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))");
            return regex.IsMatch(tmpIP);
        }

        private bool checkPort(string strPort)
        {
            Regex regex = new Regex("^[0-9]*[1-9][0-9]*$");
            return regex.IsMatch(strPort);
        }

        private bool dealRegister()
        {
            m_bConnectMspSuccess = false;

            if (this.Text_MSPIP.Text.Trim().Equals(""))
            {
                WriteTestLog("请输入MSP IP地址", Color.Red);
                return false;
            }
            if (!checkIP(this.Text_MSPIP.Text.Trim()))
            {
                WriteTestLog("请输入合法的 IP地址", Color.Red);
                return false;
            }

            if (this.Text_MSPPort.Text.Trim().Equals(""))
            {
                WriteTestLog("请输入MSP 端口", Color.Red);
                return false;
            }
            if (!checkPort(this.Text_MSPPort.Text.Trim()))
            {
                WriteTestLog("请输入合法的 端口，例如：30003", Color.Red);
                return false;
            }
            if (this.textBoxJoinEui.Text.Trim().Equals(""))
            {
                WriteTestLog("请输入JoinEUI", Color.Red);
                return false;
            }

            if (!checkEUI(this.textBoxJoinEui.Text.Trim()))
            {
                WriteTestLog("请输入合法的JoinEUI，长度为16位，0-f,例：2c26c50065650030", Color.Red);
                return false;
            }

            CreateSocketClient();
            m_WaitConnectEvent.Reset();
            CsClient.StartConnectClient(this.Text_MSPIP.Text, this.Text_MSPPort.Text);

            if (m_WaitConnectEvent.WaitOne(60 * 1000))
            {
                if (m_bConnectMspSuccess)
                {
                    WriteTestLog(string.Format("连接服务器：{0}，成功", this.Text_MSPIP.Text), Color.Green);
                    registerEUI(this.textBoxJoinEui.Text);
                    CsClient.Receive();
                }
                else
                {
                    CsClient.StartDisConnectClient();
                    WriteTestLog(string.Format("连接服务器：{0}，失败", this.Text_MSPIP.Text), Color.Red);
                    System.Threading.Thread.Sleep(5 * 1000);
                }
            }
            else
            {
                CsClient.StartDisConnectClient();
                WriteTestLog(string.Format("连接服务器：{0}，超时", this.Text_MSPIP.Text), Color.Green);
            }
            return m_bConnectMspSuccess;
        }
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            ClearLog();
            m_bInitiativeDisconnect = false;

            Thread ConnectThread = new Thread(ConnectServer);
            ConnectThread.IsBackground = true;
            ConnectThread.Start();   
        }

        private void ConnectServer()
        {
            dealRegister();
        }
        private void dealUnRegister()
        {
            string quitData = string.Format("{{\"cmd\":\"quit\",\"cmdseq\":{0}}}", getCmdSeq());
            sendDataToMSP(quitData);
        }
        private void buttonUnRegister_Click(object sender, EventArgs e)
        {
            ClearLog();
            m_bInitiativeDisconnect = true;

            StopHeartCheckTimer();

            this.buttonRegister.Enabled = true;
            this.buttonUnRegister.Enabled = false;
            dealUnRegister();
        }

        private void buttonSendToMSP_Click(object sender, EventArgs e)
        {
            sendType = SENDTYPE.NONE;
            if (this.checkBox_Time.Checked && this.checkBox_Number.Checked)
            {
                sendType = SENDTYPE.ALL;
                string strTimes = this.textBox_Time.Text;
                string strNumber = this.textBox_Number.Text;
                if (strTimes.Equals(""))
                {
                    WriteTestLog("请输入要发送时长，取消发送，取消选中即可", Color.Red);
                    return;
                }
                else if (strNumber.Equals(""))
                {
                    WriteTestLog("请输入要发送多少次，取消发送，取消选中即可", Color.Red);
                    return;
                }
                else
                {
                    WriteTestLog(string.Format("{0}分钟内，将发送{1}次", this.textBox_Time.Text, this.textBox_Number), Color.Red);
                }
            }
            else if (this.checkBox_Time.Checked)
            {
                string strTimes = this.textBox_Time.Text;
                if (strTimes.Equals(""))
                {
                    WriteTestLog("请输入要发送多长时间，0表示：一直发送，取消发送，取消选中即可", Color.Red);
                    return;
                }
            }
            else if (this.checkBox_Number.Checked)
            {
                string strNumber = this.textBox_Number.Text;
                if (strNumber.Equals(""))
                {
                    WriteTestLog("请输入要发送多少次，取消发送，取消选中即可", Color.Red);
                    return;
                }
            }

            if (sendType != SENDTYPE.NONE)
            {
                this.button_SendToMsp.Enabled = false;
                this.comboBox_Cmd.Enabled = false;
                this.button_CancelSend.Enabled = true;

                sendThread = new Thread(sendMsg);
                sendThread.IsBackground = true;
                sendThread.Start();
                return;
            }
            dealSelectCmd();
        }

        private void dealSelectCmd()
        {
            switch (selectcmd)
            {
                case CMD.SENDTO:
                    dealSendToCmd();
                    break;
                case CMD.BUFFQUE:
                    dealBufferQuery();
                    break;
                case CMD.BCAST:
                    dealbCast();
                    break;
                case CMD.DEV_STATE:
                    dealDevState();
                    break;
                case CMD.CLASSQUE:
                    dealClassQuery();
                    break;
                default:
                    WriteTestLog("命令不识别", Color.Red);
                    break;
            }
        }
        private void sendDataToMSP(string data)
        {
                try
                {
                    CsClient.Send(data);
                }
                catch (Exception ex)
                {
                    WriteTestLog("in sendDataToMSP"+ex.Message, Color.Red);
                }
        }
        private void sendMsg()
        {
            int iSleepTime = 60000;
            switch (sendType)
            {
                case SENDTYPE.ALL:
                    ToSendTime = int.Parse(this.textBox_Time.Text);
                    ToSendNumber = int.Parse(this.textBox_Number.Text);
                    if (ToSendTime / ToSendNumber != 0)
                    {
                        iSleepTime = (ToSendTime / ToSendNumber) * 60000;
                    }
                    break;
                case SENDTYPE.TIMER:
                    ToSendTime = int.Parse(this.textBox_Time.Text);
                    iSleepTime = 5 * 60000;
                    break;
                case SENDTYPE.NUMBER:
                    ToSendNumber = int.Parse(this.textBox_Number.Text);
                    iSleepTime = 2 * 60000;
                    break;
                default:
                    break;
            }

            while (true)
            {
                dealSelectCmd();
                Thread.Sleep(iSleepTime);
            }
        }
        private void dealRecvData(string recvData)
        {
            JObject obj;
            try
            {
                obj = JObject.Parse(recvData);
            }
            catch (Exception ex)
            {
                WriteTestLog(ex.Message, Color.Red);
                return;
            }
            try
            {
                string cmd = (null == obj["cmd"]?"":(string)obj["cmd"]);
                WriteTestLog(string.Format("{0}", cmd), Color.Green);
                if (cmd.Equals("join_ack"))
                {
                    WriteTestLog(recvData, Color.Green);
                    int Code = (null == obj["code"]?0:(int)obj["code"]);
                    if (Code.Equals(200) || Code.Equals(203))
                    {                       
                        this.buttonRegister.Enabled = false;
                        this.buttonUnRegister.Enabled = true;

                        heartCheckTimer.Enabled = false;
                        heartCheckTimer.Stop();
                        heartCheckTimer.Enabled = true;
                        heartCheckTimer.Start();
                    }
                }
                else if (cmd.Equals("updata"))
                {
                    StopHeartCheckTimer();
                    StartHeartCheckTimer();
                    updataData.deveui = (null == obj["deveui"]?"":(string)obj["deveui"]);
                    updataData.payload = (null == obj["payload"]?"":(string)obj["payload"]);
                    updataData.port = (null == obj["port"]?0:(int)obj["port"]);

                    int index = FindDeviceIndex(updataData.deveui);
                    lock (this)
                    {
                        if (-1 != index)
                        {
                            CheckDeviceList[index].JoinStatus = true;
                            CheckDeviceList[index].UpdataStatus = true;
                        }
                    }

                    string strDetail = (null == obj["detail"]?"":obj["detail"].ToString());
                    if(string.IsNullOrEmpty(strDetail))
                    {
                        WriteTestLog("no detail field,check the app config", Color.Red);
                        return;
                    }
                    JObject objDetail = null;
                    try
                    {
                        objDetail = JObject.Parse(strDetail);
                    }
                    catch
                    {
                        WriteTestLog("parse detai field fail", Color.Red);
                        return;
                    }
                    

                    string strApp = (null == objDetail["app"]?"":objDetail["app"].ToString());
                    if(string.IsNullOrEmpty(strApp))
                    {
                        WriteTestLog("no app field,check the app config", Color.Red);
                        return;
                    }
                    JObject objApp = null;
                    try
                    {
                        objApp = JObject.Parse(strApp);
                    }
                    catch
                    {
                        WriteTestLog("parse app field fail", Color.Red);
                        return;
                    }
                    updataData.seqno = (null == objApp["seqno"]?0:(int)objApp["seqno"]);

                    string strMotetx = (null == objApp["motetx"]?"":objApp["motetx"].ToString());
                    if(string.IsNullOrEmpty(strMotetx))
                    {
                        WriteTestLog("motetx field is empty", Color.Red);
                        return;
                    }
                    JObject objMotetx = null;
                    try
                    {
                        objMotetx = JObject.Parse(strMotetx);
                    }
                    catch
                    {
                        WriteTestLog("parse motetx field fail", Color.Red);
                        return;
                    }

                    updataData.freq = (null == objMotetx["freq"]?0:(float)objMotetx["freq"]);
                    updataData.datr = (null == objMotetx["datr"]?"":(string)objMotetx["datr"]);

                    WriteTestLog(string.Format("deveui:{0},port:{1},seqno:{2},freq:{3},datr:{4},payload:{5}", updataData.deveui, updataData.port, updataData.seqno, updataData.freq, updataData.datr, updataData.payload), Color.Green);
                    if (this.checkBox_saveData.Checked)
                    {
                        if(CheckDeviceList.Count !=0 && index==-1)
                        {
                            return;
                        }
                        string strGwRx = (null == objApp["gwrx"]?"":objApp["gwrx"].ToString());
                        if(string.IsNullOrEmpty(strGwRx))
                        {
                            WriteTestLog("gwrx field is empty", Color.Red);
                            return;
                        }
                        JArray jarGwRx = null;
                        try
                        {
                            jarGwRx = JArray.Parse(strGwRx);
                        }
                        catch
                        {
                            WriteTestLog("parse gwrx field fail", Color.Red);
                            return;
                        }
                        int iCount = jarGwRx.Count;
                        for (int i = 0; i < iCount; i++)
                        {
                            updataData.gweui = (null == jarGwRx[i]["gweui"]?"":(string)jarGwRx[i]["gweui"]);
                            updataData.rssic = (null == jarGwRx[i]["rssic"]?0:(float)jarGwRx[i]["rssic"]);
                            updataData.lsnr = (null == jarGwRx[i]["lsnr"]?0:(float)jarGwRx[i]["lsnr"]);
                            SaveResult(false);
                        }
                    }
                }
                else if (cmd.Equals("dev_join"))
                {
                    WriteTestLog(recvData, Color.Green);
                    StopHeartCheckTimer();
                    StartHeartCheckTimer();

                    if (this.checkBox_saveData.Checked)
                    {
                        devJoinData.deveui = (null == obj["deveui"] ? "" : (string)obj["deveui"]);

                        int index = FindDeviceIndex(devJoinData.deveui);
                        if (CheckDeviceList.Count != 0 && index == -1)
                        {
                            return;
                        }
                        
                        lock(this)
                        {
                            if (-1 != index)
                            {
                                CheckDeviceList[index].JoinStatus = true;
                            }
                        }

                        devJoinData.classType = (null == obj["class"]?"":(string)obj["class"]);
                        devJoinData.version = (null == obj["ver"]?"":(string)obj["ver"]);

                        SaveResult(true);
                    }
                }
                else if (cmd.Equals("quit_ack"))
                {
                    WriteTestLog(recvData, Color.Green);
                    CsClient.StartDisConnectClient();
                    //DisconnectFromMSP();
                }
                else if (cmd.Equals("sendto_ack"))
                {
                    WriteTestLog(recvData, Color.Green);
                }
                else if (cmd.Equals("buffque_ack"))
                {
                    WriteTestLog(recvData, Color.Green);
                }
                else if (cmd.Equals("classque_ack"))
                {
                    WriteTestLog(recvData, Color.Green);
                }
                else if (cmd.Equals("heartbeat"))
                {
                    WriteTestLog(recvData, Color.Green);
                    sendHeartbeatAck();
                }
                else if (cmd.Equals("forced_quit"))
                {
                    WriteTestLog(recvData, Color.Green);
                }
                else if (cmd.Equals("bcast_ack"))
                {
                    WriteTestLog(recvData, Color.Green);
                }
                else if (cmd.Equals("dev_state_ack"))
                {
                    WriteTestLog(recvData, Color.Green);
                }
                else if (cmd.Equals("dev_online"))
                {
                    WriteTestLog(recvData, Color.Green);
                }
                else if (cmd.Equals("dev_state_notify"))
                {
                    WriteTestLog(recvData, Color.Green);
                }
                else
                {
                    WriteTestLog(recvData, Color.Green);
                }
            }
            catch (Exception ex)
            {
                WriteTestLog(ex.Message, Color.Red);
                WriteTestLog(recvData, Color.Red);
            }
        }
        public string GetJsonValue(JEnumerable<JToken> jToken, string key)
        {
            System.Collections.IEnumerator enumerator = jToken.GetEnumerator();
            while (enumerator.MoveNext())
            {
                JToken jc = (JToken)enumerator.Current;
                if (jc is JObject || ((JProperty)jc).Value is JObject)
                {
                    return GetJsonValue(jc.Children(), key);
                }
                else
                {
                    if (((JProperty)jc).Name == key)
                    {
                        return ((JProperty)jc).Value.ToString();
                    }
                }
            }
            return null;
        }

        private void WriteTestLog(string msg, Color col)
        {
            try
            {
                if (this.richTextBox_Log.InvokeRequired)
                {
                    WriteTestLogDelegate d = new WriteTestLogDelegate(WriteTestLog);
                    this.richTextBox_Log.Invoke(d, new object[] { msg, col });
                }
                else
                {
                    this.richTextBox_Log.SelectionColor = col;
                    string log = msg;
                    this.richTextBox_Log.AppendText(string.Format("{0}:",System.DateTime.Now));
                    this.richTextBox_Log.AppendText(log);
                    this.richTextBox_Log.AppendText("\r\n");
                    this.richTextBox_Log.ScrollToCaret();
                    m_LogRecord = m_LogRecord + log + "\n";
                }
            }
            catch (Exception)
            {

            }

        }
        private void dealSendToCmd()
        {
            string strDeveui = this.textBoxDevEui.Text;
            if (!checkEUI(strDeveui))
            {
                WriteTestLog("请输入合法的DevEUI，长度为16位，0-f,例：004a77033900009b", Color.Red);
                return;
            }
            string strPort = this.textBox_DownPort.Text;
            /*
            if (string.IsNullOrEmpty(strPort))
            {
                WriteTestLog("请输入合法的下行端口，可在终端上行的数据中查看端口号例如：15", Color.Red);
                return;
            }
            */
            string confirm = this.checkBox_Confirm.Checked ? "true" : "false";
            string payload = this.textBox_Payload.Text;

            byte[] bytes = Encoding.Default.GetBytes(payload);
            if (this.radioButton_Hex.Checked)
            {
                bytes = strToToHexByte(payload);
            }

            payload = Convert.ToBase64String(bytes);

            string strSendtoContent = string.Format("{{\"cmd\":\"sendto\",\"cmdseq\":{0},\"deveui\":\"{1}\",\"confirm\":{2},\"payload\":\"{3}\",\"port\":{4}}}", getCmdSeq(), strDeveui, confirm, payload, int.Parse(strPort));
            sendDataToMSP(strSendtoContent);
        }
        private void dealBufferQuery()
        {
            string strDeveui = this.textBoxDevEui.Text;
            if (!checkEUI(strDeveui))
            {
                WriteTestLog("请输入合法的DevEUI，长度为16位，0-f,例：004a77033900009b", Color.Red);
                return;
            }
            string strBufferContent = string.Format("{{\"cmd\":\"buffque\",\"cmdseq\":{0},\"deveui\":\"{1}\"}}", getCmdSeq(), strDeveui);
            sendDataToMSP(strBufferContent);
        }
        private void dealClassQuery()
        {
            string strDeveui = this.textBoxDevEui.Text;
            if (!checkEUI(strDeveui))
            {
                WriteTestLog("请输入合法的DevEUI，长度为16位，0-f,例：004a77033900009b", Color.Red);
                return;
            }
            string strClassQueryContent = string.Format("{{\"cmd\":\"classque\",\"cmdseq\":{0},\"deveui\":\"{1}\"}}", getCmdSeq(), strDeveui);
            sendDataToMSP(strClassQueryContent);
        }

        private void dealbCast()
        {
            string strPort = this.textBox_DownPort.Text;
            if (!checkPort(strPort))
            {
                WriteTestLog("请输入合法的下行端口，可在终端上行的数据中查看端口号例如：15", Color.Red);
                return;
            }
            string strPayload = this.textBox_Payload.Text;

            byte[] bytes = Encoding.Default.GetBytes(strPayload);
            if (this.radioButton_Hex.Checked)
            {
                bytes = strToToHexByte(strPayload);
            }

            strPayload = Convert.ToBase64String(bytes);

            string strTimes = this.textBox_Number.Text;
            if (!checkPort(strTimes))
            {
                WriteTestLog("请输入合法的发送次数，例如：15", Color.Red);
                return;
            }
            string strbCastContent = string.Format("{{\"cmd\":\"bcast\",\"cmdseq\":{0},\"payload\":\"{1}\",\"port\":{2},\"times\":{3}}}", getCmdSeq(), strPayload, int.Parse(strPort), int.Parse(strTimes));
            sendDataToMSP(strbCastContent);
        }
        private void dealDevState()
        {
            string strDeveui = this.textBoxDevEui.Text;
            if (!checkEUI(strDeveui))
            {
                WriteTestLog("请输入合法的DevEUI，长度为16位，0-f,例：004a77033900009b", Color.Red);
                return;
            }
            string strDevStateContent = string.Format("{{\"cmd\":\"dev_state\",\"cmdseq\":{0},\"deveui\":\"{1}\"}}", getCmdSeq(), strDeveui);
            sendDataToMSP(strDevStateContent);
        }

        private void StopHeartCheckTimer()
        {
            iRecvHeartBeatNumber = 0;
            heartCheckTimer.Enabled = false;
            heartCheckTimer.Stop();
        }

        private void StartHeartCheckTimer()
        {
            heartCheckTimer.Enabled = true;
            heartCheckTimer.Start();
        }
        private void sendHeartbeatAck()
        {
            string strheartBeatAck = string.Format("{{\"cmd\":\"heartbeat_ack\"}}");
            iRecvHeartBeatNumber = 0;
            sendDataToMSP(strheartBeatAck);

            StopHeartCheckTimer();
            StartHeartCheckTimer();
        }

        void CreateSocketClient()
        {
            CsClient = new AsynchronousClient();
            CsClient.ConnectEvent += new AsynchronousClient.SocketConnectResultEventHandler(SocketConnectResult);
            //CsClient.DisconnectEvent += new AsynchronousClient.SocketDisconnectResultEventHandler(SocketDisconnectResult);
            //CsClient.SendDataEvent += new AsynchronousClient.SocketSendEventHandler(SocketSendResult);
            CsClient.RecvDataEvent += new AsynchronousClient.SocketRecvEventHandler(SocketRecvString);
            CsClient.WriteLog += new AsynchronousClient.WriteLogEventHandler(WriteTestLog);
        }
        private void CLaa_IoT_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            List<string> cmd = new List<string>();
            cmd.Add("下发数据");
            cmd.Add("查询指定终端下行消息队列状态");
            cmd.Add("查询终端类型");
            cmd.Add("广播数据到终端");
            cmd.Add("查询终端状态");

            this.comboBox_Cmd.DataSource = cmd;
            this.comboBox_Cmd.DisplayMember = "命令";
            this.comboBox_Cmd.SelectedIndex = 0;

            this.Text_MSPIP.Text = "139.129.216.128";
            this.Text_MSPPort.Text = "30003";
            this.textBoxJoinEui.Text = "2c26c50065650041";
           
            this.buttonRegister.Enabled = true;
            this.buttonUnRegister.Enabled = false;

            heartCheckTimer.Enabled = false;
            heartCheckTimer.Interval = 61000; //执行间隔时间,单位为毫秒; 这里实际间隔为1分钟  
            heartCheckTimer.Elapsed += new System.Timers.ElapsedEventHandler(HeartTimeOut);
            heartCheckTimer.Stop();

            m_WaitTimer.Interval = 200;
            m_WaitTimer.Elapsed += new System.Timers.ElapsedEventHandler(WaitTimeOut);
            m_WaitTimer.AutoReset = false;
            m_WaitTimer.Enabled = false;
            m_WaitTimer.Stop();

            this.button_ReportResult.Enabled = this.checkBox_saveData.Checked;
            this.textBox_input_deveui.Enabled = this.checkBox_saveData.Checked;
            this.richTextBox_ReportResult.Enabled = this.checkBox_saveData.Checked;

            CreateSocketClient();
        }
        private void StartWaitTimer(string strText)
        {
            if (string.IsNullOrEmpty(strText))
            {
                WriteTestLog("输入为空，请确认输入信息", Color.Red);
                return;
            }
            if (!m_bRunningTimer)
            {
                m_WaitTimer.Enabled = false;
                m_WaitTimer.Stop();

                m_WaitTimer.Enabled = true;
                m_WaitTimer.Start();

                m_bRunningTimer = true;
            }
        }

        private int FindDeviceIndex(string strdeveui)
        {
            lock(this)
            {
                int index = 0;
                int iCount = CheckDeviceList.Count;
                for (index = 0; index < iCount; index++)
                {
                    if (CheckDeviceList[index].deveui == strdeveui)
                    {
                        return index;
                    }
                }

                return -1;
            }
        }
        private void AddTestDevice(string strdeveui)
        {
            if(FindDeviceIndex(strdeveui) == -1)
            {
                TestDevice Device = new TestDevice();
                Device.deveui = strdeveui;
                Device.JoinStatus = false;
                Device.UpdataStatus = false;

                CheckDeviceList.Add(Device);
                this.label_Test_Count.Text = string.Format("{0}",CheckDeviceList.Count);
            }
        }
        private void WaitTimeOut(object source, ElapsedEventArgs e)
        {
            m_bRunningTimer = false;
            AddTestDevice(this.textBox_input_deveui.Text);
            this.textBox_input_deveui.Clear();
        }

        private void SocketConnectResult(bool bConnect)
        {
            m_bConnectMspSuccess = bConnect;
            m_WaitConnectEvent.Set();
        }
        private void SocketDisconnectResult(bool bDisconnect)
        {
            if(bDisconnect)
            {
                this.buttonRegister.Enabled = true;
                this.buttonUnRegister.Enabled = false;
                WriteTestLog(string.Format("Disconnect from {0},成功",this.Text_MSPIP.Text),Color.Red);
            }
            else
            {
                WriteTestLog(string.Format("Disconnect from {0} 失败",this.Text_MSPIP.Text), Color.Red);
            }
        }
        private void ReConnect()
        {
            for(int iIndex=0;iIndex < 5;iIndex++)
            {
                WriteTestLog(string.Format("正在重新连接服务器：{0}", this.Text_MSPIP.Text), Color.Red);
                
                if (dealRegister())
                {
                    break;
                }
                else
                {
                    WriteTestLog(string.Format("静等{0}秒后会再次尝试重连", 60 * (iIndex + 1)), Color.Red);
                    System.Threading.Thread.Sleep(1000*60*(iIndex+1));
                }
            }
        }
        private void SocketRecvString(string strResult)
        {
            byte[] RecvStr = UTF8Encoding.UTF8.GetBytes(strResult);
            if (RecvStr[0] =='\n' && RecvStr[1] == 1 && RecvStr[2] == 2)
            {
                string[] recv = strResult.Split(new string[] { "\n12" }, StringSplitOptions.RemoveEmptyEntries);
                string recvData = string.Empty;
                for (int i = 0; i < recv.Length; i++)
                {
                    byte[] str = Encoding.UTF8.GetBytes(recv[i]);
                    recvData = Encoding.UTF8.GetString(str, 5, str.Length - 5);

                    recvData = recvData.TrimEnd('\0');
                    WriteTestLog(recvData, Color.Red);
                    dealRecvData(recvData);
                }
            }
            else
            {
                WriteTestLog(strResult, Color.Red);
            }
        }
        private void HeartTimeOut(object source, ElapsedEventArgs e)
        {
            iRecvHeartBeatNumber++;
            string strnumber = string.Format("recv heartbeat number ={0}", iRecvHeartBeatNumber);
            WriteTestLog(strnumber, Color.Red);
            if (iRecvHeartBeatNumber >= 3)
            {
                iRecvHeartBeatNumber = 0;
                heartCheckTimer.Enabled = false;
                heartCheckTimer.Stop();

                CsClient.StartDisConnectClient();
                System.Threading.Thread.Sleep(1000 * 2);
                if (!m_bInitiativeDisconnect)
                {
                    ReConnectThread = new Thread(ReConnect);
                    ReConnectThread.IsBackground = true;
                    ReConnectThread.Start();
                }
            }
        }
        private void comboBox_Cmd_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectcmd = (CMD)this.comboBox_Cmd.SelectedIndex;
        }

        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2+1];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }
        private string GenerateChallenge(string appeui, UInt32 appnonce)
        {
            /*
            byte[] msg = new byte[16];
            byte[] AppeuiArray = strToToHexByte(appeui);
            byte[] AppNonceArray = BitConverter.GetBytes(appnonce);
            int index = 0;
            for(index=0;index < AppeuiArray.Length;index++)
            {
                msg[index] = AppeuiArray[index];
            }

            for (int i=AppNonceArray.Length-1;i>=0;i--)
            {
                msg[index] = AppNonceArray[i];
                index++;
            }

            RijndaelManaged rDel = new RijndaelManaged();
  
            byte[] keyArray = strToToHexByte("ffffffffffffffffffffffffffffffff");
            byte[] ivArray = new byte[16];// UTF8Encoding.UTF8.GetBytes("0000000000000000");
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.None;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
          
            byte[] resultArray = cTransform.TransformFinalBlock(msg, 0, msg.Length);
            

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            */
            //byte[] keyArray = strToToHexByte("ffffffffffffffffffffffffffffffff");
            byte[] keyArray = strToToHexByte(this.textBox_APPKEY.Text.Trim());
            UInt64 inAppeui = Convert.ToUInt64(appeui, 16);
            byte[] challenge = new byte[32 + 1];
            challenge_identification(keyArray, inAppeui, appnonce, ref challenge[0]);
            string strChallenge = System.Text.Encoding.Default.GetString(challenge).TrimEnd('\0');//.Remove(32);
            WriteTestLog(string.Format("根据{0}和{1} 计算出的挑战字为{2}",appeui,appnonce, strChallenge),Color.Green);
            return strChallenge;
        }

        private void registerEUI(string strEUI)
        {
            Random rd = new Random();
            UInt32 randInt = (UInt32)rd.Next();
            if(string.IsNullOrEmpty(this.textBox_appnonce.Text.Trim()))
            {
                randInt = (UInt32)rd.Next();
            }
            else
            {
                try
                {
                    randInt = UInt32.Parse(this.textBox_appnonce.Text.Trim());
                }
                catch
                {
                    randInt = (UInt32)rd.Next();
                }
            }
            WriteTestLog(string.Format("appnonce:{0}",randInt),Color.Red);
            this.textBox_appnonce.Text = string.Format("{0}", randInt);
            string JoinData = string.Format("{{\"cmd\":\"join\",\"cmdseq\":{0},\"appeui\":\"{1}\",\"appnonce\":{2},\"challenge\":\"{3}\"}}", getCmdSeq(), strEUI, randInt, GenerateChallenge(strEUI, randInt));
            sendDataToMSP(JoinData);
        }

        private UInt64 getCmdSeq()
        {
            return CmdSeq += CmdSeq + 2;
        }

        private void ClearLog()
        {
            this.richTextBox_Log.Clear();
        }
        private void loadDeveuiFile(string strFileName)
        {
            CheckDeviceList.Clear();
            try
            {
                if(!File.Exists(strFileName))
                {
                    WriteTestLog(strFileName + " 文件不存在，请扫码录入待测试的设备\n", Color.Red);
                    return;
                }
                string strcontent = File.ReadAllText(strFileName, Encoding.UTF8);
                string[] ContentLines= { } ;
                bool parseSuccess = true;
                if (strcontent.Contains("\r\n"))
                {
                    ContentLines = strcontent.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                }
                else if (strcontent.Contains("\n"))
                {
                    ContentLines = strcontent.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                }
                else if (strcontent.Contains("\r"))
                {
                    ContentLines = strcontent.Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    parseSuccess = false;
                    WriteTestLog("解析 " + strFileName + " 文件失败\n", Color.Red);
                }
                if(parseSuccess)
                {
                    int iCount = ContentLines.Count();
                    
                    for(int i=0;i<iCount;i++)
                    {
                        if (ContentLines[i].StartsWith("00"))
                        {
                            AddTestDevice(ContentLines[i].ToLower().Substring(2));
                        }
                        else
                        {
                            AddTestDevice(ContentLines[i]);                          
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteTestLog(ex.Message, Color.Red);
            }
        }
        private void SaveResult(bool Join)
        {
            DataTable m_dt = new DataTable();
            string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            m_dt.Columns.Add("测试时间", typeof(DateTime));
            m_dt.Columns.Add("deveui", typeof(String));

            string ResultPath;
            string DirPath = m_strResultPath;
            if (Join)
            {
                m_dt.Columns.Add("class", typeof(String));
                m_dt.Columns.Add("ver", typeof(String));

                m_dt.Rows.Add(DateTime.Now, devJoinData.deveui, devJoinData.classType, devJoinData.version);
                DirPath = DirPath + @"\" + @m_JoinDeviceType;
                ResultPath = DirPath + "\\dev_join_" + devJoinData.deveui + ".csv";
            }
            else
            {
                m_dt.Columns.Add("seqno", typeof(int));
                m_dt.Columns.Add("port", typeof(int));
                m_dt.Columns.Add("payload", typeof(String));
                m_dt.Columns.Add("freq", typeof(float));
                m_dt.Columns.Add("datr", typeof(String));
                m_dt.Columns.Add("gweui", typeof(String));
                m_dt.Columns.Add("rssic", typeof(float));
                m_dt.Columns.Add("lsnr", typeof(float));

                m_dt.Rows.Add(DateTime.Now, updataData.deveui, updataData.seqno, updataData.port, updataData.payload, updataData.freq, updataData.datr, updataData.gweui, updataData.rssic, updataData.lsnr);
                DirPath = DirPath + @"\" + @m_UpdataDeviceType;
                ResultPath = DirPath + "\\dev_updata_" + updataData.deveui + ".csv";
            }
            if (!System.IO.Directory.Exists(DirPath))
            {
                System.IO.Directory.CreateDirectory(DirPath);
            }
            SaveCSV(m_dt, ResultPath);
        }

        public void SaveCSV(DataTable dt, string fullPath)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(fullPath);
            string data;
            System.IO.FileStream fs;
            System.IO.StreamWriter sw;
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            if (!File.Exists(fullPath))
            {
                fs = new System.IO.FileStream(fullPath, System.IO.FileMode.Create,
               System.IO.FileAccess.Write);
                sw = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8);
                data = "";
                for (int i = 0; i < dt.Columns.Count; i++)//写入列名
                {
                    data += dt.Columns[i].ColumnName.ToString();
                    if (i < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
            }
            else
            {
                fs = new System.IO.FileStream(fullPath, System.IO.FileMode.Append,
                System.IO.FileAccess.Write);
                sw = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8);
                data = "";
            }
            for (int i = 0; i < dt.Rows.Count; i++) //写入各行数据
            {
                data = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string str = dt.Rows[i][j].ToString();
                    str = str.Replace("\"", "\"\"");//替换英文冒号 英文冒号需要换成两个冒号
                    if (str.Contains(',') || str.Contains('"')
                        || str.Contains('\r') || str.Contains('\n')) //含逗号 冒号 换行符的需要放到引号中
                    {
                        str = string.Format("\"{0}\"", str);
                    }

                    data += str;
                    if (j < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
            }
            sw.Close();
            fs.Close();
        }
        private void buttonCancelSend_Click(object sender, EventArgs e)
        {
            this.button_SendToMsp.Enabled = true;
            this.comboBox_Cmd.Enabled = true;

            try
            {
                if (sendThread.IsAlive)
                {
                    sendThread.Abort();
                }
            }
            catch (Exception)
            {

            }
        }

        //字符串转16进制字节数组
        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        private void button_ReportResult_Click(object sender, EventArgs e)
        {
            if (CheckDeviceList.Count == 0)
            {
                WriteReportLog(string.Format("没有录入测试设备", CheckDeviceList.Count), Color.Red);
                return;
            }
            lock (this)
            {
                WriteReportLog(string.Format("正在分析{0}个测试设备", CheckDeviceList.Count), Color.Green);
                List<string> UpdataList = new List<string>();
                List<string> JoinList = new List<string>();
                List<string> NoJoinList = new List<string>();

                for (int index =0;index < CheckDeviceList.Count;index++)
                {
                    if(CheckDeviceList[index].UpdataStatus)
                    {
                        UpdataList.Add(CheckDeviceList[index].deveui);
                    }
                    else if(CheckDeviceList[index].JoinStatus)
                    {
                        JoinList.Add(CheckDeviceList[index].deveui);
                    }
                    else
                    {
                        NoJoinList.Add(CheckDeviceList[index].deveui);
                    }
                }

                if(UpdataList.Count ==0)
                {
                    WriteReportLog(string.Format("没有设备上报数据"), Color.Red);
                }
                else
                {
                    WriteReportLog(string.Format("共有{0}个设备有数据上报，分别如下所示", UpdataList.Count), Color.Green);
                    string strOut = string.Empty;
                    for (int i=0;i<UpdataList.Count;i++)
                    {
                        strOut += string.Format("{0}\r\n",UpdataList[i]);
                    }
                    WriteReportLog(string.Format("{0}", strOut), Color.Green);
                }
                
                if(UpdataList.Count == CheckDeviceList.Count)
                {
                    return;
                }

                if(JoinList.Count == 0)
                {

                }
                else
                {
                    WriteReportLog(string.Format("\r\n共有{0}个设备有入网，分别如下所示", JoinList.Count), Color.Green);
                    string strOut = string.Empty;
                    for (int i = 0; i < JoinList.Count; i++)
                    {
                        strOut += string.Format("{0}\r\n", JoinList[i]);
                    }
                    WriteReportLog(string.Format("{0}", strOut), Color.DarkGreen);
                }

                if (UpdataList.Count + JoinList.Count == CheckDeviceList.Count)
                {
                    return;
                }

                if (NoJoinList.Count == 0)
                {

                }
                else
                {
                    WriteReportLog(string.Format("\r\n共有{0}个设备无入网，分别如下所示", NoJoinList.Count), Color.Green);
                    string strOut = string.Empty;
                    for (int i = 0; i < NoJoinList.Count; i++)
                    {
                        strOut += string.Format("{0}\r\n", NoJoinList[i]);
                    }
                    WriteReportLog(string.Format("{0}", strOut), Color.DarkGreen);
                }
            }
        }

        private void textBox_input_deveui_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox_input_deveui.Text.ToString()))
                return;
            StartWaitTimer(this.textBox_input_deveui.Text);
        }

        private void checkBox_saveData_CheckedChanged(object sender, EventArgs e)
        {
            this.button_ReportResult.Enabled = this.checkBox_saveData.Checked;
            this.textBox_input_deveui.Enabled = this.checkBox_saveData.Checked;
            this.richTextBox_ReportResult.Enabled = this.checkBox_saveData.Checked;
            if (this.checkBox_saveData.Checked)
            {
                loadDeveuiFile("deveui.txt");
            }
            else
            {
                this.richTextBox_ReportResult.Clear();
            }
        }

        private void WriteReportLog(string msg, Color col)
        {
            try
            {
                if (this.richTextBox_ReportResult.InvokeRequired)
                {
                    WriteTestLogDelegate d = new WriteTestLogDelegate(WriteReportLog);
                    this.richTextBox_Log.Invoke(d, new object[] { msg, col });
                }
                else
                {
                    this.richTextBox_ReportResult.SelectionColor = col;
                    this.richTextBox_ReportResult.AppendText(msg);
                    this.richTextBox_ReportResult.AppendText("\r\n");
                    this.richTextBox_ReportResult.ScrollToCaret();
                }
            }
            catch (Exception)
            {

            }

        }
    }
}
