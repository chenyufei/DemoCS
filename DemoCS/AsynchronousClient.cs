using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoCS
{
    public class AsynchronousClient
    {
        //定义的委托事件
        public delegate void SocketConnectResultEventHandler(bool bConnect);
        //public delegate void SocketSendEventHandler(bool bResult);
        public delegate void SocketRecvEventHandler(string strRecv);
        //public delegate void SocketDisconnectResultEventHandler(bool bDisconnect);
        public delegate void WriteLogEventHandler(string msg, Color col);

        public event SocketConnectResultEventHandler ConnectEvent;
        //public event SocketSendEventHandler SendDataEvent;
        public event SocketRecvEventHandler RecvDataEvent;
        //public event SocketDisconnectResultEventHandler DisconnectEvent;
        public event WriteLogEventHandler WriteLog;

        //ManualResetEvent instances signal completion.
        Socket DemoCSclient =null;
        
        private ManualResetEvent connectDone = new ManualResetEvent(false);
        private ManualResetEvent disConnectDone = new ManualResetEvent(false);
        private ManualResetEvent sendDone = new ManualResetEvent(false);
        private ManualResetEvent receiveDone = new ManualResetEvent(false);

        byte[] RecvBuffer = new byte[1024 * 1024];
        //The response from the remote device.
        private static string response = string.Empty;
        private string sendString = string.Empty;

        private string m_MspIp = string.Empty;
        private string m_Port = string.Empty;

        Thread ConnectMSPThread = null;
        Thread DisConnectMSPThread = null;
        Thread DataSendThread = null;
        Thread DataRecvThread = null;

        bool bExit = false;
        public bool GetConnectState()
        {
            return DemoCSclient.Connected;
        }
        private void StartConnect()
        {
            try
            {
                bExit = false;
                IPAddress ip = IPAddress.Parse(m_MspIp);
                IPEndPoint remoteIP = new IPEndPoint(ip, int.Parse(m_Port));
                DemoCSclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //connect to the remote endpoint.
                WriteLog("before beginConnect",Color.Black);
                DemoCSclient.BeginConnect(remoteIP, new AsyncCallback(ConnectCallback), DemoCSclient);
                if(connectDone.WaitOne(70*1000))
                {
                    WriteLog("Connect Success", Color.Black);
                    this.ConnectEvent(true);
                }
                else
                {
                    WriteLog("Connect Fail", Color.Black);
                    this.ConnectEvent(false);
                }
            }
            catch(Exception)
            {
                WriteLog("Connect Exception", Color.Black);
                this.ConnectEvent(false);
            }
        }
        private void StartDisConnect()
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                if (null != DemoCSclient && DemoCSclient.Connected)
                {
                    DemoCSclient.Shutdown(SocketShutdown.Both);
                    System.Threading.Thread.Sleep(10);
                    DemoCSclient.Close();
                    DemoCSclient = null;
                }

                //this.DisconnectEvent(true);
                /*
                DemoCSclient.BeginDisconnect(false, new AsyncCallback(DisConnectCallback), DemoCSclient);
                if(disConnectDone.WaitOne(5*1000))
                {
                    this.DisconnectEvent(true);
                }
                else
                {
                    this.DisconnectEvent(false);
                }
                */
            }
            catch(Exception)
            {
                //this.DisconnectEvent(false);
            }
        }

        private void StartSend()
        {
            if(null == DemoCSclient)
            {
                WriteLog("in StartSend this DemoCSClient is null so return", Color.Red);
                return;
            }
            try
            {
                int length = (sendString.Length + 1) & 0xFFFF;
                byte[] senddata = new byte[length + 5];

                int hValue = length >> 8;
                int lValue = length & 0xFF;
                byte[] arr = new byte[] { (byte)'\n', (byte)1, (byte)2, (byte)hValue, (byte)lValue };
                arr.CopyTo(senddata, 0);
                /*
                senddata[0] = UTF8Encoding.UTF8.GetBytes("\n")[0];
                senddata[1] = UTF8Encoding.UTF8.GetBytes("1")[0];
                senddata[2] = UTF8Encoding.UTF8.GetBytes("2")[0];
                */

                byte[] str = UTF8Encoding.UTF8.GetBytes(sendString);
                Buffer.BlockCopy(str, 0, senddata, 5, sendString.Length);

                senddata[sendString.Length + 5] = UTF8Encoding.UTF8.GetBytes("\0")[0];

                DemoCSclient.BeginSend(senddata, 0, senddata.Length, 0, new AsyncCallback(SendCallback), DemoCSclient);
                if(sendDone.WaitOne(60*1000))
                {
                    //WriteLog("in StartSend  send ok", Color.Red);
                }
                else
                {
                    WriteLog("in StartSend send timeout", Color.Red);
                }
            }
            catch(Exception ex)
            {
                //this.SendDataEvent(false);
                WriteLog("in StartSend exception" +ex.ToString(),Color.Red);
            }  
        }

        private void StartRecv()
        {
            if(null == DemoCSclient)
            {
                WriteLog(" in StartRecv,the DemoCSclient is null so return", Color.Black);
                return;
            }
            //   while (true)
            //  {
            //WriteLog(" in StartRecv", Color.Black);
            try
                {
                    if(!DemoCSclient.Connected)
                    {
                        return;
                    }
                    //Create the state object.
                    StateObject state = new StateObject();
                    state.workSocket = DemoCSclient;

                //WriteLog("beginReceive", Color.Black);
                //Begin receiving the data from the remote device.
                    DemoCSclient.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    receiveDone.WaitOne();
                }
                catch (Exception ex)
                {
                    WriteLog(ex.ToString() + " in catch StartRecv", Color.Black);
                }
           // }
        }
        public void StartConnectClient(string mspid, string strport)
        {
            //connect to a remote device
            try
            {              
                m_MspIp = mspid;
                m_Port = strport;

                ConnectMSPThread = new Thread(StartConnect);
                ConnectMSPThread.IsBackground = true;
                ConnectMSPThread.Start();
            }
            catch (Exception)
            {
                this.ConnectEvent(false);
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            WriteLog("In ConnectCallback", Color.Black);
            try
            {
                WriteLog("End Connect", Color.Black);
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                //Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());
                //Signal that the connection has been made.
                connectDone.Set();
            }
            catch (Exception ex)
            {
                WriteLog("exception:"+ ex.Message, Color.Black);
                this.ConnectEvent(false);
            }
        }

        public void StartDisConnectClient()
        {
            WriteLog("DisConnect", Color.Black);
            try
            {
                bExit = true;
                DataRecvThread.Abort();

                DisConnectMSPThread = new Thread(StartDisConnect);
                DisConnectMSPThread.IsBackground = true;
                DisConnectMSPThread.Start();   
            }
            catch (Exception)
            {
                //this.DisconnectEvent(false);
            }
        }

        private void DisConnectCallback(IAsyncResult ar)
        {
            try
            {
                DemoCSclient.Shutdown(SocketShutdown.Both);
                DemoCSclient.Disconnect(false);
                //Signal that the connection has been made.
                DemoCSclient.EndDisconnect(ar);
                
                DemoCSclient.Close();
                DemoCSclient.Dispose();
                
                DemoCSclient = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            disConnectDone.Set();
        }

        public void Receive()
        {
            WriteLog("start Receive", Color.Black);
            try
            {
                if(null != DataRecvThread)
                {
                    DataRecvThread.Abort();
                }
                DataRecvThread = new Thread(StartRecv);
                DataRecvThread.IsBackground = true;
                DataRecvThread.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                WriteLog(ex.ToString() + " in catch Receive", Color.Black);
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                //Retrieve the state object and the client socket.
                // from the asynchronous state object.
                if(null == ar)
                {
                    return;
                }

                StateObject state = (StateObject)ar.AsyncState;
                if(null == state)
                {
                    return;
                }
                Socket client = state.workSocket;
                if(null == client)
                {
                    return;
                }

                //Read data from the remote device.
                int bytesRead = client.EndReceive(ar);
                if (bytesRead > 0)
                {
                    // There might be more data,so store the data received so far.
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    //WriteLog(string.Format("ReceiveCallback recv data: {0} from server", state.sb.ToString()), Color.Black);
                    this.RecvDataEvent(state.sb.ToString());
              
                    //receiveDone.Set();
                    //Get the rest of the data
                    //client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);  
                }
                else
                {
                    WriteLog(string.Format("ReceiveCallback recv data empty from server"), Color.Black);
                }
                receiveDone.Set();
                if(!bExit)
                {
                    StartRecv();
                }          
            }
            catch (Exception ex)
            {
                WriteLog(string.Format(" ReceiveCallback recv data error: {0} from server", ex.ToString()), Color.Black);
            }
        }

        public void Send(string byteData)
        {
            try
            {
                sendString = byteData;
                DataSendThread = new Thread(StartSend);
                DataSendThread.IsBackground = true;
                DataSendThread.Start();
            }
            catch(Exception ex)
            {
                WriteLog("send exception in send:" + ex.Message, Color.Black);
                //this.SendDataEvent(false);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                //Retrive the socket from the state object.
                Socket client = (Socket)ar.AsyncState;
                //complete sned the data to the remote device.
                int bytesSend = client.EndSend(ar);
                //Console.WriteLine("Send {0} bytes to server.", bytesSend);

                //Signal that all bytes have been send.
                //this.SendDataEvent(true);   
                sendDone.Set();
            }
            catch (Exception ex)
            {
                WriteLog("send exception in send callback:" + ex.Message, Color.Black);
                //this.SendDataEvent(false);
            }
        }
    }
}
