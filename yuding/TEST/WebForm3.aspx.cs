using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using yuding.JsonRequest;
using yuding.JsonResult;
using yuding.Model;

namespace yuding.TEST
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var time = DateTime.Parse("2017-08-24");
            //var index = (int)time.DayOfWeek;
            //var a = index.ToString();
            //using (var db = new yudingEntities())
            //{
            //    var roomlist = new List<roomlist>()
            //    {
                    

            //    };
            //    var roomtime = db.newroomtimes.Where(x => x.startdate <= time && time <= x.enddate && x.flag == 0 && x.week == a).ToList();
            //    foreach (var item in roomtime)
            //    {
            //        var room = new roomlist()
            //        {
            //            roomname=item.descript,
            //            roomtype=item.roomtype,
            //            pricelist=new List<price>(),
            //        };

            //        var xz = db.GetRoomAndPrices.Where(x => x.roomtype == item.roomtype && x.WeekIndex == index).ToList();
            //        foreach (var item1 in xz)
            //        {
            //            var  x = 
                        
            //        }
                   
                    
            //    }
            using (var db = new yudingEntities())
            {

                var ratecode = "KSHZ20170828396";
                var a = db.GetRateCode_xz.Where(x => x.ratecode == ratecode).GroupBy(x => x.roomtype).ToList();
                var list = new List<info>();
                var time = DateTime.Parse("2017-08-24");
                foreach (var item in a)
                {
                    var b = new info()
                    {
                        xz = new List<xz>()
                    };
                    var r = item.FirstOrDefault().roomtype;
                    if (db.roomtimestart_t.Any(x => x.roomtype == r&& x.startdate == time && x.flag == 0))
                    {
                        b.roomtype = item.FirstOrDefault().roomtype;
                        b.roomname = item.FirstOrDefault().descript;
                    }
                    var xz1 = new List<xz>();
                    foreach (var item1 in item.GroupBy(x => x.xz_code))
                    {
                        var c = item1.FirstOrDefault().xz_code;
                        if (db.everydate_price_t.Any(x => x.xz_code == c&& x.everydate == time))
                        {
                            var s = new xz()
                            {
                                xz_code = item1.FirstOrDefault().xz_code.ToString(),
                                xz_name = item1.FirstOrDefault().xz_name.ToString(),
                                price=item1.FirstOrDefault().price,
                                package=item1.FirstOrDefault().package,
                            };
                            xz1.Add(s);
                        }
                    }
                    b.xz.AddRange(xz1);
                    list.Add(b);
                }
                var json = JsonConvert.SerializeObject(list);
                Response.Write(json);
            }
        }
    }
}