using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws.XMS.Models.Response
{
    public class getRateCodeDes:BaseJsonResult
    {
        public List<RateCode> results { get; set; }
    }
    public class RateCode
    {
        /// <summary>
        /// 
        /// </summary>
        public string des1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ratecode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string des { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hotelid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string restype { get; set; }
    }
}
