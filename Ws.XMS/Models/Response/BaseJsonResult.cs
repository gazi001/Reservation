using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws.XMS.Models.Response
{
    public  class BaseJsonResult
    {
        public string code { get; set; }
        public string msg { get; set; }
        public bool success { get; set; }
        public string ts { get; set; }
    }
}
