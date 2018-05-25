using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.foxhis.xop;
using Ks_Crs2._0_SDK.AdvancedAPIs;
using Ks_Crs2._0_SDK.RequestModel;
using Newtonsoft.Json;
using RM.Common.DotNetHttp;
using RM.Common.DotNetJson;
using SimpleJson;
using WS.SDK;
using yuding.JsonRequest;
using yuding.JsonResult;
using yuding.Model;
using RM.Common.DotNetCode;

namespace yuding.API
{
    /// <summary>
    /// Test 的摘要说明
    /// </summary>
    public class Test : IHttpHandler
    {
        public string hotelcode;
        JsonReturn jsonResult = new JsonReturn();
        protected LogHelper Logger = new LogHelper("order");
        public void ProcessRequest(HttpContext context)
        {
            hotelcode = context.Request.Form["hotelcode"];
            var action = context.Request.QueryString["action"];
            switch (action)
            {
                case "XiaDan":
                    XiaDan(context);
                    break;
            }
            HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
        }
        public static string Getpost(string name)
        {
            string value = HttpContext.Current.Request.Form[name];
            //string value = HttpContext.Current.Request.QueryString[name];
            return value == null ? string.Empty : value.Trim();
        }
        private void XiaDan(HttpContext context)
        {
            var orderdata = Getpost("orderdata");
            var increasedata = Getpost("increasedata");
            Logger.WriteLog(orderdata);
            var increaselist = JsonConvert.DeserializeObject<List<order_increase>>(increasedata);
            var orderlist = JsonConvert.DeserializeObject<List<order>>(orderdata);
            using (var db = new yudingEntities())
            {
                if (orderlist.Count > 0)
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        bool isorder = true;
                        foreach (var item in orderlist)
                        {
                            //var hotelname = db.hotel_list.Where(x => x.hotelId ==item. hotelcode).FirstOrDefault();
                            //item.hotelname = hotelname.hotelName;
                            var roomxz = db.rateroom_xz.Where(x => x.roomtype == item.roomtype&&x.xz_code==item.xz_code).FirstOrDefault();
                            var newroom = db.ratecode_t.Where(x => x.ratecode == roomxz.ratecode).FirstOrDefault();
                            var date = DateTime.Parse(item.arr);
                            var every = db.everydate_price_t.Where(x => x.everydate == date && x.xz_code == item.xz_code).FirstOrDefault();
                            var room = db.newroom_t.Where(x => x.roomtype == item.roomtype).FirstOrDefault();
                            var hotel = db.hotel_list.FirstOrDefault(x => x.hotelId == item.hotelcode);
                            if (every.ordernum < every.num)
                            {
                                if (roomxz.qudao != null && roomxz.qudao != "")
                                {
                                    //绿云下单
                                    if (!PlaceOrder(db, item, hotel.pms,  room.pms, roomxz.pmsratecode, roomxz.qudao, true,roomxz.cusno))
                                    {
                                        isorder = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (newroom.qudao != "" && newroom.qudao != null)
                                    {
                                        //绿云下单
                                        if (!PlaceOrder(db, item, hotel.pms,  room.pms, newroom.pmsratecode, newroom.qudao, true,roomxz.cusno))
                                        {
                                            isorder = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        //本地下单
                                        if (!PlaceOrder(db, item))
                                        {
                                            isorder = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                isorder = false;
                                break;
                            }
                        }
                        if (isorder)
                        {
                            db.SaveChanges();
                            //transaction.Commit();
                            if (increaselist != null)
                            {
                                if (increaselist.Count > 0)
                                {
                                    foreach (var item in increaselist)
                                    {
                                        AddIncrease(db, item);
                                    }
                                    db.SaveChanges();
                                }
                            }
                            transaction.Commit();
                            jsonResult.code = "200";
                            jsonResult.msg = "下单成功";
                        }
                        else
                        {
                            jsonResult.code = "500";
                            jsonResult.msg = "满房";
                        }
                    }
                }
            }
        }
        private void AddIncrease(yudingEntities db, order_increase item)
        {
            var increase = new increase_order()
            {
                descript = item.increasedescribe,
                hotelid = hotelcode,
                increasenum = int.Parse(item.increasenum),
                increaseprice = int.Parse(item.increaseprice),
                pay = int.Parse(item.pay),
                price = item.price,
                roomtype = item.roomtype,
                sessionid = item.sessionid,
                tian = item.tian,
            };
            db.increase_order.Add(increase);

        }
        private bool PlaceOrder(yudingEntities db, order item, string pmscode = "0", string pms = "", string pmsratecode = "", string qudao = "", bool ispms = false,string  cusno="")
        {
            var order = new order_t()
            {
                ptchannel = item.ptchannel,
                isSendMsg = "0",
                hotelname = item.hotelname,
                City = item.City,
                channelid = item.channelid,
                guestname = item.guestname,
                arrivetime = DateTime.Parse(item.arr),
                baojia = item.baojia,
                bosscard = item.resby,
                contact_mobile = item.mobile,
                contact_name = item.name,
                count = int.Parse(item.count),
                hotelcode = item.hotelcode,
                leavetime = DateTime.Parse(item.dep),
                ordernumber = item.ordernumber,
                pay = item.pay,
                yuanjia = item.rate.ToString(),
                trueRate = item.truerate,
                notes = item.remark,
                roomname = item.roomname,
                roomtype = item.roomtype,
                xz_code = item.xz_code,
                xz_name = item.xz_name,
                sessionid = item.sessionid,
                formulaid = item.formulaid,
                categoryid = item.categoryid,
                increasemoney = item.increasemoney,
                yhmoney = decimal.Parse(item.yhmoney),
                rate = item.rate,
                tpid = item.tpid,
                type = int.Parse(item.type),
                Fmoney = item.Fmoney,
                TicketSn = item.TicketSn,
            };
            if (ispms && item.type == "0")
            {
                if (pmscode == "1")
                {
                    ly ly = new ly("http://115.159.81.168:8100/ipmsgroup/router", item.hotelcode);
                    Dictionary<string, string> parms = new Dictionary<string, string>();
                    parms.Add("arr", item.arr + " 12:00:00");
                    parms.Add("dep", item.dep + " 12:00:00");
                    parms.Add("rmtype", pms);
                    parms.Add("channel", qudao);
                    parms.Add("rateCode", pmsratecode);
                    parms.Add("rmNum", item.count);
                    parms.Add("rsvMan", item.name);
                    parms.Add("sex", "1");
                    parms.Add("mobile", item.mobile);
                    parms.Add("idType", "");
                    parms.Add("idNo", "");
                    parms.Add("email", "");
                    parms.Add("cardType", "");
                    parms.Add("cardNo", "");
                    parms.Add("adult", "1");
                    parms.Add("resultCode", "");
                    parms.Add("remark", item.remark);
                    var json = new List<object>();
                    foreach (var item1 in item.everydate)
                    {
                        var everydate = new
                        {
                            date = item1.date + " 12:00:00",
                            realRate = item1.realRate - item1.yhmoney,
                        };
                        json.Add(everydate);
                    }
                    var everyDayRateJson = JsonConvert.SerializeObject(json);
                    parms.Add("everyDayRate", everyDayRateJson);
                    //parms.Add("everyDayRate", "[{\"date\":\"" + item.arr + " 12:00:00\",\"realRate\":\"" + (item.rate - int.Parse(item.yhmoney)).ToString() + "\"}]");
                    var result = ly.xiadan(parms, "http://115.159.81.168:8100/ipmsgroup/CRS/book");
                    //success:
                    var crsNo = JsonHelper.GetJsonValue(result, "crsNo");
                    if (crsNo != "")
                    {
                        var date = DateTime.Parse(item.arr);
                        var every = db.everydate_price_t.Where(x => x.everydate == date && x.xz_code == item.xz_code).FirstOrDefault();
                        every.ordernum += 1;
                        order.lvorder = crsNo;
                        db.order_t.Add(order);
                        foreach (var item1 in item.everydate)
                        {
                            var time = DateTime.Parse(item1.date);
                            //BookingWeb.Refresh(db, item.xz_code, item.hotelcode, int.Parse(item.type), time);
                        }
                        return true;
                    }
                    //error
                    else
                    {
                        return false;
                    }
                }
                if (pmscode == "2")
                {
                    var service = new WxAPI(item.hotelcode);
                    var mp = new MPDBEntities();
                    var hotel = mp.MPConfigs.FirstOrDefault(x => x.ShopCode == item.hotelcode);
                    var hotel1 = db.hotel_list.FirstOrDefault(x => x.hotelId == item.hotelcode);
                    if (hotel != null)
                    {
                        var nightNum = (DateTime.Parse(item.dep) - DateTime.Parse(item.arr)).Days.ToString();
                        var res3 = service.Reservation("10", DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss"), "1009", hotel1.username, hotel1.pwd, item.guestname, "NA", "ADD", "6016913", "", "CHANGE", item.remark, "GUEST", "MALE", item.name, "", "C", "NA", "ADD", "CHANGE", "RESERVED", "", pms, "", "DAY", item.arr + " 18:00:00", nightNum, "", "ADULT", "1", "CHANGE", pmsratecode, hotel.src, hotel.market, item.count, qudao, order.contact_mobile, order.contact_name, "");
                        if (res3 != null)
                        {
                            if (res3.Head.retcode == "00001")
                            {
                                var date = DateTime.Parse(item.arr);
                                var every = db.everydate_price_t.Where(x => x.everydate == date && x.xz_code == item.xz_code).FirstOrDefault();
                                every.ordernum += 1;
                                order.lvorder = res3.Body.ReservationResponse.Reservation.confirmationID;
                                db.order_t.Add(order);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                if (pmscode == "0")
                {
                    for (var start = DateTime.Parse(item.arr); start < DateTime.Parse(item.dep); start = start.AddDays(1))
                    {
                        //var date = DateTime.Parse(start);
                        var every = db.everydate_price_t.Where(x => x.everydate == start && x.xz_code == item.xz_code).FirstOrDefault();
                        every.ordernum += 1;
                        db.order_t.Add(order);
                        //BookingWeb.Refresh(db, item.xz_code, item.hotelcode, int.Parse(item.type), start);
                    }
                    return true;
                }
                if (pmscode == "3")
                {
                    var mp = new MPDBEntities();
                    var hotel = mp.MPConfigs.FirstOrDefault(x => x.ShopCode == item.hotelcode);
                    var hotel1 = db.hotel_list.FirstOrDefault(x => x.hotelId == item.hotelcode);
                    // 平台提供的地址
                    string url = hotel.orderUrl;
                    // 平台appkey
                    string appKey ="KSKJ";
                    // 平台授权码
                     string secret = hotel1.pwd;
                    // 使用哪个酒店的身份接入 
                     string hotelId = "G000001";
                     var client = new XopClient(url, appKey, secret, hotelId);
                     // 开始登录平台
                     var rspLogin = client.login();

                     if (!XopClient.isResponseSuccess(rspLogin))
                     {
                         return false;
                     }
                     var daily = new JsonArray();
                     foreach (var item1 in item.everydate)
                     {
                         var one = new JsonObject();
                         one["ratecode"] = pmsratecode;
                         one["rate"] = item1.realRate - item1.yhmoney;
                         one["date"] = item1.date + " 12:00:00";
                         one["rmrate"] = item1.realRate - item1.yhmoney;
                         daily.Add(one);
                     }
                     var remark = Enum.Parse(typeof(Pay), item.pay, false).ToString();
                     var r = client.saveReservation(qudao, hotel1.username, pmsratecode, pms, item.count, item.everydate[0].realRate.ToString(), item.name, item.name, item.arr, item.dep, "2", daily, hotel.rsvType, item.mobile, "支付方式：" + remark + "," + item.remark, hotel.src, hotel.market, cusno);
                     if (r.success)
                     {
                         // var c = client.cxlReservation("KSKJ", "H000069", r.results[0].rsvno, "测试", "T");
                         var date = DateTime.Parse(item.arr);
                         var every = db.everydate_price_t.Where(x => x.everydate == date && x.xz_code == item.xz_code).FirstOrDefault();
                         every.ordernum += 1;
                         order.notes = "支付方式：" + remark + "," + item.remark;
                         order.lvorder = r.results[0].rsvno;
                         db.order_t.Add(order);
                         return true;
                     }
                }
                if (pmscode == "4")
                {
                    //var mp = new MPDBEntities();
                    //var hotel = mp.MPConfigs.FirstOrDefault(x => x.ShopCode == item.hotelcode);
                    //var hotel1 = db.hotel_list.FirstOrDefault(x => x.hotelId == item.hotelcode);
                    //PlaceOrderData orderData = new PlaceOrderData
                    //{
                    //    srcHotelGroupCode = "MKYL",
                    //    //webFrom = "KUAISHUN",
                    //    //srcHotelGroupCode="GCBZG",
                    //    hotelCode = item.hotelcode,
                    //    hotelGroupCode = hotel.LvyunHotelgroupId,
                    //    operationType = "Book",
                    //    otaChannel = "CRS",
                    //    otaRsvNo = Guid.NewGuid().ToString(),
                    //    productCode = pmsratecode,
                    //    bookOrderInfoRQ = new P_BookOrderInfoRQ()
                    //    {
                    //        arr = item.arr,
                    //        dep = item.dep,
                    //        earlyArrTime = item.arr,
                    //        lastArrTime = item.dep,
                    //        paySta = "2",
                    //        guests = new List<Guests>(),
                    //        everyDayPrices = new List<EveryDayPrices>(),
                    //        rmNum =int.Parse( item.count),
                    //        rateSum = 0,
                    //        rsvMan = item.name,
                    //        mobile =item.mobile,
                    //        adult =1,
                    //        remark = item.remark,
                    //        isCheckRoomQuentity = "T",
                    //        needPay = "F",
                    //        isZeroOrder = "F",
                    //    }
                    //};
                    //var everyprice = new List<EveryDayPrices>();
                    //var guests = new List<Guests>();
                    //var m = new
                    //{
                    //    hotelGroupCode = hotel.LvyunHotelgroupId,
                    //    hotelCode = hotel.LvyunHotelId,
                    //    fromDate = item.arr,
                    //    toDate = item.dep,
                    //    otaChannel = "CRS",
                    //    productCode = pmsratecode
                    //};
                    //var crsEveryPrice = CrsAPI.GetproductBaseDetail(Config.CRS_URL, m);
                    //if (crsEveryPrice.resultCode == ReturnCode.请求成功 && crsEveryPrice != null)
                    //{
                    //    decimal rateSum = 0;
                    //    foreach (var item in crsEveryPrice.resultInfo[0].everyDetails)
                    //    {
                    //        var price = new EveryDayPrices
                    //        {
                    //            costPrice = item.costPrice,
                    //            isClosed = item.isClosed,
                    //            realPrice = item.realPrice,
                    //            rsvDate = item.rsvDate,
                    //            saleNum = item.saleNum,
                    //        };
                    //        rateSum += item.costPrice * rmNum;
                    //        everyprice.Add(price);
                    //    }
                    //    orderData.bookOrderInfoRQ.everyDayPrices = everyprice;
                    //    foreach (var item in data.guests)
                    //    {
                    //        var guest = new Guests
                    //        {
                    //            name = item,
                    //        };
                    //        guests.Add(guest);
                    //    }
                    //    orderData.bookOrderInfoRQ.guests = guests;
                    //    orderData.bookOrderInfoRQ.rateSum = rateSum;
                    //    //result.data = orderData;
                    //    orderData.bookOrderInfoRQ.everyDayPrices = everyprice;
                    //    order.subRoomTypeId = data.productCode;
                    //    order.roomTypeId = room.roomTypeId;
                    //    order.hotelGroupCode = room.hotelGroupCode;
                    //    var str = JsonConvert.SerializeObject(orderData);

                    //    var orderResult = CrsAPI.PlaceOrder(Config.CRS_URL, orderData);
                    //    if (orderResult.resultCode == ReturnCode.请求成功 && orderResult != null)
                    //    {
                    //        Logger.WriteLog(string.Concat(new string[]{
                    //                        "绿云数据："+str+"\r\n",
                    //                        //  "下单数据："+JsonConvert.SerializeObject(data)+"\r\n",
                    //                        "绿云接口返回："+orderResult.resultMessage
                    //                    }));
                    //        //var orderResult = new PlaceOrderResult();
                    //        //orderResult.resultCode = ReturnCode.请求失败;
                    //        //orderResult.resultMessage = "暂时关闭调试";
                    //        if (orderResult.resultCode == ReturnCode.请求成功)
                    //        {
                    //            order.gcRsvNo = orderResult.resultInfo;
                    //            order.apiType = "SUCCESS";
                    //            result.code = ApiCode.成功;
                    //            result.message = "成功";
                    //            order.apiMessage = "成功";
                    //            result.orderId = data.orderId;
                    //            result.orderStatus = 1;
                    //            result.amount = data.amount;
                    //            result.confirmNum = orderResult.resultInfo;
                    //        }
                    //        else
                    //        {
                    //            order.apiType = "FAIL";
                    //            result.code = ApiCode.成功;
                    //            result.message = orderResult.resultMessage;
                    //            order.apiMessage = orderResult.resultMessage;
                    //            result.orderStatus = 2;
                    //        }
                    //        Logger.WriteLog(string.Concat(new string[]{
                    //                        "下单数据："+JsonConvert.SerializeObject(data)+"\r\n",                               
                    //                        "绿云接口返回："+JsonConvert.SerializeObject( orderResult)+"\r\n"
                    //                    }));
                    //    }
                    //    else
                    //    {
                    //        order.apiType = "FAIL";
                    //        result.orderStatus = 2;
                    //        result.code = ApiCode.成功;
                    //        order.apiMessage = "接口调用失败";
                    //    }
                    //}
                    //else
                    //{
                    //    Logger.WriteLog(string.Concat(new string[]{
                    //                        //"下单数据："+JsonConvert.SerializeObject(data)+"\r\n",                                
                    //                        "没有该产品价格"
                    //                    }));
                    //    order.apiType = "FAIL";
                    //    result.code = ApiCode.成功;
                    //    result.message = "没有该产品价格";
                    //    order.apiMessage = "没有该产品价格";
                    //    result.orderStatus = 2;
                    //}
 
                }
                return false;
            }
            else
            {
                for (var start = DateTime.Parse(item.arr); start < DateTime.Parse(item.dep); start = start.AddDays(1))
                {
                    //var date = DateTime.Parse(start);
                    var every = db.everydate_price_t.Where(x => x.everydate == start && x.xz_code == item.xz_code).FirstOrDefault();
                    every.ordernum += 1;
                    db.order_t.Add(order);
                    //BookingWeb.Refresh(db, item.xz_code, item.hotelcode, int.Parse(item.type), start);
                }
                return true;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}