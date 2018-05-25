using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws.XMS.Models.Response
{
    public class getDailyRateJsonResult:BaseJsonResult
    {
        public List<DailyRate> results { get; set; }
    }
    public class DailyRate
    {
        /// <summary>
        /// 
        /// </summary>
        public string ratecode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ratecodetag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rmtype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string descript { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pkg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string descript2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string descript1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isvip { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ratecodeinfo { get; set; }
    }
}
