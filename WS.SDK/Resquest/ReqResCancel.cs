using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ks_Model
{
       [XmlRoot(ElementName = "Request")]
    public class ReqResCancel
    {
        public Head Head { get; set; }
        public ReqResCancel_Body Body { get; set; }
    }
    public class ReqResCancel_Body
    {
        public ReservationCancel ReservationCancel { get; set; }
    }
    public class ReservationCancel
    {
        public string confirmationID { get; set; }
    }
}
