using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yuding.JsonRequest
{
    public class RoomDisplay
    {
        public string hotelid { get; set; }
        public string roomtype { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public Nullable<int> flag { get; set; }
        public string batch { get; set; }
        public Nullable<int> px { get; set; }
        public List<week> week { get; set; }
    }
    public class RateCodeDisplay
    {
        public int id { get; set; }
        public string hotelid { get; set; }
        public string xz_code { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public List<week> week { get; set; }
        public Nullable<int> flag { get; set; }
        public string batch { get; set; }
        public Nullable<int> px { get; set; }
    }
    public class week
    {
        public int index { get; set; }
        public string num { get; set; }
        public string  price { get; set; }
    }
}