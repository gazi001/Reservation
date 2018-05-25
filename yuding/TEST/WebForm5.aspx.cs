using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using yuding.JsonRequest;
using yuding.Model;

namespace yuding.TEST
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var time = DateTime.Parse("2017-09-08");
            var week = (int)time.DayOfWeek;
            using (var db = new yudingEntities())
            {

                var a = db.ratecode_t.Where(x => x.flag == 0 && x.scenario == 1 && x.hotelid == "KSHZ").FirstOrDefault().ratecode;


                var ratecodelist = db.newroom_t.Where(x => x.flag == 0 && x.hotelid == "KSHZ").ToList();
                var list = new List<info>();
                foreach (var item in ratecodelist)
                {
                    var b = new info()
                    {
                        xz = new List<xz>()
                    };

                    var room = db.newroomtimes.Where(x => x.flag == 0 && x.roomtype == item.roomtype && x.startdate <= time && x.enddate >= time).FirstOrDefault();
                    if (room != null)
                    {
                        var xzlist = db.rateroom_xz.Where(x => x.roomtype == item.roomtype && x.flag == 0&&x.ratecode==a).GroupBy(x => x.xz_code).ToList();
                        foreach (var item1 in xzlist)
                        {
                            var ratecode = item1.FirstOrDefault().ratecode;
                            if (ratecode == a)
                            {
                                //集合里添加一个房型
                               
                                var xzcode = item1.FirstOrDefault(x => x.WeekIndex == week).xz_code;
                                var c = db.xztimestart_t.Where(x => x.startdate == time && x.xz_code == xzcode).FirstOrDefault();
                                if (c != null)
                                {
                                    var isok = db.everydate_price_t.Where(x => x.everydate == time && x.xz_code == xzcode).FirstOrDefault();
                                    if (isok != null)
                                    {
                                        b.price = room.yuanjia;
                                        b.pic = room.img;
                                        b.roomtype = item.roomtype;
                                        b.roomname = item.descript;
                                        var xz1 = new List<xz>();
                                        //集合里添加一个房价码
                                        var s = new xz()
                                        {
                                            xz_code = item1.FirstOrDefault().xz_code.ToString(),
                                            xz_name = item1.FirstOrDefault().xz_name.ToString(),
                                            price = isok.price,
                                            package = isok.package,
                                        };
                                        xz1.Add(s);
                                        b.xz.AddRange(xz1);
                                    }
                                }
                            }
                        }
                        var min = db.GetRateCodexzByRoomtypes.Where(x => x.everydate == time && x.roomtype == item.roomtype).Min(x => x.price);
                        b.minprice = min;
                        list.Add(b);
                    }
                }
            }
        }
    }
}