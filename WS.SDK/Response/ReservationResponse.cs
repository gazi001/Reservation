using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ks_Model.Respone
{
    [XmlRoot(ElementName = "Respone")]
    public class ReservationRespone
    {
        public ResponeHead Head { get; set; }
        public ResponeBody Body { get; set; }
    }
    public class ResponeHead
    {
        public string retcode { get; set; }
        public string retmsg { get; set; }
    }
    public class ResponeBody
    {
        public ReservationResponse ReservationResponse { get; set; }
    }
    public class ReservationResponse
    {
        public string responsecode { get; set; }
        public string responsemsg { get; set; }
        public Reservation Reservation { get; set; }
    }
    public class Reservation
    {
        public string numRooms { get; set; }
        public string credit { get; set; }
        public string confirmationID { get; set; }
        public string resNo { get; set; }
        public string profileID { get; set; }
        public string roomInventoryCode { get; set; }
        public TimeSpan TimeSpan { get; set; }
    }
    public class TimeSpan
    {
        [XmlAttribute]
        public string timeUnitType { get; set; }
        public string startTime { get; set; }
        public string numberOfTimeUnits { get; set; }
    }
    
}
