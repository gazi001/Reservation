using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ks_Model.Respone
{
    public class RateQueryResponse
    {
        public ResponseHead Head { get; set; }
        public ResponseBody Body { get; set; }
    }
    public class ResponseHead
    {
        public string retcode { get; set; }
        public string retmsg { get; set; }
    }
    public class ResponseBody
    {
        public List<record> record { get; set; }
    }
    public class record
    {
        public string roomInventoryCode { get; set; }
        public string descript { get; set; }
        public string roomrate { get; set; }
        public string package { get; set; }
        public string surnum { get; set; }
    }
}
