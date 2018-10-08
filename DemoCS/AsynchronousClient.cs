using System;
using System.Collections.Generic;
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
        public delegate void SocketSendEventHandler(bool bResult);
        public delegate void SocketRecvEventHandler(string strRecv);
        public delegate void SocketDisconnectResultEventHandler(bool bDisconnect);

        public event SocketConnectResultEventHandler ConnectEvent;
        public event SocketSendEventHandler SendDataEvent;
        public event SocketRecvEventHandler RecvDataEvent;
        public event SocketDisconnectResultEventHandler DisconnectEvent;

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

        Thread ConnectMSPThread;
        Thread DisConnectMSPThread;
        Thread DataSendThread;
        Thread DataRecvThread;

        public bool GetConnectState()
        {
            return DemoCSclient.Connected;
        }
        private void StartConnect()
        {
            try
            {
                IPAddress ip = IPAddress.Parse(m_MspIp);
                IPEndPoint remoteIP = new IPEndPoint(ip, int.Parse(m_Port));
                DemoCSclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //connect to the remote endpoint.
                DemoCSclient.BeginConnect(remoteIP, new AsyncCallback(ConnectCallback), DemoCSclient);
                connectDone.WaitOne();
            }
            catch(Exception)
            {
                this.ConnectEvent(false);
            }
        }
        private void StartDisConnect()
        {
            try
            {
                DemoCSclient.BeginDisconnect(true, new AsyncCallback(DisConnectCallback), DemoCSclient);
                if(disConnectDone.WaitOne(5*1000))
                {
                    this.DisconnectEvent(true);
                }
                else
                {
                    this.DisconnectEvent(false);
                }
            }
            catch(Exception)
            {
                this.DisconnectEvent(false);
            }
        }

        private void StartSend()
        {
            try
            {
                byte[] byteData = Encoding.UTF8.GetBytes(sendString);
                DemoCSclient.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), DemoCSclient);
                sendDone.WaitOne();
            }
            catch(Exception ex)
            {
                this.SendDataEvent(false);
                Console.WriteLine(ex.ToString());
            }
            
        }

        private void StartRecv()
        {

         //   while (true)
          //  {
                try
                {
                    //Create the state object.
                    StateObject state = new StateObject();
                    state.workSocket = DemoCSclient;

                    //Begin receiving the data from the remote device.
                    DemoCSclient.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    receiveDone.WaitOne();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
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
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                //Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());
                //Signal that the connection has been made.
                connectDone.Set();
                this.ConnectEvent(true);
            }
            catch (Exception)
            {
                this.ConnectEvent(false);
            }
        }

        public void StartDisConnectClient()
        {
            try
            {
                DemoCSclient.Shutdown(SocketShutdown.Both);
                DisConnectMSPThread = new Thread(StartDisConnect);
                DisConnectMSPThread.IsBackground = true;
                DisConnectMSPThread.Start();   
            }
            catch (Exception)
            {
                this.DisconnectEvent(false);
            }
        }

        private void DisConnectCallback(IAsyncResult ar)
        {
            try
            {
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
            try
            {
                DataRecvThread = new Thread(StartRecv);
                DataRecvThread.IsBackground = true;
                DataRecvThread.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                //Retrieve the state object and the client socket.
                // from the asynchronous state object.
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                //Read data from the remote device.
                int bytesRead = client.EndReceive(ar);
                if (bytesRead > 0)
                {
                    // There might be more data,so store the data received so far.
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    Console.WriteLine("recv data: {0} from server", state.sb.ToString());
                    this.RecvDataEvent(state.sb.ToString());

                    //receiveDone.Set();
                    //Get the rest of the data
                    //client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    StartRecv();
                }
                else
                {
                    //All the data has arrived;put it in response.
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }
                    //Signal that all bytes have been received.
                    //this.RecvDataEvent(state.sb.ToString());
                    receiveDone.Set();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Send(byte[] byteData)
        {
            try
            {
                sendString = Encoding.UTF8.GetString(byteData);
                DataSendThread = new Thread(StartSend);
                DataSendThread.IsBackground = true;
                DataSendThread.Start();
            }
            catch(Exception)
            {
                this.SendDataEvent(false);
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
                Console.WriteLine("Send {0} bytes to server.", bytesSend);

                //Signal that all bytes have been send.
                this.SendDataEvent(true);
                sendDone.Set();
            }
            catch (Exception)
            {
                //Console.WriteLine(ex.ToString());
                this.SendDataEvent(false);
            }
        }
    }
}
