using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RM.Common.DotNetHttp;
using yuding.JsonResult;
using yuding.Model;

namespace yuding.TEST
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            yudingEntities db = new yudingEntities();
            var time = DateTime.Parse("2017-11-27");
            var end =  DateTime.Parse("2017-11-04");
            var week = (int)time.DayOfWeek;
            var strWeek = week.ToString();
            
            var room = db.newroomtimes.Where(x => x.flag == 0 && x.timeflag == 0 && x.roomtype == "KSHZ20171124507" && x.startdate <= time && x.enddate >= time
).OrderByDescending(x=>x.px).ToList();
            var px = room.Max(x => x.px);
            room = room.Where(x => x.px == px && x.week == strWeek).ToList();
            
            
            //var hotelcode="LDSPT";
            //var list = db.order_t.Where(x => x.addtime >= time && x.addtime <= end && x.hotelcode == hotelcode&&x.state!="X"&&x.ispay==1).GroupBy(x => x.bosscard).ToList();
            //var result = new List<object>();
            //foreach (var item in list)
            //{
            //   Nullable<decimal> tmoney = 0;
            //    foreach (var item1 in item)
            //    {
            //        tmoney += item1.count * item1.rate;
            //    }
            //    result.Add(new
            //    {
            //        bosscard=item.FirstOrDefault().bosscard,
            //        contact_name=item.FirstOrDefault().contact_name,
            //        contact_mobile=item.FirstOrDefault().contact_mobile,
            //        num=item.GroupBy(x=>x.sessionid).Count(),
            //        roomnum=item.Sum(x=>x.count),
            //        tmoney = tmoney,
            //    });
            //}
            var json = JsonConvert.SerializeObject(room);
            Response.Write(json);
            //
            //var a = db.GetRateCodexzByRoomtypes.Where(x => x.everydate == time && x.roomtype == "KSHZ20170929709" && x.flag == 0&&x.WeekIndex==6).ToList();
            //Nullable<decimal> MinPrice = 0;
            //var xzprice = db.GetRateCodexzByRoomtypes.Where(x => x.everydate == time && x.roomtype == "LDSPT20171031484" && x.flag == 0 && x.WeekIndex == 6).OrderByDescending(x => x.price).ToList();
            //foreach (var item2 in xzprice)
            //{
            //    var xztime = db.xztimestart_t.Where(x => x.xz_code == item2.xz_code && x.week == "6" && x.startdate <= time && x.enddate >= time).OrderByDescending(x => x.px).FirstOrDefault();
            //    if (xztime != null&&item2.price!=0)
            //    {
            //        MinPrice = item2.price;
            //    }

            //}
            //var json = JsonConvert.SerializeObject(xzprice);
            //Response.Write(json);
            //var post = new
            //{
            //    hotelGroupCode = "LANDISON",
            //    clientChannel = "CRS",
            //    fromDate = DateTime.Now.AddDays(-1),
            //    toDate = DateTime.Now,
            //    otaChannel = "WEB"
            //};
            //IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            //timeFormat.DateTimeFormat = "yyyy-MM-ddTHH:mm:ss";
            //var json = JsonConvert.SerializeObject(post, Formatting.Indented, timeFormat);
            //var result = HttpHepler.SendPostJson("http://gds.ipms.cn/int/Crs/hotels", json);
            //var hotelInfo = JsonConvert.DeserializeObject<HotelListResult>(result);
            //Response.Write(json);
            //var groupcode = "YMJDJTG";
            //var post = new
            //{
            //    hotelGroupCode = groupcode,
            //    clientChannel = "CRS",
            //    fromDate = "2017-10-09T00:00:00",
            //    toDate = "2017-10-10T00:00:00",
            //    otaChannel = "WEB"
            //};
            //var json = JsonConvert.SerializeObject(post);

            //var result = HttpHepler.SendPostJson("http://gds.ipms.cn/int/Crs/hotels", json);
        }
    }
}