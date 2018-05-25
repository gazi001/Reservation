using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Ks_Model
{
    public class RespReservationDetail
    {
        public RespReservationDetail_Head Head { get; set; }

        public RespReservationDetail_Body Body { get; set; }
    }

    public class RespReservationDetail_Head
    {
        public string retcode { get; set; }

        public string retmsg { get; set; }
    }

    public class RespReservationDetail_Body
    {
        public ReservationResponses ReservationResponses { get; set; }
    }
    public class ReservationResponses
    {
        public ReservationResponse ReservationResponse { get; set; }
    }
    public class ReservationResponse
    {
        public string responsecode { get; set; }
        public string responsemsg { get; set; }
        public Select_Reservation Reservation { get; set; }
    }
    public class Select_Reservation
    {
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string Ref { get; set; }
        public string numRooms { get; set; }
        public string credit { get; set; }
        public string confirmationID { get; set; }
        public string mfLegNumbers { get; set; }
        public string roomInventoryCode { get; set; }
        public string roomInventoryCodeDescript { get; set; }
        public string roomNo { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public Rates Rates { get; set; }
    }
    public class TimeSpan
    {
        [XmlAttribute]
        public string timeUnitType { get; set; }
        public string startTime { get; set; }
        public string numberOfTimeUnits { get; set; }
    }
    public class Rates
    {
        public Rate Rate { get; set; }
    }
    public class Rate
    {
        public string rateRPH { get; set; }
        public string rateBasisUnits { get; set; }
        public string mfAdults { get; set; }
        public string mfChildren { get; set; }
        public Amount Amount { get; set; }
    }
    public class Amount
    {
        public string valueNum { get; set; }
        public string valueTotal { get; set; }
    }
}
