using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ks_Model.Requst.Reservation
{
       [XmlRoot(ElementName = "Request")]
    public class ReservationRequest
    {
        public Head Head { get; set; }
        public ReservationRequestBody Body { get; set; }
    }
    public class ReservationRequestBody
    {
        public Reservation Reservation { get; set; }
    }
    public class Reservation
    {
        [XmlAttribute]
        public string mfShareAction { get; set; }
        [XmlAttribute]
        public string mfReservationAction { get; set; }
        public string reservationOriginatorCode { get; set; }
        public string protocol { get; set; }
        public ResComments ResComments { get; set; }
        public ResProfiles ResProfiles { get; set; }
        public RoomStays RoomStays { get; set; }
        public string resCommentRPHs { get; set; }
        public string resProfileRPHs { get; set; }
        public string mfsaleid { get; set; }

    }
    public class ResComments
    {
        public ResComment ResComment { get; set; }
    }
    public class ResComment
    {
        [XmlAttribute]
        public string reservationActionType { get; set; }
        public string comment { get; set; }
    }
    public class ResProfiles
    {
        public ResProfile ResProfile { get; set; }
    }
    public class ResProfile
    {
        public Profile Profile { get; set; }
    }
    public class Profile
    {
        [XmlAttribute]
        public string profileType { get; set; }
        [XmlAttribute]
        public string gender { get; set; }
        public IndividualName IndividualName { get; set; }
        public string primaryLanguageID { get; set; }
    }
    public class IndividualName
    {
        public string  nameTitle{get;set;}
        public string nameFirst { get; set; }
        public string nameSur { get; set; }
    }
    public class nameTitle
    {
        public string nameFirst { get; set; }
        public string nameSur { get; set; }
    } 
    public class RoomStays
    {
        public RoomStay RoomStay { get; set; }
    }
    public class TimeSpan
    {
        [XmlAttribute]
        public string timeUnitType { get; set; }
        public string startTime { get; set; }
        public string numberOfTimeUnits { get; set; }
        public string arrtime { get; set; }
    }
    public class RoomStay
    {
        [XmlAttribute]
        public string mfShareAction { get; set; }
        [XmlAttribute]
        public string mfReservationAction { get; set; }
        [XmlAttribute]
        public string reservationActionType { get; set; }
        [XmlAttribute]
        public string reservationStatusType { get; set; }
        public string roomStayRPH { get; set; }
        public string roomInventoryCode { get; set; }
        public string roomNo { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public GuestCounts GuestCounts { get; set; }
        public RatePlans_ReservationRequest RatePlans { get; set; }
        public string mfchannelCode { get; set; }

    }

    public class TimeSpan_ReservationRequest
    {
        [XmlAttribute]
        public string timeUnitType { get; set; }
        public string startTime { get; set; }
        public string numberOfTimeUnits { get; set; }
        public string arrtime { get; set; }
    }
    
    public class GuestCounts
    {
        [XmlElement(ElementName="GuestCount")]
        public List<GuestCount> GuestCount { get; set; }
    }
    public class GuestCount
    {
        public string ageQualifyingCode { get; set; }
        public string mfCount { get; set; }
    }
    public class RatePlans_ReservationRequest
    {
        public RatePlan_ReservationRequest RatePlan { get; set; }
    }
    public class RatePlan_ReservationRequest
    {
        [XmlAttribute]
        public string reservationActionType { get; set; }
        public string ratePlanCode { get; set; }
        public string mfsourceCode { get; set; }
        public string mfMarketCode { get; set; }
        public string numRooms { get; set; }
    }
}
