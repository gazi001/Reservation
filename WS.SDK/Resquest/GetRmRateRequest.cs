using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ks_Model.Requst
{
     [XmlRoot(ElementName = "Request")]
    public  class GetRmRateRequest
    {
        public Head Head { get; set; }
        public GetRmRateRequrstBody Body { get; set; }
    }

    public class GetRmRateRequrstBody
    {
        public RoomRate RoomRate { get; set; }
    }
    public class RoomRate
    {
        public string stay { get; set; }
        public string type { get; set; }
        public string day { get; set; }
        public string ratecode { get; set; }
    }

}
