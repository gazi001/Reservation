using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yuding.Model.ServiceModel
{
    public class specialtime
    {
        public DateTime time { get; set; }
    }
    public class CategroyData
    {
        //public string cid { get; set; }
        public string categroyid { get; set; }
        public string formulaid { get; set; }
        public int live { get; set; }
        public int weekend { get; set; }
        public DateTime starttime { get; set; }
        public DateTime endtime { get; set; }
        public specialtime[] specialtime { get; set; }
        public string hotelcode { get; set; }
    }
    public class SetgroyData
    {
        public string categroyid { get; set; }
        public string formulaid { get; set; }
        public int live { get; set; }
        public int weekend { get; set; }
        public DateTime starttime { get; set; }
        public DateTime endtime { get; set; }
        public DateTime[] specialtime { get; set; }
        public string hotelcode { get; set; }
    }

}