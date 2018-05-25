using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using yuding.Model;
using System.Data.Entity.Migrations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace yuding.TEST
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var db = new yudingEntities())
            {
                var roomtype = "KSHZ2017082261";
                var time = DateTime.Parse("2017-09-01");

                var end = GetTimeStamp(DateTime.Parse("2017-09-01"));
                var xzlist = db.GetRateCodexzByRoomtypes.Where(x => x.roomtype == roomtype && x.flag == 0 && x.hotelid == "KSHZ"&&x.everydate==time).GroupBy(x => x.xz_code).ToList();
                var yuanjia = db.newroom_t.Where(x => x.roomtype == roomtype).FirstOrDefault().yuanjia;

                var list = new List<object>();
               
                foreach (var item in xzlist)
                {
                    Nullable<int> sum =0;
                    foreach (var item1  in xzlist)
                    {
                        sum += item1.FirstOrDefault().ordernum;

                    }
                    var xzcode = item.FirstOrDefault().xz_code;
                    var price = db.everydate_price_t.Where(x => x.xz_code == xzcode && x.everydate == time).FirstOrDefault();
                    if (price != null)
                    {
                        var num =price.num;
                        var ordernum = price.ordernum.ToString();
                        var miaosha = db.miaosha_t.Where(x => x.xz_code == xzcode).FirstOrDefault();
                        var starttime = "";
                        var endtime = "";
                        if (miaosha != null)
                        {
                            starttime = GetTimeStamp(DateTime.Parse(miaosha.starttime)).ToString();
                            endtime = GetTimeStamp(DateTime.Parse(miaosha.endtime)).ToString();
                        }

                        var ass = db.associateds.Where(x => x.xz_code == xzcode).FirstOrDefault();
                        var baojia = db.baojia_t.Where(x => x.xz_code == xzcode).FirstOrDefault();
                        var baojiaid = "";
                        if (baojia != null)
                        {
                            baojiaid = baojia.formulaid.ToString();
                        }
                        object rules = null;
                        object rulesname = null;
                        if (ass != null)
                        {
                            if (ass.rules == "early")
                            {
                                var id = int.Parse(ass.codeid);
                                rulesname = "early";
                                var early = db.earlies.Where(x => x.id == id).FirstOrDefault();
                                rules = early.code;
                            }
                            if (ass.rules == "live")
                            {
                                var id = int.Parse(ass.codeid);
                                rulesname = "live";
                                var live = db.lives.Where(x => x.id == id).FirstOrDefault();
                                rules = live.code;
                            }
                        }
                        var activty = db.activity_link.Where(x => x.xz_code == xzcode).FirstOrDefault();
                        var dis = "";
                        if (activty != null)
                        {
                            dis = db.Activities.Where(x => x.Code == activty.activitycode).FirstOrDefault().discount;
                        }
                        var formula = db.price_formulaid.Where(x => x.xz_code == xzcode).ToList();
                        List<System.String> listS = new List<System.String>();
                        string str = "";
                        if (formula.Count > 0)
                        {

                            foreach (var item1 in formula)
                            {
                                listS.Add(item1.formulaid + ":" + item1.categoryid);
                            }
                            listS.ToArray();
                        }
                        str = string.Join(",", listS);
                        var a = new
                        {
                            xzcode = xzcode,
                            islock = item.FirstOrDefault().islock,
                            package = item.FirstOrDefault().package,
                            payway = item.FirstOrDefault().payway,
                            pay = item.FirstOrDefault().pay,
                            xzname = item.FirstOrDefault().xz_name,
                            yuanjia = item.FirstOrDefault().yuanjia,
                            ordersum=sum,
                            ordernum=ordernum,
                            physicalnum=item.FirstOrDefault().pnum,
                            num = num,
                            baojia = baojiaid,
                            price = item.FirstOrDefault().price,
                            starttime = starttime,
                            endtime = endtime,
                            startdate = GetTimeStamp(time),
                            enddate = end,
                            rulesname = rulesname,
                            rules = rules,
                            activty = dis,
                            formula = str,
                        };
                        list.Add(a);
                    }
                }
                var json = JsonConvert.SerializeObject(list);
                Response.Write(json);
            }
        }

        private int GetTimeStamp(DateTime dt)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            int timeStamp = Convert.ToInt32((dt - dateStart).TotalSeconds);
            return timeStamp;
        }
    }
}