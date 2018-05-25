using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ks_Crs2._0_SDK.ResponseModel.pms;

namespace yuding.JsonResult
{
    public class RateXzByRmType
    {
        
        public string xzcode { get; set; }
        public Nullable<int> islock { get; set; }
        public Nullable<int> package { get; set; }
        public string payway { get; set; }
        public string pay { get; set; }
        public string xzname { get; set; }
        public string yuanjia { get; set; }
        public Nullable<int> ordersum { get; set; }
        public Nullable<int> ordernum { get; set; }
        public Nullable<int> pnum { get; set; }
        public Nullable<int> num { get; set; }
        public string baojia { get; set; }
        public Nullable<decimal> price { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public string startdate { get; set; }
        public int enddate { get; set; }
        public object rulesname { get; set; }
        public object rules { get; set; }
        public string activty { get; set; }
        public string formula { get; set; }
        public string roomtype { get; set; }
        public GetRoomAvailResult lvyunnum { get; set; }
        public string activtyStarttime { get; set; }
        public string activtyEndtime { get; set; }
        public int chaifen { get; set; }
        public string onsalecode { get; set; }
    }
}