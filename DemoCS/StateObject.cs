using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DemoCS
{
    public class StateObject
    {
        //Client Socket.
        public Socket workSocket = null;
        //size of recvive buffer.
        public const int BufferSize = 1024 * 10;

        //recvive buffer.
        public byte[] buffer = new byte[BufferSize];

        //received data string.
        public StringBuilder sb = new StringBuilder();
    }
}
