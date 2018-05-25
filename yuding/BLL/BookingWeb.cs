using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Ks_Crs2._0_SDK.AdvancedAPIs;
using Ks_Crs2._0_SDK.ResponseModel.pms;
using Newtonsoft.Json;
using RM.Common.DotNetCode;
using RM.Common.DotNetHttp;
using RM.Common.DotNetJson;
using ServiceStack.Redis;
using yuding.DAL;
using yuding.JsonRequest;
using yuding.JsonResult;
using yuding.Model;

namespace yuding.BLL
{
    public class BookingWeb
    {
        yudingEntities db = null;
        public static List<RateXzByRmType> GetRateCodexzByRoomtype(yudingEntities db, string starttimeStr, string endtimeStr, string roomtype, string hotelcode, int type)
        {
            db = new yudingEntities();
            var time = DateTime.Parse(starttimeStr);
            var week = (int)time.DayOfWeek;
            var end = DateTimeHelper.GetTimeStamp(DateTime.Parse(endtimeStr));
            var XzList = db.GetRcListByRoomType(roomtype, hotelcode, DateTime.Parse(starttimeStr)).ToList().GroupBy(x => x.xz_code).OrderByDescending(x => x.FirstOrDefault().px);
            var yuanjia = db.newroom_t.Where(x => x.roomtype == roomtype).FirstOrDefault();
            var list = new List<RateXzByRmType>();
            var d = db.ratecode_t.Where(x => x.hotelid == hotelcode && x.scenario == 1 && x.flag == 0 && x.type == type).FirstOrDefault().ratecode;

            string URL = Config.WxAPIUrl+"/API/GetWX.ashx?action=GetGZHxx";
            var postData = "hotelcode=" + hotelcode;
            var result = HttpHepler.SendPost(URL, postData);
            HotelInfoJson info = JsonConvert.DeserializeObject<HotelInfoJson>(result);
            var data = info.data[0];
            GetRoomAvailResult room=null;
            if (info.data[0].LvyunHotelgroupId != "" && info.data[0].LvyunHotelgroupId != null)
            {
                //var data = info.data[0];
                room = PmsAPI.GetRoomAvail(data.orderUrl, data.LvyunHotelgroupId, data.LvyunHotelId, time.ToString("yyyy-MM-dd"), time.ToString("yyyy-MM-dd"), yuanjia.pms);
             
            }
            foreach (var item in XzList)
            {
                var xzcode = item.FirstOrDefault().xz_code;
                var rate = db.rateroom_xz.Where(x => x.xz_code == xzcode).FirstOrDefault();
                var c = db.xztimestart_t.Where(x => x.startdate <= time && x.enddate >= time && x.xz_code == xzcode && x.flag == 0 && x.week == week.ToString()).FirstOrDefault();
                //c.starttime
               // 
                if (rate.ratecode == d && item.FirstOrDefault().price != 0)
                {
                    
                    if (c != null)
                    {
                       
                        var m1 = c.starttime == "" ? "00:00" : c.starttime;
                        var m2 = c.endtime == "" ? "23:59" : c.endtime;
                        var t1 = DateTime.Parse(m1);
                        var t2 = DateTime.Parse(m2);
                        if (DateTime.Now >= t1 && DateTime.Now <= t2)
                        {
                            //if(DateTime.Now>=DateTime.Parse(c.starttime)&&DateTime.Now<=DateTime.Parse(c.endtime))
                            Nullable<int> sum = 0;
                            foreach (var item1 in XzList)
                            {
                                sum += item1.FirstOrDefault().ordernum;
                            }
                            var price = db.everydate_price_t.Where(x => x.xz_code == xzcode && x.everydate == time).FirstOrDefault();
                            if (price != null)
                            {
                                var num = price.num;
                                var ordernum = price.ordernum;
                                var miaosha = db.miaosha_t.Where(x => x.xz_code == xzcode).FirstOrDefault();
                                var starttime = "";
                                var endtime = "";
                                if (miaosha != null)
                                {
                                    starttime = DateTimeHelper.GetTimeStamp(DateTime.Parse(miaosha.starttime)).ToString();
                                    endtime = DateTimeHelper.GetTimeStamp(DateTime.Parse(miaosha.endtime)).ToString();
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
                                var aStarttime = "";
                                var aEndtime = "";
                                if (activty != null)
                                {
                                    var act = db.Activities.Where(x => x.Code == activty.activitycode).FirstOrDefault();
                                    if (act.starttime != null && act.endtime != null)
                                    {
                                        aStarttime = DateTimeHelper.GetTimeStamp(act.starttime).ToString();
                                        aEndtime = DateTimeHelper.GetTimeStamp(act.endtime).ToString();
                                    }
                                    dis = db.Activities.Where(x => x.Code == activty.activitycode).FirstOrDefault().discount;
                                }
                                var formula = db.price_formulaid.Where(x => x.xz_code == xzcode).ToList();
                                List<System.String> listS = new List<System.String>();
                                string str = "";
                                if (formula.Count > 0)
                                {
                                    foreach (var item1 in formula)
                                    {
                                        listS.Add(item1.formulaid + ":" + item1.categoryid + ":" + item1.type);
                                    }
                                    listS.ToArray();
                                }
                                str = string.Join(",", listS);
                                Nullable<int> physicalnum = item.FirstOrDefault().pnum;
                                var a = new RateXzByRmType
                                {
                                    onsalecode = rate.onsalecode,
                                    chaifen = item.FirstOrDefault().chaifen.Value,
                                    xzcode = xzcode,
                                    islock = item.FirstOrDefault().islock,
                                    package = item.FirstOrDefault().package,
                                    payway = item.FirstOrDefault().payway,
                                    pay = item.FirstOrDefault().pay.ToString(),
                                    xzname = item.FirstOrDefault().xz_name,
                                    yuanjia = item.FirstOrDefault().yuanjia,
                                    ordersum = sum,
                                    ordernum = ordernum,
                                    pnum = physicalnum,
                                    num = num,
                                    baojia = baojiaid,
                                    price = item.FirstOrDefault().price,
                                    starttime = starttime,
                                    endtime = endtime,
                                    startdate = DateTimeHelper.GetTimeStamp(time).ToString(),
                                    enddate = end,
                                    rulesname = rulesname == null ? "" : rulesname,
                                    rules = rules == null ? "" : rules,
                                    activty = dis,
                                    formula = str,
                                    roomtype = roomtype,
                                    lvyunnum = room,
                                    activtyStarttime = aStarttime,
                                    activtyEndtime = aEndtime,
                                };
                                list.Add(a);
                            }
                        }
                    }
                }
            }
            return list;
            //throw new NotImplementedException();
        }
        
        public List<RateXzByRmType> GetRateList(string starttimeStr, string endtimeStr, string roomtype, string hotelcode, string week)
        {
            Hashtable ht = new Hashtable();
            ht["roomtype"] = roomtype;
            ht["hotelcode"] = hotelcode;
            ht["time"] = starttimeStr;
            ht["end"] = endtimeStr;
            ht["week"] = week;
            StringBuilder strSql = new StringBuilder();

            //var a = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
            DataTable dt = DataFactory.SqlDataBase().GetDataTableProc("GetXzList", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strSql.Clear();
                var xzcode = dt.Rows[i]["xzcode"].ToString();
                strSql.Append("SELECT rules,codeid from associated WHERE xz_code= '" + xzcode + "'");
                var associated = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
                if (associated.Rows.Count > 0)
                {
                    strSql.Clear();
                    strSql.Append("SELECT code FROM " + associated.Rows[0]["rules"] + " WHERE id= '" + associated.Rows[0]["codeid"] + "'");
                    var code = DataFactory.SqlDataBase().GetObjectValue(strSql);
                    dt.Rows[i]["rulesname"] = associated.Rows[0]["rules"];
                    dt.Rows[i]["rules"] = code;
                }
                strSql.Clear();
                strSql.Append("SELECT activitycode FROM activity_link WHERE xz_code = '" + xzcode + "'");
                var activty = DataFactory.SqlDataBase().GetObjectValue(strSql);
                if (activty != null)
                {
                    strSql.Clear();
                    strSql.Append("SELECT discount FROM Activity WHERE Code= '" + activty + "'");
                    dt.Rows[i]["activty"] = DataFactory.SqlDataBase().GetObjectValue(strSql);
                }
                strSql.Clear();
                strSql.Append("SELECT formulaid,categoryid FROM price_formulaid WHERE xz_code = '" + xzcode + "'");
                var formula = DataFactory.SqlDataBase().GetDataTableBySQL(strSql);
                if (formula.Rows.Count > 0)
                {
                    List<System.String> listS = new List<System.String>();
                    for (int j = 0; j < formula.Rows.Count; j++)
                    {
                        listS.Add(formula.Rows[j]["formulaid"] + ":" + formula.Rows[j]["categoryid"]);
                    }
                    listS.ToArray();
                    var str = string.Join(",", listS);
                    dt.Rows[i]["formula"] = str;
                }
            }
            return JsonConvert.DeserializeObject<List<RateXzByRmType>>(JsonHelper.DataTableToJson(dt));
            //return JsonHelper.DataTableToJson(dt);
        }

        public List<RateXzByRmType> CrsGetRateCodexzByRoomtype(string starttimeStr, string endtimeStr, string roomtype, string hotelcode, int type)
        {
            db = new yudingEntities();
            var time = DateTime.Parse(starttimeStr);
            var end = DateTimeHelper.GetTimeStamp(DateTime.Parse(endtimeStr));
            var XzList = db.GetRcListByRoomType(roomtype, hotelcode, DateTime.Parse(starttimeStr)).ToList().GroupBy(x => x.xz_code).OrderByDescending(x => x.FirstOrDefault().px);
            var yuanjia = db.newroom_t.Where(x => x.roomtype == roomtype).FirstOrDefault().yuanjia;
            var list = new List<RateXzByRmType>();
            var d = db.ratecode_t.Where(x => x.hotelid == hotelcode && x.scenario == 1 && x.flag == 0 && x.type == type).FirstOrDefault().ratecode;
            foreach (var item in XzList)
            {
                var xzcode = item.FirstOrDefault().xz_code;
                var rate = db.rateroom_xz.Where(x => x.xz_code == xzcode).FirstOrDefault();
                var c = db.xztimestart_t.Where(x => x.startdate <= time && x.enddate >= time && x.xz_code == xzcode && x.flag == 0).FirstOrDefault();
                if (rate.ratecode == d )
                {
                    if (c != null)
                    {
                        Nullable<int> sum = 0;
                        foreach (var item1 in XzList)
                        {
                            sum += item1.FirstOrDefault().ordernum;
                        }
                        var price = db.everydate_price_t.Where(x => x.xz_code == xzcode && x.everydate == time).FirstOrDefault();
                        if (price != null)
                        {
                            var num = price.num;
                            var ordernum = price.ordernum;
                            var miaosha = db.miaosha_t.Where(x => x.xz_code == xzcode).FirstOrDefault();
                            var starttime = "";
                            var endtime = "";
                            if (miaosha != null)
                            {
                                starttime = DateTimeHelper.GetTimeStamp(DateTime.Parse(miaosha.starttime)).ToString();
                                endtime = DateTimeHelper.GetTimeStamp(DateTime.Parse(miaosha.endtime)).ToString();
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
                            Nullable<int> physicalnum = item.FirstOrDefault().pnum;
                            var a = new RateXzByRmType
                            {
                                xzcode = xzcode,
                                islock = item.FirstOrDefault().islock,
                                package = item.FirstOrDefault().package,
                                payway = item.FirstOrDefault().payway,
                                pay = item.FirstOrDefault().pay.ToString(),
                                xzname = item.FirstOrDefault().xz_name,
                                yuanjia = item.FirstOrDefault().yuanjia,
                                ordersum = sum,
                                ordernum = ordernum,
                                pnum = physicalnum,
                                num = num,
                                baojia = baojiaid,
                                price = item.FirstOrDefault().price,
                                starttime = starttime,
                                endtime = endtime,
                                startdate = DateTimeHelper.GetTimeStamp(time).ToString(),
                                enddate = end,
                                rulesname = rulesname,
                                rules = rules,
                                activty = dis,
                                formula = str,
                                roomtype = roomtype,
                            };
                            list.Add(a);
                        }
                    }
                }
            }
            return list;
            //throw new NotImplementedException();
        }
        public static List<info> GetRoomsByDate(string date, int type, string hotelcode)
        {
            //var r = Getpost("date");
            var time = DateTime.Parse(date);
            var week = (int)time.DayOfWeek;
            // var type = int.Parse(Getpost("type"));
            // var client = new RedisClient("127.0.0.1", 6379);
            //var str = hotelcode + time.ToString("yyyy-MM-dd") + type.ToString();
            //var roomListData = client.Get<List<info>>(str);
            //if (roomListData==null)
            //{
            using (var db = new yudingEntities())
            {
                var ratecode1 = db.ratecode_t.Where(x => x.flag == 0 && x.scenario == 1 && x.hotelid == hotelcode && x.type == type).FirstOrDefault();
                if (ratecode1 != null)
                {
                    var a = ratecode1.ratecode;
                    var ratecodelist = db.newroom_t.Where(x => x.flag == 0 && x.hotelid == hotelcode && x.type == type).OrderByDescending(x => x.pxid).ToList();
                    var list = new List<info>();
                    foreach (var item in ratecodelist)
                    {
                        var b = new info()
                        {
                            xz = new List<xz>()
                        };
                        var now = DateTime.Now;
                        var strWeek = week.ToString();
                        var roomxz = db.newroomtimes.Where(x => x.flag == 0 && x.timeflag == 0 && x.roomtype == item.roomtype && x.startdate <= time && x.enddate >= time
    ).OrderByDescending(x => x.pxid);
                        var px = roomxz.Max(x => x.px);
                        var room = roomxz.Where(x => x.px == px && x.week == strWeek).FirstOrDefault();
                        if (room != null)
                        {
                            Nullable<DateTime> roomstarttime = room.startdate;
                            Nullable<DateTime> roomendtime = room.enddate;
                            if (room.starttime != "" && room.endtime != "")
                            {
                                roomstarttime = now.AddHours(DateTime.Parse(room.starttime).Hour).AddMinutes(DateTime.Parse(room.starttime).Minute);
                                roomendtime = now.AddHours(DateTime.Parse(room.endtime).Hour).AddMinutes(DateTime.Parse(room.endtime).Minute);
                            }
                            if (room != null && time >= roomstarttime && time <= roomendtime)
                            {
                                var xzlist = db.rateroom_xz.Where(x => x.roomtype == item.roomtype && x.flag == 0 && x.ratecode == a && x.WeekIndex == week).ToList();
                                foreach (var item1 in xzlist)
                                {
                                    var ratecode = item1.ratecode;
                                    if (ratecode == a)
                                    {
                                        if (b.roomtype != item.roomtype)
                                        {
                                            b.price = room.yuanjia;
                                            b.pic = room.img;
                                            b.roomtype = item.roomtype;
                                            b.roomname = item.descript;
                                            var xz1 = new List<xz>();
                                            var xzcode = item1.xz_code;
                                            // var c = db.xztimestart_t.Where(x => x.startdate <= time && x.enddate >= time && x.xz_code == xzcode && x.flag == 0).FirstOrDefault();
                                            //if(c)
                                            Nullable<decimal> MinPrice = 0;
                                            var xzprice = db.GetRateCodexzByRoomtypes.Where(x => x.everydate == time && x.roomtype == item.roomtype && x.flag == 0 && x.WeekIndex == week).OrderByDescending(x => x.price).ToList();
                                            foreach (var item2 in xzprice)
                                            {
                                                var xztime = db.xztimestart_t.Where(x => x.xz_code == item2.xz_code && x.week == week.ToString() && x.startdate <= time && x.enddate >= time).OrderByDescending(x => x.px).FirstOrDefault();
                                                if (xztime != null && item2.price != 0)
                                                {
                                                    MinPrice = item2.price;
                                                }
                                            }
                                            //var min = db.GetRateCodexzByRoomtypes.Where(x => x.everydate == time && x.roomtype == item.roomtype && x.flag == 0&&x.WeekIndex==week ).Min(x => x.price);
                                            b.minprice = MinPrice;
                                            list.Add(b);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return list;

                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取房型列表
        /// </summary>
        /// <param name="hotelcode"></param>
        /// <param name="type"></param>
        /// <param name="week"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static List<info> GetRoomTypeList(yudingEntities db, string hotelcode, int type, int week, DateTime time)
        {
            var a = db.ratecode_t.Where(x => x.flag == 0 && x.scenario == 1 && x.hotelid == hotelcode && x.type == type).FirstOrDefault().ratecode;
            var ratecodelist = db.newroom_t.Where(x => x.flag == 0 && x.hotelid == hotelcode && x.type == type).ToList();
            var list = new List<info>();
            foreach (var item in ratecodelist)
            {

                var b = new info()
                {
                    xz = new List<xz>()
                };
                var now = DateTime.Now;
                var strWeek = week.ToString();
                var room = db.newroomtimes.Where(x => x.flag == 0 && x.timeflag == 0 && x.roomtype == item.roomtype && x.startdate <= time && x.enddate >= time && x.week == strWeek
).OrderByDescending(x => x.px).FirstOrDefault();
                if (room != null)
                {
                    Nullable<DateTime> roomstarttime = room.startdate;
                    Nullable<DateTime> roomendtime = room.enddate;
                    if (room.starttime != "" && room.endtime != "")
                    {
                        roomstarttime = now.AddHours(DateTime.Parse(room.starttime).Hour).AddMinutes(DateTime.Parse(room.starttime).Minute);
                        roomendtime = now.AddHours(DateTime.Parse(room.endtime).Hour).AddMinutes(DateTime.Parse(room.endtime).Minute);
                    }
                    if (room != null && time >= roomstarttime && time <= roomendtime)
                    {
                        var xzlist = db.rateroom_xz.Where(x => x.roomtype == item.roomtype && x.flag == 0 && x.ratecode == a && x.WeekIndex == week).ToList();
                        foreach (var item1 in xzlist)
                        {
                            var ratecode = item1.ratecode;
                            if (ratecode == a)
                            {
                                if (b.roomtype != item.roomtype)
                                {
                                    b.price = room.yuanjia;
                                    b.pic = room.img;
                                    b.roomtype = item.roomtype;
                                    b.roomname = item.descript;
                                    var xz1 = new List<xz>();
                                    var xzcode = item1.xz_code;
                                    // var c = db.xztimestart_t.Where(x => x.startdate <= time && x.enddate >= time && x.xz_code == xzcode && x.flag == 0).FirstOrDefault();
                                    //if(c)
                                    Nullable<decimal> MinPrice = 0;
                                    var xzprice = db.GetRateCodexzByRoomtypes.Where(x => x.everydate == time && x.roomtype == item.roomtype && x.flag == 0 && x.WeekIndex == week).OrderByDescending(x => x.price).ToList();
                                    foreach (var item2 in xzprice)
                                    {
                                        var xztime = db.xztimestart_t.Where(x => x.xz_code == item2.xz_code && x.week == week.ToString() && x.startdate <= time && x.enddate >= time).OrderByDescending(x => x.px).FirstOrDefault();
                                        if (xztime != null && item2.price != 0)
                                        {
                                            MinPrice = item2.price;
                                        }
                                    }
                                    //var min = db.GetRateCodexzByRoomtypes.Where(x => x.everydate == time && x.roomtype == item.roomtype && x.flag == 0&&x.WeekIndex==week ).Min(x => x.price);
                                    b.minprice = MinPrice;
                                    list.Add(b);
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <param name="db"></param>
        /// <param name="key"></param>
        /// <param name="hotelcode"></param>
        /// <param name="type"></param>
        public static void Refresh(yudingEntities db, string key, string hotelcode, int type, DateTime time)
        {
            //var min = db.everydate_price_t.Where(x => x.hotelid == hotelcode&&x.xz_code==key&&x.everydate==time).Min(x => x.everydate);
            //var max = db.everydate_price_t.Where(x => x.hotelid == hotelcode&&x.xz_code==key).Max(x => x.everydate);
            // var min = DateTime.Parse("");
            //var max = DateTime.Parse("");
            //var days = (int)(max - min).TotalDays;
            var client = new RedisClient("127.0.0.1", 6379);
            var week = (int)time.DayOfWeek;
            var list = GetRoomTypeList(db, hotelcode, type, week, time);
            if (list.Count > 0)
            {
                var str = hotelcode + time.ToString("yyyy-MM-dd")+type.ToString();
                client.Set<List<info>>(str, list);
                // var starttime = time.ToString("yyyy-MM-dd");
                //foreach (var item in list)
                //{
                //    client.Set<List<RateXzByRmType>>(hotelcode + item.roomtype + time.ToString("yyyy-MM-dd"), GetRateCodexzByRoomtype(db, starttime, starttime, item.roomtype, hotelcode, type));
                //}
            }
        }
    }
}