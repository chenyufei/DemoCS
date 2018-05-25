using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCS
{
    public class TestDevice
    {
        public string DeviceType { get; set; }
        public void AddDevice(string strDevEUI)
        {
            devList.Add(strDevEUI);
        }
        public bool CheckExist(string strDevEUI)
        {
            return devList.Exists((string s) => s == strDevEUI? true : false);
        }
        private List<string> devList = new List<string>();
    }
}
