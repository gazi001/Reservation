using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ks_Model.Respone
{
    /// <summary>
    /// ceshi
    /// </summary>
    //[XmlRoot(ElementName = "Response")]
    public class GetRmRateResponse
    {
        public GetRmRateResponseHead Head { get; set; }
        public GetRmRateResponseBody Body { get; set; }
    }

  
    public class GetRmRateResponseHead
    {
        public string transcode { get; set; }
        public string systype { get; set; }
        public string retcode { get; set; }
        public string retmsg { get; set; }
    }
    
    public class GetRmRateResponseBody
    {
        public ResponseBodyRmrate ResponseBodyRmrate { get; set; }
    }
   
    public class ResponseBodyRmrate
    {
        [XmlElement(ElementName = "Rmrate")]
        public List<Rmrate> Rmrate { get; set; }
        public string responsecode { get; set; }
        public string responsemsg { get; set; }
    }
   
    public class Rmrate
    {
        public string date { get; set; }
        public string rate { get; set; }
        public string ratecode { get; set; }
        public string rmtype { get; set; }
    }
}
