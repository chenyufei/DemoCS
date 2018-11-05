using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCS
{
    public class TestDevice
    {
        public string deveui { get; set; }
        public bool JoinStatus { get; set; }
        public bool UpdataStatus { get; set; }

        public string rssi { get; set; }
        public string snr { get; set; }
    }
}
