using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.ThirdPartINTFC.Model
{
    public class VehInfo
    {
        private string _clidlist;

        private string _cphlist;

        public string Clidlist { get => _clidlist; set => _clidlist = value; }
        public string Cphlist { get => _cphlist; set => _cphlist = value; }
    }
}
