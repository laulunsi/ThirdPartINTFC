using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.ThirdPartINTFC.Model
{
     public class VehPosition
    {
        private string _id;

        private string _sj;

        private string _jd;

        private string _wd;

        public string Id { get => _id; set => _id = value; }
        public string Sj { get => _sj; set => _sj = value; }
        public string Jd { get => _jd; set => _jd = value; }
        public string Wd { get => _wd; set => _wd = value; }
    }
}
