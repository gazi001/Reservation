using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ks_Model.Requst
{
    [XmlRoot(ElementName = "Request")]
    public class RateQueryRequest
    {
        public Head Head { get; set; }
        public RateQueryBody Body { get; set; }
    }

    public class RateQueryBody
    {
        public RatePlans RatePlans { get; set; }
    }
    public class RatePlans
    {
        public RatePlan RatePlan { get; set; }
    }
    public class RatePlan
    {
        [XmlAttribute]
        public string reservationActionType { get; set; }
        public string ratePlanCode { get; set; }
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
        [XmlAttribute]
        public string reservationActionType { get; set; }
        [XmlAttribute]
        public string rateBasisTimeUnitType { get; set; }
    }
}
