using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ks_Model
{
    [XmlRoot(ElementName = "Request")]
    public class ReqGetReservationDetail
    {
        public Head Head { get; set; }
        public ReqGetReservationDetail_Body Body { get; set; }
    }

    public class ReqGetReservationDetail_Body
    {
        public ReservationDetail ReservationDetail { get; set; }
    }
    public class ReservationDetail
    {
        public string confirmationID { get; set; }

        public string cardno { get; set; }

        public string mobile { get; set; }
    }
}
