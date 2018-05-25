using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yuding.Model.ServiceModel
{
  
    public class HotelMappingResult
    {
        public string code { get; set; }
        public string msg { get; set; }
        public HotelMapping[] data { get; set; }

    }
    public class HotelMapping
    {
        public int id { get; set; }
        public string hotelCode { get; set; }
        public string hotelCodesMapping { get; set; }
        public string roomTypeCodes { get; set; }
        public string roomRateCodes { get; set; }
        public string roomTypeName { get; set; }
        public string roomRateCodeName { get; set; }
        public string hotelName { get; set; }
        public string scene { get; set; }
    }
}