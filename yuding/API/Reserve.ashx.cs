using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using RM.Common.DotNetHttp;
using yuding.JsonResult;
using yuding.Model;
using System.Data.Entity.Migrations;
using yuding.JsonRequest;
using System.Data;
using System.Reflection;
using System.Data.Entity.Infrastructure;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Collections;
using RM.Common.DotNetJson;
using System.Data.Entity.SqlServer;
using yuding.BLL;
using Newtonsoft.Json.Converters;
using RM.Common.DotNetCode;
using System.Threading;
using yuding.Model.ServiceModel;
using ServiceStack.Redis;
using Ks_Crs2._0_SDK.AdvancedAPIs;
using Ks_Crs2._0_SDK.RequestModel;
using Ks_Crs2._0_SDK.ResponseModel;
using yuding.TEST;
using WS.SDK;
using com.foxhis.xop;

namespace yuding.API
{
    /// <summary>
    /// Reserve 的摘要说明
    /// </summary>
    public class Reserve : IHttpHandler
    {
        public string oid;
        public string hotelcode;
        public string yemian;
        public string logtype;
        public string operation;
        public int rows = 10;
        JsonReturn jsonResult = new JsonReturn();
        public string logStr;
        public static object _lock = new object();
        BookingWeb bll = new BookingWeb();
        public void ProcessRequest(HttpContext context)
        {
            var action = context.Request.QueryString["action"];
            oid = context.Request.Form["oid"];
            hotelcode = context.Request.Form["hotelcode"];
            yemian = context.Request.Form["yemian"];
            logtype = context.Request.Form["logtype"];
            operation = context.Request.Form["operation"];
            logStr = context.Request.Form["logJson"];
                switch (action)
                {
                    #region 系统图片CRUD
                    case "GetSystemPic":
                        GetSystemPic(context);//获取系统图片
                        break;
                    case "AddOrUpdateSystemPic":
                        AddOrUpdateSystemPic(context);//新增或修改系统图片
                        break;
                    case "GetClassPicByType":
                        GetClassPicByType(context);//获取分类图
                        break;
                    case "AddOrUpdateClassPic":
                        AddOrUpdateClassPic(context);
                        break;
                    case "GetHotelFacilities":
                        GetHotelFacilities(context);//获取酒店设施
                        break;
                    case "SetHotelFacilities":
                        SetHotelFacilites(context);//添加酒店设施
                        break;
                    case "DelHotelFacilities":
                        DelHotelFacilities(context);//删除酒店设施
                        break;
                    case "DelPicDb":
                        DelPicDb(context);//删除系统图片库
                        break;
                         
                    #endregion
                    #region 海报图CURD
                    case "GetHotelPic":
                        GetHotelPic(context);//获取酒店海报图
                        break;
                    case "AddOrUpdateHotelPic":
                        AddOrUpdateHotelPic(context);//新增或修改酒店海报图
                        break;
                    case "DelHotelPic":
                        DelHotelPic(context);
                        break;
                    #endregion
                    #region 酒店介绍编辑
                    case "GetHotelPresentation":
                        GetHotelPresentation(context);//获取酒店介绍
                        break;
                    case "AddOrUpdateHotelPresentation":
                        AddOrUpdateHotelPresentation(context);//修改酒店介绍
                        break;
                    case "SearchHotel":
                        SearchHotel(context);//搜索酒店
                        break;
                    case "SearchHotelByCity":
                        SearchHotelByCity(context);
                        break;
                    case "SearchHotelByGroupCode":
                        SearchHotelByGroupCode(context);
                        break;
                    case "GetHotelGroupList":
                        GetHotelGroupList(context);
                        break;
                    #endregion
                    #region 房型信息管理编辑
                    case "GetRooms":
                        GetRooms(context);//获取房型基础信息
                        break;
                    case "GetRooms_detail":
                        GetRooms_detail(context);
                        break;
                    case "AddOrUpdateRoom":
                        AddOrUpdateRoom(context);//新增房型或修改房型基础信息
                        break;
                    case "UpdateQX":
                        UpdateQX(context);//更新取消细则
                        break;
                    case "UpdateYD":
                        UpdateYD(context);//更新预定细则
                        break;
                    case "UpdateRoomRemark":
                        UpdateRoomRemark(context);//房型说明
                        break;
                    case "UpdateRoomsCoverPic":
                        UpdateRoomsCoverPic(context);//添加封面图
                        break;
                    case "StopRoom":
                        StopRoom(context);//停用启用房型
                        break;
                    case "GetRoomPic":
                        GetRoomPic(context);//根据type获取房间轮播图和封面图
                        break;
                    case "DelRoomPic":
                        DelRoomPic(context);//删除房型轮播图
                        break;
                    case "AddOrUpdateRoomPic":
                        AddOrUpdateRoomPic(context);//增加或修改房价轮播图和封面图
                        break;
                    case "GetRoomFacilities":
                        GetRoomFacilities(context);//获取房间设施
                        break;
                    case "AddOrUpdateRoomFacilities":
                        AddOrUpdateRoomFacilities(context);//更新房型设施
                        break;
                    case "GetRoomDisplay":
                        GetRoomDisplay(context);//查询房型显示规则
                        break;
                    case "SetRoomDisplay":
                        SetRoomDisplay(context);//设置显示时间段(生成)
                        break;
                    case "GetRoomDisplayGroup":
                        GetRoomDisplayGroup(context);//分组
                        break;
                    case "StopRoomDisplay":
                        StopRoomDisplay(context);//停用
                        break;
                    case "GetRoomDisplayByBatch":
                        GetRoomDisplayByBatch(context);//根据批次号获取
                        break;
                    case "DelRoomDisplayByBatch":
                        DelRoomDisplayByBatch(context);//删除显示时间段
                        break;
                    case "AddPicDb":
                        AddPicDb(context);//图片库添加
                        break;
                    case "GetPicDbByHotelcode":
                        GetPicDbByHotelcode(context);//获取图片库数据
                        break;
                      
                    #endregion
                    #region 房价管理
                    case "AddOrUpdateScenceCode":
                        AddOrUpdateScenceCode(context);//添加一条情景码(√)
                        break;
                    case "StopScenceCode":
                        StopScenceCode(context);//情景码停用(√)
                        break;
                    case "GetScenceCode":
                        GetScenceCode(context);//获取酒店的所有情景码
                        break;
                    case "AddOrUpdateRateCode_xz":
                        AddOrUpdateRateCode_xz(context);//添加或更新房价码细则(√)
                        break;
                    case "CopyRatecode_xz":
                        CopyRatecode_xz(context);//复制房价码细则(√)
                        break;
                    case "GetRateCode_xz":
                        GetRateCode_xz(context);//获取房价码
                        break;
                    case "GetRateCode_xz_List":
                        GetRateCode_xz_List(context);
                        break;
                    case "AddOrUpdateRateCode_Advanced":
                        AddOrUpdateRateCode_Advanced(context);//添加或更新房价码细则高级设置(√)
                        break;
                    case "GetAdvanced":
                        GetAdvanced(context);//获取高级设置内容
                        break;
                    case "StopRateCode_xz":
                        StopRateCode_xz(context);//tingyong(√)
                        break;
                    case "SetRateCodeDisplay_xz":
                        SetRateCodeDisplay_xz(context);//设置房价码细则显示时间段
                        break;
                    case "GetRateCodeGroup_xz":
                        GetRateCodeGroup_xz(context);//分组
                        break;
                    case "GetRateCodeByBatch_xz":
                        GetRateCodeByBatch_xz(context);//根据批次号获取细则内容
                        break;
                    case "DelRateCodeByBatch_xz":
                        DelRateCodeByBatch_xz(context);//根据批次号删除
                        break;
                    case "GetRateCode_xz_all":
                        GetRateCode_xz_all(context);//获取所有房型房价
                        break;
                    case "SetEveryDate_Price":
                        SetEveryDate_Price(context);//设置每日价格(*添加日志)
                        break;   
                    case "GetActivityList":
                        GetActivityList(context);//获取活动
                        break;
                    case "AddOrUpdateActivity":
                        AddOrUpdateActivity(context);//新增或更新活动
                        break;
                    case "UpdateActivityFlag":
                        UpdateActivityFlag(context);
                        break;
                    case "everydate_price_t_one_list":
                        everydate_price_t_one_list(context);////查看一天所有房价明细码的价格
                        break;
                    case "GetRateCodeByRoomtype_xz":
                        GetRateCodeByRoomtype_xz(context);//根据房型获取细则
                        break;
                    case "UpdateEveryDate":
                        UpdateEveryDate(context);//更新每日
                        break;
                    case "BatchStopRatecode_xz":
                        BatchStopRatecode_xz(context);//批量锁房
                        break;
                    #endregion
                    #region 特殊操作
                    case "GetRulesList":
                        GetRulesList(context);////查看所有功能
                        break;
                    case "GetRules_xz":
                        GetRules_xz(context);//查看功能细则
                        break;
                    case "AddOrUpdateRules":
                        AddOrUpdateRules(context);//添加或修改细则
                        break;
                    case "Getyhcode":
                        Getyhcode(context);//查询所有优惠码
                        break;
                    case "YhCodeFlag":
                        YhCodeFlag(context);//优惠码停用启用
                        break;
                    case "AddOrUpdateYhCode":
                        AddOrUpdateYhCode(context);//添加改优惠码
                        break;
                    case "UpdateYhCode":
                        UpdateYhCode(context);//修改优惠码
                        break;
                    case "GetOpenIdList":
                        GetOpenIdList(context);//获取openId列表
                        break;
                    case "AddOrUpdateOpenId":
                        AddOrUpdateOpenId(context);//添加或者修改openID信息
                        break;
                    case "DelOpenId":
                        DelOpenId(context);
                        break;
                    case "LockRoom":
                        LockRoom(context);//批量锁房
                        break;
                    case "GetIncreaseList":
                        GetIncreaseList(context);//获取房型增值服务列表
                        break;
                    case "GetHotelIncreaseList":
                        GetHotelIncreaseList(context);//获取酒店增值服务
                        break;
                    case "StopIncrease":
                        StopIncrease(context);//停用增值码
                        break;
                    case "AddOrUpdateIncrease":
                        AddOrUpdateIncrease(context);//修改或添加增值服务
                        break;
                    case "RelateIncreaseAndRateCode":
                        RelateIncreaseAndRateCode(context);//房价码关联的增值服务
                        break;
                    case "RateCodeIncreaseList":
                        RateCodeIncreaseList(context);//查询房价码下的增值服务
                        break;
                    case "GetSpecial":
                        GetSpecial(context);//获取所有特殊操作
                        break;
                    case "EditSpecialminutes":
                        EditSpecialminutes(context);//编辑特殊操作
                        break;
                    case "EditSpecialIsName":
                        EditSpecialIsName(context);
                        break;
                    #endregion
                    #region 订单查看
                    case "GetOrderList":
                        GetOrderList(context);//分页查看所有订单
                        break;
                    case "GetOrderListByConditions":
                        GetOrderListByConditions(context);
                        break;
                    case "UpdateOrderState":
                        UpdateOrderState(context);//修改订单号和支付状态
                        break;
                    case "GetOrderListBySessionid":
                        GetOrderListBySessionid(context);//订单显示，根据批次号分组
                        break;
                    case"GetSessionIdOrder":
                        GetSessionIdOrder(context);//获取批次号下的所有订单
                        break;
                    case "GetOrderBySessionid":
                        GetOrderBySessionid(context);
                        break;
                    case "GetOrderListByIsPay":
                        GetOrderListByIsPay(context);
                        break;
                    case"GetOrder":
                        GetOrder(context);
                        break;
                    case "GetYFOrder":
                        GetYFOrder(context);//预付订单统计表
                        break;
                    #endregion
                    #region 前端
                    case "GetRoomsByDate":
                        GetRoomsByDate(context);
                        break;
                    case "WxRoomList":
                        WxRoomList(context);//新的查询接口
                        break;
                    case "GetRateCodexzByRoomtype":
                        GetRateCodexzByRoomtype(context);
                        break;
                    case "GetRoomInfo":
                        GetRoomInfo(context);
                        break;
                    case "XiaDan":
                        XiaDan(context);//下单
                        break;
                    case "CrsOrder":
                        CrsOrder(context);
                        break;
                    case "UpdateOrder":
                        UpdateOrder(context);//修改备注
                        break;
                    case "UpdatePayState":
                        UpdatePayState(context);
                        break;
                    case "GetOrderListByState":
                        GetOrderListByState(context);
                        break;
                    case "GetOrderById":
                        GetOrderById(context);
                        break;
                    case  "GetRoomByRoomtype":
                        GetRoomByRoomtype(context);
                        break;
                    case "CancelOrder":
                        CancelOrder(context);
                        break;
                    case "UpdateOrderYh":
                        UpdateOrderYh(context);
                        break;

                    case "GetRoomAvail":
                        GetRoomAvail(context);//查询可用房量
                        break;
                    #endregion
                    #region 发票
                    case "AddInvoice":
                        AddInvoice(context);
                        break;
                    case "UpdateInvoice":
                        UpdateInvoice(context);
                        break;
                    case "DelInvoice":
                        DelInvoice(context);
                        break;
                    case "GetInvoice":
                        GetInvoice(context);
                        break;
                    case "UpdateState":
                        UpdateState(context);
                        break;
                    #endregion
                    #region CRS 
                    case "GetHotelGroupAll":
                        GetHotelGroupAll(context);
                        break;
                    case "GetHotelListByGroupCode":
                        GetHotelListByGroupCode(context);
                        break;
                    case "GetCrsOrderInfo":
                        GetCrsOrderInfo(context);
                        break;
                    case "SaveCrsOrder":
                        SaveCrsOrder(context);
                        break;
                    case "CrsGetRateCodexzByRoomtype":
                        CrsGetRateCodexzByRoomtype(context);
                        break;
                    case "GetCrsHotelList":
                        GetCrsHotelList(context);
                        break;
                    case "GetCrsProductList":
                        GetCrsProductList(context);
                        break;
                    case "GetProductDeatil":
                        GetProductDeatil(context);
                        break;
                    case "CrsCheckOrder":
                        CrsCheckOrder(context);
                        break;
                    case "CrsPlaceOrder":
                        CrsPlaceOrder(context);
                        break;
                    case "CrsUpdateOrder":
                        CrsUpdateOrder(context);
                        break;
                    case "CrsCancelOrder":
                        CrsCancelOrder(context);
                        break;
                    #endregion
                    case "HotelList":
                        TEST(context);
                        break;
                    case "GetLog":
                        GetLog(context);
                        break;
                    #region 中间层
                    case "OrderService":
                        OrderService(context);//支付完成
                        break;
                    #endregion
                    case "RepoRate":
                        RepoRate(context);//复购率
                        break;
                    case "AddActivityUrl":
                        AddActivityUrl(context);
                        break;
                    case "GetOrderListBySessionidQT":
                        GetOrderListBySessionidQT(context);
                        break;
                    case "SetCategroyInfo":
                        SetCategroyInfo(context);
                        break;
                    case "GetCategroyInfo":
                        GetCategroyInfo(context);
                        break;
                    case "GetMappingRoomsByDate":
                        GetMappingRoomsByDate(context);
                        break;
                    case "GetMappingRateCodeByRoomtype":
                        GetMappingRateCodeByRoomtype(context);
                        break;
                    case "GetUseTicketRateCodeByRmtype"://获取票券可订房接口
                        GetUseTicketRateCodeByRmtype(context);
                        break;
                    case "GetActivityUrl":
                        GetActivityUrl(context);
                        break;
                    case "UpdateRemark":
                        UpdateRemark(context);
                        break;
                    case "GetServerTime":
                        GetServerTime(context);
                        break;
                }
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
        }

        private void GetServerTime(HttpContext context)
        {
            jsonResult.code = "200";
            jsonResult.data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void UpdateRemark(HttpContext context)
        {
            throw new NotImplementedException();
        }

        private void GetActivityUrl(HttpContext context)
        {
            yudingEntities db = new yudingEntities();
            var data = db.payurls.Where(x => x.hotelcode == hotelcode).ToList();
            jsonResult.code = "200";
            jsonResult.data = data;

        }

        private void GetUseTicketRateCodeByRmtype(HttpContext context)
        {

            var formulaid =int.Parse( Getpost("formulaid"));
            var categoryid =int.Parse(Getpost("categoryid"));
            var channel = Getpost("channel");
            var date = Getpost("date");
            var type = int.Parse(Getpost("type"));
            var roomtype = Getpost("roomtype");
            var time = Getpost("starttime");
            var end = Getpost("endtime");
            var db = new yudingEntities();
            var week = (int)DateTime.Parse(time).DayOfWeek;
            var pricelist = BookingWeb.GetRateCodexzByRoomtype(db, time, end, roomtype, hotelcode, type);
            var formulalist = db.price_formulaid.Where(x => x.hotelid == hotelcode && x.categoryid == categoryid && x.formulaid == formulaid&&x.type==channel&&x.state=="1").ToList();

            var query = (from a in pricelist join b in formulalist on a.xzcode equals b.xz_code select a).ToList();
         
            jsonResult.code = "200";
            jsonResult.msg = "成功";
            jsonResult.data = query;
        }

        private void GetMappingRateCodeByRoomtype(HttpContext context)
        {
            var mappingHotelCode = Getpost("mappingHotelCode");
            var date = Getpost("date");
            var type = int.Parse(Getpost("type"));
            var postData = "hotelcode=" + hotelcode + "&mappingHotelCode=" + mappingHotelCode;
            string URL = "https://interface.hanibabyppo.com/WxAPI/API/Config/HotelMappingApi.ashx?action=GetMappingHotel";
            var result = HttpHepler.SendPost(Config.WxAPIUrl + "/API/Config/HotelMappingApi.ashx?action=GetMappingHotel", postData);
            var obj = JsonConvert.DeserializeObject<HotelMappingResult>(result);
            var roomtype = Getpost("roomtype");
            var time = Getpost("starttime");
            var end = Getpost("endtime");
            var db = new yudingEntities();
            var week = (int)DateTime.Parse(time).DayOfWeek;
            var pricelist = BookingWeb.GetRateCodexzByRoomtype(db, time, end, roomtype, hotelcode, type);

            var query = (from a in pricelist join b in obj.data[0].roomRateCodes.Split(',') on a.xzcode equals b.ToString() select a).ToList();
            jsonResult.code = "200";
            jsonResult.msg = "成功";
            jsonResult.data = query;
        }

        private void GetMappingRoomsByDate(HttpContext context)
        {
            var mappingHotelCode = Getpost("mappingHotelCode");
            var date = Getpost("date");
            var type =int.Parse( Getpost("type"));
            var postData = "hotelcode="+hotelcode+"&mappingHotelCode=" + mappingHotelCode;
            string URL = "https://interface.hanibabyppo.com/WxAPI/API/Config/HotelMappingApi.ashx?action=GetMappingHotel";
            var result = HttpHepler.SendPost(Config.WxAPIUrl + "/API/Config/HotelMappingApi.ashx?action=GetMappingHotel", postData);
            var obj = JsonConvert.DeserializeObject<HotelMappingResult>(result);
            var list = BookingWeb.GetRoomsByDate(date,type,hotelcode);
            var hotel = obj.data[0].roomTypeCodes.Split(',');
            //房型查询
            var q = (from a in list
                     join b in hotel on a.roomtype equals b.ToString()
                     select a).ToList();
            jsonResult.code = "200";
            jsonResult.msg = "成功";
            jsonResult.data = q;
        }
         
        private void GetCategroyInfo(HttpContext context)
        {
            yudingEntities db = new yudingEntities();
            var categroyid =context. Request.Form["categroyid"];
            var hotelcode =context. Request.Form["hotelcode"];
            var cid = GetCid(categroyid, hotelcode);
            if (cid != "")
            {
                var info = db.categroyinfoes.FirstOrDefault(x => x.cid == cid);
                if (info != null)
                {
                    var rulelist = (from q in db.categroyrules
                                    where q.cid == cid
                                    select new specialtime
                                    {
                                        time = q.time
                                    }).ToArray();
                    var result = new CategroyData();
                    result.formulaid = info.formulaid;
                    result.hotelcode = info.hotelcode;
                    result.starttime = info.starttime.Value;
                    result.endtime = info.endtime.Value;
                    result.weekend = info.weekend.Value;
                    result.live = info.live.Value;

                    result.categroyid = info.categroyid;
                    result.specialtime = rulelist;
                    //result.specialtime = rulelist;

                    jsonResult.code = "200";
                    jsonResult.msg = "成功";
                    jsonResult.data = result;
                }
                else
                {
                    jsonResult.code = "201";
                    jsonResult.msg = "木有数据";
                }
            }
        }

        private void SetCategroyInfo(HttpContext context)
        {
            yudingEntities db = new yudingEntities();
            var str = context.Request.Form["postData"];
            var data = JsonConvert.DeserializeObject<SetgroyData>(str);
            var cid = GetCid(data.categroyid,data.hotelcode);
            var categroy = db.categroyinfoes.FirstOrDefault(x=>x.cid==cid);
            if (categroy == null)
            {
                db.categroyinfoes.Add(new categroyinfo
                {
                    categroyid = data.categroyid,
                    cid = data.hotelcode + "_" + data.categroyid,
                    formulaid = data.formulaid,
                   starttime=data.starttime,
                   endtime=data.endtime,
                   hotelcode=data.hotelcode,
                   live=data.live,
                   weekend=data.weekend
                });
            }
            else
            {
                categroy.live = data.live;
                categroy.weekend = data.weekend;
                categroy.starttime = data.starttime;
                categroy.endtime = data.endtime;
               // categroy.formulaid = data.formulaid;
            }
            var rulelist = db.categroyrules.Where(x => x.cid == cid).ToList();
            if (rulelist.Count > 0)
            {
                foreach (var item in rulelist)
                {
                    db.categroyrules.Remove(item);
                }
                db.SaveChanges();
            }
            if (data.specialtime.Count() > 0)
            {
                foreach (var item in data.specialtime)
                {
                    db.categroyrules.Add(new categroyrule
                    {
                        cid = cid,
                        time = item
                    });
                }
            }

            //if (rulelist != null)
            //{
            //    if (rulelist.Count() > 0)
            //    {
            //        foreach (var item in rulelist)
            //        {
            //            db.categroyrules.Remove(item);
            //        }
            //        db.SaveChanges();

            //        foreach (var item in data.specialtime)
            //        {
            //            db.categroyrules.Add(new categroyrule
            //            {
            //                cid = cid,
            //                time = item
            //            });
            //        }
            //    }
            //}
            db.SaveChanges();
            jsonResult.code = "200";
            jsonResult.msg = "成功";
        }

        private string GetCid(string categroyid, string hotelcode)
        {
            if (!string.IsNullOrEmpty(categroyid) && !string.IsNullOrEmpty(hotelcode))
            {
                return hotelcode + "_" + categroyid;
            }
            return "";
        }
        private void AddActivityUrl(HttpContext context)
        {
            var url = Getpost("url");
            var remark = Getpost("remark");

            var id =int.Parse( Getpost("id"));
            using (var db = new yudingEntities())
            {

                var data = db.payurls.FirstOrDefault(x => x.id == id);
                if (data==null)
                {
                    db.payurls.Add(new payurl { hotelcode = hotelcode, url = url ,remark=remark,flag=1});
                    db.SaveChanges();     
                    jsonResult.code = "200";
                    jsonResult.msg = "添加支付成功绑定活动链接成功";
                }
                else
                {
                    //var idparse = int.Parse(id);
                    var flag = Getpost("flag");
                    if (flag != null)
                    {
                        data.flag = int.Parse(flag);
                    }
                    data.remark = remark;
                    data.url = url;
                    db.SaveChanges();
                    jsonResult.code = "200";
                    jsonResult.msg = "修改支付成功绑定活动链接成功";
                }
            }
        }

        private void GetRoomAvail(HttpContext context)
        {
            string URL = Config.WxAPIUrl+"/API/GetWX.ashx?action=GetGZHxx";
            var postData = "hotelcode=" + hotelcode;
            var result = HttpHepler.SendPost(URL, postData);
            HotelInfoJson info = JsonConvert.DeserializeObject<HotelInfoJson>(result);
            var arr = HttpHepler.GetPost("arr");
            var dep = HttpHepler.GetPost("dep");
            var roomtype = HttpHepler.GetPost("roomtype");
            if (info.data[0].LvyunHotelgroupId != "" && info.data[0].LvyunHotelgroupId != null)
            {
                var data = info.data[0];
                var room = PmsAPI.GetRoomAvail(data.orderUrl, data.LvyunHotelgroupId, data.LvyunHotelId, arr, dep, roomtype);
                jsonResult.code = "200";
                jsonResult.data = room;
            }
            else
            {
                jsonResult.code = "201";
                jsonResult.msg = "不是绿云酒店";
            }
        }

        private void RepoRate(HttpContext context)
        {
            var starttime = DateTime.Parse(Getpost("starttime"));
            var endtime = DateTime.Parse(Getpost("endtime"));
            var type = int.Parse(Getpost("type"));
            var db = new yudingEntities();
            var list = db.order_t.Where(x =>x.type==type&& x.hotelcode == hotelcode && x.addtime >= starttime && x.addtime <= endtime).GroupBy(x => x.bosscard).ToList();
            int people = 0;
            foreach (var item in list)
            {
                if (item.Count() > 1)
                {
                    people += 1;
                }
            }
            float percent = (float)people / (float)list.Count;
            jsonResult.code = "200";
            jsonResult.data =percent>0?percent.ToString("p"):"0%";
        }
        private void GetLog(HttpContext context)
        {
           
            var page =int.Parse( context.Request.Form["page"]);
            var row = int.Parse(Getpost("row"));
            using (var db = new yudingEntities())
            {
                var q = (from a in db.newbacks where a.hotelid == hotelcode select a).OrderByDescending(x=>x.id).ToList();
                if (q.Count > 0)
                {
                    var totalcount = q.ToList().Count.ToString();
                    jsonResult.data = q.Skip((page - 1) * row).Take(rows).OrderByDescending(x=>x.addtime);
                    jsonResult.msg = totalcount;
                    jsonResult.code = "200";
                }
                else
                {
                    jsonResult.code = "502";
                    jsonResult.msg = "没有该实体数据";
                }
            }
        }
        private void CrsCancelOrder(HttpContext context)
        {
            var id = int.Parse(Getpost("id"));
            yudingEntities db = new yudingEntities();
            var data = db.order_t.FirstOrDefault(x => x.id == id);
            if (data != null)
            {
                data.state = "X";
                db.SaveChanges();
                if (data.lvorder != null)
                {
                    var q = new
                    {
                        hotelGroupCode = data.sourceid,
                        hotelCode = data.hotelcode,
                        gcRsvNo = data.lvorder,
                        otaChannel = "CRS"
                    };
                    var order = CrsAPI.QueryOrder(url, q);
                    if (order.resultCode == 0)
                    {
                        var d = new
                        {
                            srcHotelGroupCode = order.resultInfo.srcHotelGroupCode,
                            hotelGroupCode = order.resultInfo.hotelGroupCode,
                            hotelCode = order.resultInfo.hotelCode,
                            otaChannel = "CRS",
                            operationType = "Cancel",
                            otaRsvNo = order.resultInfo.otaRsvNo,
                            productCode = order.resultInfo.productCode,
                            cancelOrderInfoRQ = new
                            {
                                gcRsvNo = order.resultInfo.gcRsvNo
                            }
                        };
                        var result = CrsAPI.CancelOrder(url, d);
                        jsonResult.code = "200";
                        jsonResult.data = result;
                    }
                }
                else
                {
                    jsonResult.code = "200";
                }
            }
        }
        public static string url = "http://gds.ipms.cn";
        private void CrsUpdateOrder(HttpContext context)
        {
            var json = Getpost("postData");
            var postData = JsonConvert.DeserializeObject<UpdateOrderData>(json);
            var result = CrsAPI.PlaceOrder(url, postData);
            if (result.resultCode == 0)
            {
                yudingEntities db = new yudingEntities();
                var ordernumber = postData.bookOrderInfoRQ.gcRsvNo;
                var remark = postData.bookOrderInfoRQ.remark;
                var data = db.order_t.Where(x => x.ordernumber == ordernumber).FirstOrDefault();
                data.notes += remark;
                db.SaveChanges();
            }
            jsonResult.code = "200";
            jsonResult.data = result;
        }
        private void CrsPlaceOrder(HttpContext context)
        {
            var json = Getpost("postData");
            var postData = JsonConvert.DeserializeObject<PlaceOrderData>(json);
            var result = CrsAPI.PlaceOrder(url, postData);
            if (result.resultCode == 0)
            {
                var bosscard = Getpost("bosscard");
                var formula = Getpost("formula");
                var pay = Getpost("pay");
                var xz_name = Getpost("xz_name");
                var xz_code = Getpost("xz_code");
                var baojia = Getpost("baojia");
                var openid = Getpost("openid");
                var channelid = Getpost("channelid");
                var type = Getpost("type");
                var ticketsn = Getpost("ticketsn");
                var fmoney = Getpost("fmoney");
                var catgoryid = Getpost("catgoryid");
                var roomname = Getpost("roomname");
                var hotelname = Getpost("hotelname");
                var city = Getpost("city");
                yudingEntities db = new yudingEntities();
                SaveCrsOrder(postData.hotelCode, postData.hotelGroupCode, "", postData.bookOrderInfoRQ.arr, postData.bookOrderInfoRQ.dep, hotelname, city, postData.bookOrderInfoRQ.rsvMan, postData.bookOrderInfoRQ.everyDayPrices.Count.ToString(), roomname, postData.bookOrderInfoRQ.mobile, postData.bookOrderInfoRQ.remark, result.resultInfo, postData.bookOrderInfoRQ.rateSum.ToString(), postData.bookOrderInfoRQ.rateSum.ToString(), bosscard, formula, pay, xz_name, xz_code, baojia, openid, channelid, result.resultInfo, type, ticketsn, fmoney, catgoryid, db);

            }
            jsonResult.code = "200";
            jsonResult.data = result;
        }
        private void SaveCrsOrder(string hotelcode, string hotelgroupcode, string roomtype, string arr, string dep, string hotelname, string City, string name, string count, string roomname, string mobile, string remark, string ordernumber, string truerate, string rate, string resby, string formula, string pay, string xz_name, string xz_code, string baojia, string guestname, string channelid, string lvorder, string type, string TicketSn, string Fmoney, string categoryid, yudingEntities db)
        {
            var order = new order_t()
            {
                sourceid = hotelgroupcode,
                Fmoney = decimal.Parse(Fmoney),
                TicketSn = TicketSn,
                lvorder = lvorder,
                isSendMsg = "0",
                hotelname = hotelname,
                City = City,
                channelid = channelid,
                guestname = guestname,
                arrivetime = DateTime.Parse(arr),
                baojia = baojia,
                bosscard = resby,
                contact_mobile = mobile,
                contact_name = name,
                count = int.Parse(count),
                hotelcode = hotelcode,
                leavetime = DateTime.Parse(dep),
                ordernumber = ordernumber,
                pay = pay,
                yuanjia = rate.ToString(),
                trueRate = truerate,
                notes = remark,
                roomname = roomname,
                roomtype = roomtype,
                xz_code = xz_code,
                xz_name = xz_name,
                //sessionid = sessionid,
                formulaid = formula,
                categoryid = categoryid,
                // increasemoney = decimal.Parse(increasemoney),
                //yhmoney = decimal.Parse(yhmoney),
                rate = decimal.Parse(rate),
                //  tpid = tpid,
                type = int.Parse(type),
            };
            db.order_t.Add(order);
            db.SaveChanges();
        }
        private void CrsCheckOrder(HttpContext context)
        {
            var json = Getpost("postData");
            var postData = JsonConvert.DeserializeObject<CheckOrderData>(json);
            var result = CrsAPI.CheckOrder(url, postData);
            jsonResult.code = "200";
            jsonResult.data = result;

        }
        private void GetProductDeatil(HttpContext context)
        {
            var json = Getpost("postData");
            var postData = JsonConvert.DeserializeObject<QueryProductDetail>(json);
            var result = CrsAPI.GetproductBaseDetail(url, postData);
            jsonResult.code = "200";
            jsonResult.data = result;
        }
        private void GetCrsProductList(HttpContext context)
        {

            var hotelGroupCode = Getpost("hotelGroupCode");
            var hotelCode = Getpost("hotelCode");
            var queryProduct = new
            {
                hotelGroupCode = hotelGroupCode,
                hotelCodes = new List<string>()
                {
                    hotelCode
                },

                otaChannel = "CRS"
            };
            var result = CrsAPI.GetProductList(url, queryProduct);
            if (result.resultCode == 0)
            {
                jsonResult.code = "200";
                jsonResult.data = result;
            }
            else
            {
                jsonResult.code = "201";
            }
        }
        private void GetCrsHotelList(HttpContext context)
        {
            var json = Getpost("postData");
            var postData = JsonConvert.DeserializeObject<QueryHotelInfo>(json);
            var result = CrsAPI.GetHotelInfo(url, postData);
            var list = new Ks_Crs2._0_SDK.ResponseModel.HotelListResult()
            {
                resultInfos = new List<GcHotelDefaultRSDto>(),
            };


            foreach (var item in result.resultInfos)
            {
                if (item.hotelGroupCode == postData.hotelGroupCode)
                {
                    list.resultInfos.Add(item);
                }
            }
            jsonResult.code = "200";
            jsonResult.data = list;
        }
        private void OrderService(HttpContext context)
        {
            var sessionid = Getpost("sessionid");
            var invoice = Getpost("invoice");
            var payway = Getpost("payway");
            var transaction_id = Getpost("transaction_id");
            var transaction_ali_id = Getpost("transaction_ali_id");
            //Thread Thread1 = new Thread(new ParameterizedThreadStart(OrderServiceThread));
            var obj = new PayOverJson { Sessionid = sessionid, InvoiceId = invoice, PayWay = payway, Transaction_id = transaction_id, Transaction_Ali_id = transaction_ali_id };
            OrderServiceThread(obj);
            jsonResult.code = "200";
            jsonResult.msg = "成功";
        }
        private void OrderServiceThread(object obj)
        {
            var data = obj as PayOverJson;
            if (data != null)
            {
                yudingEntities db = new yudingEntities();
                var orderlist = db.order_t.Where(x => x.sessionid == data.Sessionid).ToList();
                foreach (var item in orderlist)
                {
                    if (data.PayWay == "WX")
                    {
                        //查询支付订单号
                        var postData = "hotelcode=" + hotelcode + "&trade_no=" + data.Sessionid + "&Notify=";
                        var Transaction_id_str = HttpHepler.SendPost(Config.CheckWxPayUrl, postData);
                        var Transaction_id = JsonHelper.GetJsonValue(Transaction_id_str, "transaction_id");
                        if (Transaction_id != "" && Transaction_id != "null" && Transaction_id!=null)
                        {
                            var hotel = db.hotel_list.FirstOrDefault(x => x.hotelId == item.hotelcode);
                            if (item.lvorder != null)
                            {
                                //if (hotel.pms == "3")
                                //{
                                //    var mp = new MPDBEntities();
                                //    var hotel = mp.MPConfigs.FirstOrDefault(x => x.ShopCode == item.hotelcode);
                                //    var hotel1 = db.hotel_list.FirstOrDefault(x => x.hotelId == item.hotelcode);
                                //    // 平台提供的地址
                                //    string url = hotel.orderUrl;
                                //    // 平台appkey
                                //    string appKey = "KSKJ";
                                //    // 平台授权码
                                //    string secret = hotel1.pwd;
                                //    // 使用哪个酒店的身份接入 
                                //    string hotelId = "G000001";
                                //    var client = new XopClient(url, appKey, secret, hotelId);
                                //    // 开始登录平台
                                //    var rspLogin = client.login();

                                //    if (!XopClient.isResponseSuccess(rspLogin))
                                //    {
                                //        //return false;
                                //    }
                                //}
                                if (hotel.pms == "1")
                                {
                                    ly ly = new ly("http://115.159.81.168:8100/ipmsgroup/router", hotelcode);
                                    Dictionary<string, string> parms = new Dictionary<string, string>();
                                    parms.Add("crsNo", item.lvorder);
                                    parms.Add("remark", "[已支付]微信支付,微信支付单号：" + Transaction_id == null ? "" :Transaction_id);
                                    //order.notes = order.notes + remark;
                                    ly.Change(parms, "http://115.159.81.168:8100/ipmsgroup/CRS/appendResrvBaseRemark");
                                }

                            }
                            item.notes += "[已支付]微信支付,微信支付单号：" + Transaction_id == null ? "" : Transaction_id;
                        }
                       

                      
                        var url = db.payurls.FirstOrDefault(x => x.hotelcode == hotelcode&&x.flag==0);
                        var urldata = "";
                        var remark = "";
                        if (url != null)
                        {
                            if (url.url != null && url.url != "")
                            {
                                urldata = System.Web.HttpUtility.UrlEncode(url.url);
                            }
                            if (url.remark != null && url.remark != "")
                            {
                                remark = url.remark;
                            }

                        }
                        var paramData = new
                        {
                            first = new
                            {
                                value = "您好，您的微信支付已成功\n预订人：" + item.contact_name + "\n联系方式：" + item.contact_mobile,
                                color = "#1C1C1C",
                            },
                            keyword1 = new
                            {
                                value = data.Sessionid,
                                color = "#1C1C1C",
                            },
                            keyword2 = new
                            {
                                value =( item.rate * item.count - item.yhmoney).Value.ToString("#0.00"),
                                color = "#1C1C1C",
                            },
                            keyword3 = new
                            {
                                value = item.hotelname,
                                color = "#1C1C1C",
                            },
                            keyword4 = new
                            {
                                value = DateTime.Now.ToString("yyyy-MM-dd"),
                                color = "##1C1C1C",
                            },
                            remark = new
                            {
                                value =remark,
                                color = "#FF3030",
                            },
                        };
                        var json = JsonConvert.SerializeObject(paramData);
                        var openidList = db.openids.Where(x => x.hotelid == item.hotelcode && x.type == 0).ToList();
                        if (openidList != null)
                        {
                            if (openidList.Count > 0)
                            {
                                foreach (var openid in openidList)
                                {
                                    var postStr = "hotelcode=" + item.hotelcode + "&openid=" + openid.openid1 + "&param=" + json + "&templateName=PayNotify";
                                    var r =HttpHepler.SendPost(Config.SendTemplateUrl, postStr);
                                }
                            }
                        }
                        var TemplateData = "hotelcode=" + item.hotelcode + "&openid=" + item.guestname + "&param=" + json + "&templateName=PayNotify&url="+urldata;
                        var result = HttpHepler.SendPost(Config.SendTemplateUrl, TemplateData);
                    }
                    if (data.PayWay == "ZFB")
                    {
                          if (item.lvorder != null)
                            {
                                ly ly = new ly("http://115.159.81.168:8100/ipmsgroup/router", hotelcode);
                                Dictionary<string, string> parms = new Dictionary<string, string>();
                                parms.Add("crsNo", item.lvorder);
                                parms.Add("remark",  "[已支付]支付宝支付,支付宝支付单号：" + data.Transaction_Ali_id);
                                //order.notes = order.notes + remark;
                                var result = ly.Change(parms, "http://115.159.81.168:8100/ipmsgroup/CRS/appendResrvBaseRemark");
                            }
                        item.notes = "[已支付]支付宝支付,支付宝支付单号：" + data.Transaction_Ali_id;
                    }
                    //查询发票
                    if (data.InvoiceId != "")
                    {
                        int InvoiceId = int.Parse(data.InvoiceId);
                        var InvoiceData = db.invoices.Where(x => x.id == InvoiceId).FirstOrDefault();
                        if (InvoiceData.type == 0)
                        {
                            item.notes += "  普通发票信息(企业名称/个人:" + InvoiceData.EnterpriseName + ",纳税人识别号:" + InvoiceData.Taxpayer + ",统一社会信用代码:" + InvoiceData.CreditCode + ",注册地址:" + InvoiceData.address + ",联系电话:" + InvoiceData.ContactNumber + ",开户行:" + InvoiceData.OpeningBank + ",开户行帐号:" + InvoiceData.BankNumber + ",发票明细:" + InvoiceData.InvoiceDetails + ",领取方式:" + InvoiceData.Payment + ")";

                        }
                        if (InvoiceData.type == 1)
                        {
                            item.notes += "  普通发票信息(企业名称/个人:" + InvoiceData.EnterpriseName + ",纳税人识别号:" + InvoiceData.Taxpayer + ",发票明细:" + InvoiceData.InvoiceDetails + ",领取方式:" + InvoiceData.Payment + ")";
                        }
                    }
                    //查包价，发产品
                    if (item.baojia != "")
                    {
                        net.kuaishun.ticketmk.Service service = new net.kuaishun.ticketmk.Service();
                        var postData = "hotelcode=" + hotelcode;
                        string info = HttpHepler.SendPost(Config.WxAPIUrl+"/API/GetWX.ashx?action=GetGZHxx", postData);
                        var hotel = JsonConvert.DeserializeObject<HotelInfoJson>(info).data[0];
                        var result = service.Set_newhy_new_json(hotel.YQTOperatorId, hotel.YQTOperatorId, hotelcode, item.baojia, item.contact_mobile, hotelcode);

                    }
                    //改状态
                    item.ispay = 1;
                }
                db.SaveChanges();
            }
        }
        private void GetYFOrder(HttpContext context)
        {
            yudingEntities db = new yudingEntities();
            var time = DateTime.Parse(Getpost("starttime"));
            var end = DateTime.Parse(Getpost("endtime"));
            var list = db.order_t.Where(x => x.addtime >= time && x.addtime <= end && x.hotelcode == hotelcode && x.state != "X" && x.ispay == 1).GroupBy(x => x.bosscard).ToList();
            var result = new List<object>();
            foreach (var item in list)
            {
                Nullable<decimal> tmoney = 0;
                foreach (var item1 in item)
                {
                    tmoney += item1.count * item1.rate;
                }
                result.Add(new
                {
                    bosscard = item.FirstOrDefault().bosscard,
                    contact_name = item.FirstOrDefault().contact_name,
                    contact_mobile = item.FirstOrDefault().contact_mobile,
                    num = item.GroupBy(x => x.sessionid).Count(),
                    roomnum = item.Sum(x => x.count),
                    tmoney = tmoney,
                });
            }
            jsonResult.code = "200";
            jsonResult.msg = "查询成功";
            jsonResult.data = result;
        }
        private void CrsGetRateCodexzByRoomtype(HttpContext context)
        {
            var roomtype = Getpost("roomtype");
            var time = Getpost("starttime");
            var end = Getpost("endtime");
            var type = int.Parse(Getpost("type"));

            var week = (int)DateTime.Parse(time).DayOfWeek;
            var list = bll.CrsGetRateCodexzByRoomtype(time, end, roomtype, hotelcode, type);
            jsonResult.code = "200";
            jsonResult.msg = "";
            jsonResult.data = list;
        }
        private void SaveCrsOrder(HttpContext context)
        {
            try
            {
                var roomtype = Getpost("roomtype");
                var arr = Getpost("arr");
                var dep = Getpost("dep");
                var hotelname = Getpost("hotelname");
                var City = Getpost("City");
                var name = Getpost("name");
                var count = Getpost("count");
                var roomname = Getpost("roomname");
              //  var increasemoney = Getpost("increasemoney");
                var mobile = Getpost("mobile");
                var remark = Getpost("remark");
                var ordernumber = Getpost("ordernumber");
                var truerate = Getpost("truerate");
                var rate = Getpost("rate");
                var resby = Getpost("resby");
                var formula = Getpost("formula");
                var pay = Getpost("pay");
                var payway = Getpost("payway");
                var xz_name = Getpost("xz_name");
                var xz_code = Getpost("xz_code");
                var baojia = Getpost("baojia");
                var guestname = Getpost("guestname");
                var channelid = Getpost("channelid");
                var activty = Getpost("activty");
                var lvorder = Getpost("lvorder");
                var type = Getpost("type");
                var tpid = Getpost("tpid");
                var TicketSn = Getpost("TicketSn");
                var Fmoney = Getpost("Fmoney");
             //   var yhmoney = Getpost("yhmoney");
                var categoryid = Getpost("categoryid");
                using (var db = new yudingEntities())
                {
                    var data = db.order_t.Where(x => x.lvorder == lvorder).FirstOrDefault();
                    if (data == null)
                    {
                        var order = new order_t()
                        {
                            Fmoney=decimal.Parse(Fmoney),
                            TicketSn=TicketSn,
                            lvorder=lvorder,
                            isSendMsg = "0",
                            hotelname = hotelname,
                            City = City,
                            channelid = channelid,
                            guestname = guestname,
                            arrivetime = DateTime.Parse(arr),
                            baojia = baojia,
                            bosscard = resby,
                            contact_mobile = mobile,
                            contact_name = name,
                            count = int.Parse(count),
                            hotelcode = hotelcode,
                            leavetime = DateTime.Parse(dep),
                            ordernumber = ordernumber,
                            pay = pay,
                            yuanjia = rate.ToString(),
                            trueRate = truerate,
                            notes = remark,
                            roomname = roomname,
                            roomtype = roomtype,
                            xz_code = xz_code,
                            xz_name = xz_name,
                            //sessionid = sessionid,
                            formulaid = formula,
                            categoryid = categoryid,
                           // increasemoney = decimal.Parse(increasemoney),
                            //yhmoney = decimal.Parse(yhmoney),
                            rate = decimal.Parse(rate),
                          //  tpid = tpid,
                            type = int.Parse(type),
                        };
                        db.order_t.Add(order);
                        db.SaveChanges();
                        jsonResult.code = "200";
                        jsonResult.msg = "添加成功";

                    }
                    else
                    {
                        jsonResult.code = "201";
                        jsonResult.msg = "已存在";
                    }
                }
            }
            catch (Exception ex)
            {

                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
            }

        }
        private void TEST(HttpContext context)
        {
            using (var db = new yudingEntities())
            {
                var data = db.hotel_list.OrderBy(x=>x.hotelId).ToList();
                jsonResult.code = "200";
                jsonResult.msg = "";
                jsonResult.data = data;
            }
        }
        private void GetCrsOrderInfo(HttpContext context)
        {
            try
            {
                var hotelgroupocode = Getpost("groupcode");
                var ordernum = Getpost("ordernum");
                var post = new
                {
                    hotelGroupCode = hotelgroupocode,
                    clientChannel = "CRS",
                    hotelCode = hotelcode,
                    gcRsvNo = ordernum,
                };
                var json = JsonConvert.SerializeObject(post);
                var result = HttpHepler.SendPostJson("http://gds.ipms.cn/int/Crs/otaOrderDetailService", json);
                var orderInfo = JsonConvert.DeserializeObject<CrsOrderJson>(result);
                if (orderInfo.resultCode == 0)
                {
                    var order = new
                    {
                        hotelCode = orderInfo.resultInfo.hotelCode,
                        hotelGroupCode = hotelgroupocode,
                        hotelName = orderInfo.resultInfo.hotelDesc,
                        otaChannel = orderInfo.resultInfo.otaChannel,
                        gcRsvNo = orderInfo.resultInfo.gcRsvNo,
                        crsNo = orderInfo.resultInfo.crsNo,
                        rsvNo = orderInfo.resultInfo.rsvNo,
                        rsvMan = orderInfo.resultInfo.rsvMan,
                        mobile = orderInfo.resultInfo.mobile,
                        rmDesc = orderInfo.resultInfo.rmDesc,
                        rmtype = orderInfo.resultInfo.rmtype,
                        rmnum = orderInfo.resultInfo.rmnum,
                        arrStr = orderInfo.resultInfo.arrStr,
                        depStr = orderInfo.resultInfo.depStr,
                        arr = orderInfo.resultInfo.arr,
                        dep = orderInfo.resultInfo.dep,
                        rateSum = orderInfo.resultInfo.rateSum,
                    };
                    jsonResult.code = "200";
                    jsonResult.data = order;
                    jsonResult.msg = "查询成功";
                }
                else
                {
                    jsonResult.code = "404";
                    jsonResult.msg = orderInfo.resultMessage;
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
            }
        }
        private void GetHotelListByGroupCode(HttpContext context)
        {
            try
            {
                var groupcode = Getpost("groupcode");
                var post = new
                {
                    hotelGroupCode = groupcode,
                    clientChannel = "CRS",
                    fromDate = DateTime.Now.AddDays(-1),
                    toDate = DateTime.Now,
                    groupFirst=true,
                    page=1,
                    pageSize=100,
                    otaChannel = "WEB"
                };
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-ddTHH:mm:ss";
                var json = JsonConvert.SerializeObject(post, Formatting.Indented, timeFormat);
                var result = HttpHepler.SendPostJson("http://gds.ipms.cn/int/Crs/hotels", json);
                var hotelInfo = JsonConvert.DeserializeObject<yuding.JsonResult.HotelListResult>(result);
                if (hotelInfo.resultCode == 0)
                {
                    var list = new List<object>();
                    foreach (var item in hotelInfo.resultInfos)
                    {
                        if (item.hotelGroupCode == groupcode)
                        {
                            var hotel = new
                            {
                                hotelcode = item.hotelCode,
                                hotelname = item.gcHotel.descript,
                            };
                            list.Add(hotel);
                        }
                    }
                    jsonResult.code = "200";
                    jsonResult.data = list;
                    jsonResult.msg = "查询成功";
                }
                else
                {
                    jsonResult.code = "404";
                    jsonResult.msg = hotelInfo.resultMessage;
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
            }
        }
        private void UpdateOrderYh(HttpContext context)
        {
            var ordernum = Getpost("ordernum");
            var yhmoney = Getpost("yhmoney");
            using (var db = new yudingEntities()) 
            {
                var data = db.order_t.Where(x => x.ordernumber == ordernum&&x.hotelcode==hotelcode).FirstOrDefault();
                data.yhmoney = decimal.Parse(yhmoney);
                db.SaveChanges();
                jsonResult.code = "200";
                jsonResult.msg = "修改成功";
                
            }
        }
        private void GetHotelGroupList(HttpContext context)
        {
            using (var db = new yudingEntities())
            {
                var data = db.hotel_list.GroupBy(x => x.HotelGroupCode).ToList();
                var list = new List<object>();
                foreach (var item in data)
                {
                    if(item.FirstOrDefault().HotelGroupCode!="")
                    list.Add(item.FirstOrDefault());
                }
                jsonResult.code = "200";
                jsonResult.data = list;
                jsonResult.msg = "查询成功";
            }
        }
        private void SearchHotelByGroupCode(HttpContext context)
        {
            var hotelname = Getpost("hotelGroupCode");
            using (var db = new yudingEntities())
            {
                var data = db.hotel_list.Where(x => x.HotelGroupCode == hotelname).ToList();
                jsonResult.code = "200";
                jsonResult.msg = "查询成功";
                jsonResult.data = data;
            }
        }
        private void GetHotelGroupAll(HttpContext context)
        {
            throw new NotImplementedException();
        }
        private void CrsOrder(HttpContext context)
        {
            var orderdata = Getpost("orderdata");
            var increasedata = Getpost("increasedata");
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
                            var hotelname = db.hotel_list.Where(x => x.hotelId == item.hotelcode).FirstOrDefault();
                            item.hotelname = hotelname.hotelName;
                            var roomxz = db.rateroom_xz.Where(x => x.roomtype == item.roomtype).FirstOrDefault();
                            var newroom = db.ratecode_t.Where(x => x.ratecode == roomxz.ratecode).FirstOrDefault();
                            var date = DateTime.Parse(item.arr);
                            var every = db.everydate_price_t.Where(x => x.everydate == date && x.xz_code == item.xz_code).FirstOrDefault();
                            var room = db.newroom_t.Where(x => x.roomtype == item.roomtype).FirstOrDefault();
                            var hotel = db.hotel_list.FirstOrDefault(x => x.hotelId == item.hotelcode);
                            if (every.ordernum < every.num)
                            {

                                if (roomxz.qudao != "" && roomxz.pmsratecode != "")
                                {
                                    //绿云下单
                                    if (!PlaceOrder(db, item,hotel.pms, room.pms, newroom.pmsratecode, newroom.qudao, true))
                                    {
                                        isorder = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (newroom.qudao != "" && newroom.pmsratecode != "")
                                    {
                                        //绿云下单
                                        if (!PlaceOrder(db, item, hotel.pms, room.pms, newroom.pmsratecode, newroom.qudao, true))
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
        private void SearchHotelByCity(HttpContext context)
        {
            var hotelname = Getpost("City");
            using (var db = new yudingEntities())
            {
                var data = db.hotel_list.Where(x => x.City == hotelname).ToList();
                jsonResult.code = "200";
                jsonResult.msg = "查询成功";
                jsonResult.data = data;
            }
        }
        private void SearchHotel(HttpContext context)
        {
            var hotelname = Getpost("hotelname");
            using (var db = new yudingEntities())
            {
                var queryable = db.hotel_list.Where(x => SqlFunctions.PatIndex("%"+hotelname+"%", x.hotelName) > 0).ToList();
                //List<Entity1> entity1s = queryable.ToList();
                jsonResult.code = "200";
                jsonResult.msg = "查询成功";
                jsonResult.data = queryable;
            }
        }
        private void WxRoomList(HttpContext context)
        {
            throw new NotImplementedException();
        }
        private void UpdateState(HttpContext context)
        {
            var id =int.Parse( Getpost("id"));
            
            using (var db = new yudingEntities())
            {
                var data = db.invoices.Where(x => x.id == id).FirstOrDefault();
                
                var list = db.invoices.Where(x => x.bosscard == data.bosscard&&x.hotelid==data.hotelid).ToList();
                foreach (var item in list)
                {
                    item.state = 0;
                }
                data.state = 1;
                db.SaveChanges();
                jsonResult.code = "200";
                jsonResult.msg = "修改成功";
            }
        }
        private void GetInvoice(HttpContext context)
        {
            var id =Getpost("id");
            using (var db = new yudingEntities())
            {
                
                if (id != "")
                {
                    var idparse = int.Parse(id);
                    var data = db.invoices.Where(x => x.id == idparse).ToList();
                    jsonResult.code = "200";
                    jsonResult.data = data;
                    jsonResult.msg = "查询成功";
                }
                else
                {
                    var phone = Getpost("bosscard");
                    var data = db.invoices.Where(x => x.bosscard == phone&&x.hotelid==hotelcode).ToList();
                    jsonResult.code = "200";
                    jsonResult.data = data;
                    jsonResult.msg = "查询成功";
                }
                
            }
        }
        private void DelInvoice(HttpContext context)
        {
            var id =int.Parse( Getpost("id"));
            using (var db = new yudingEntities())
            {
                var data = db.invoices.Where(x => x.id == id).FirstOrDefault();
                db.invoices.Remove(data);
                db.SaveChanges();
                jsonResult.code = "200";
                jsonResult.msg = "删除成功";
            }
        }
        private void UpdateInvoice(HttpContext context)
        {
            using (var db = new yudingEntities())
            {
                var level = Getpost("level");
                var Drawer = Getpost("Drawer");
                var phone = Getpost("phone");
                var hotelname = Getpost("hotelname");
                var EnterpriseName = Getpost("EnterpriseName");
                var InvoiceDetails = Getpost("InvoiceDetails");
                var Payment = Getpost("Payment");
                var type = Getpost("type");
                var Taxpayer = Getpost("Taxpayer");
                var CreditCode = Getpost("CreditCode");
                var address = Getpost("address");
                var ContactNumber = Getpost("ContactNumber");
                var OpeningBank = Getpost("OpeningBank");
                var BankNumber = Getpost("BankNumber");
                var bosscard = Getpost("bosscard");
                var id = int.Parse(Getpost("id"));
                var data = db.invoices.Where(x => x.id == id).FirstOrDefault();
                if (type == "0")
                {
                    data.Drawer = Drawer;
                    data.phone = phone;
                    data.hotelid = hotelcode;
                    data.hotelname = hotelname;
                    data.EnterpriseName = EnterpriseName;
                    data.InvoiceDetails = InvoiceDetails;
                    data.Payment = Payment;
                    data.type = int.Parse(type);
                    data.Taxpayer = Taxpayer;
                    data.bosscard = bosscard;
                    data.BankNumber = BankNumber;
                }
                else
                {
 
                    data. Drawer = Drawer;
                       data. phone = phone;
                        data.hotelid = hotelcode;
                        data.hotelname = hotelname;
                        data.EnterpriseName = EnterpriseName;
                      data.  InvoiceDetails = InvoiceDetails;
                       data. Payment = Payment;
                       data. type = int.Parse(type);
                       data. Taxpayer = Taxpayer;
                       data. address=address;
                       data. ContactNumber=ContactNumber;
                       data. OpeningBank = OpeningBank;
                      data.  CreditCode = CreditCode;
                      data.bosscard = bosscard;
                      data.BankNumber = BankNumber;
                }
                db.SaveChanges();
                jsonResult.code = "200";
                jsonResult.msg = "修改成功";
            }
        }
        private void AddInvoice(HttpContext context)
        {
            
            var Drawer = Getpost("Drawer");
            var phone = Getpost("phone");
            var hotelname = Getpost("hotelname");
            var EnterpriseName = Getpost("EnterpriseName");
            var InvoiceDetails = Getpost("InvoiceDetails");
            var Payment = Getpost("Payment");
            var type = Getpost("type");
            var Taxpayer = Getpost("Taxpayer");
            var CreditCode = Getpost("CreditCode");
            var address = Getpost("address");
            var ContactNumber = Getpost("ContactNumber");
            var OpeningBank = Getpost("OpeningBank");
            var BankNumber = Getpost("BankNumber");
            var bosscard = Getpost("bosscard");
            
            using (var db = new yudingEntities())
            {
                if (type == "0")
                {
                    invoice invoice = new invoice()
                    {
                        Drawer = Drawer,
                        phone = phone,
                       BankNumber=BankNumber,
                       hotelname = hotelname,
                        EnterpriseName = EnterpriseName,
                        InvoiceDetails = InvoiceDetails,
                        Payment = Payment,
                        type = int.Parse(type),
                        Taxpayer = Taxpayer,
                        bosscard = bosscard,
                        hotelid = hotelcode,
                    };
                    db.invoices.Add(invoice);
                }
                else
                {
                    invoice invoice = new invoice()
                    {
                        BankNumber = BankNumber,
                        Drawer = Drawer,
                        phone = phone,
                        hotelid = hotelcode,
                        hotelname = hotelname,
                        EnterpriseName = EnterpriseName,
                        InvoiceDetails = InvoiceDetails,
                        Payment = Payment,
                        type = int.Parse(type),
                        Taxpayer = Taxpayer,
                        address=address,
                        ContactNumber=ContactNumber,
                        OpeningBank = OpeningBank,
                        CreditCode = CreditCode,
                        bosscard = bosscard,
                    };
                    db.invoices.Add(invoice);
                }
                db.SaveChanges();
                jsonResult.code = "200";
                jsonResult.data = "添加成功";
            }
        }
        private void GetOrder(HttpContext context)
        {
           
            var orderid = context.Request.Form["orderid"];
            var tel = context.Request.Form["tel"];
            var name = context.Request.Form["name"];
            var ispay = context.Request.Form["ispay"];
            var arrivetime = context.Request.Form["arrivetime"];
            var channelid = context.Request.Form["channelid"];
            var addtime = Getpost("addtime");
            using (var db = new yudingEntities())
            {
                var q = (from a in db.order_t where a.channelid == channelid select a);
                if (addtime != null&&addtime!="")
                {
                    var time = addtime.Split(',');
                    var t1 = DateTime.Parse(time[0]);
                    var t2 = DateTime.Parse(time[1]);
                    q = q.Where(x => x.addtime >= t1 && x.addtime <= t2);
                }
                if (orderid != null)
                {
                    q = q.Where(x => x.ordernumber == orderid);
                }
                if (tel != null)
                {
                    q = q.Where(x => x.contact_mobile == tel);
                }
                if (name != null)
                {
                    q = q.Where(x => x.guestname == name);
                }
                if (ispay != null)
                {
                    var ispayParse = int.Parse(ispay);
                    q = q.Where(x => x.ispay == ispayParse);
                }
                if (arrivetime != null)
                {
                    var time = arrivetime.Split(',');
                    var t1 = DateTime.Parse(time[0]);
                    var t2 = DateTime.Parse(time[1]);
                    q = q.Where(x => x.arrivetime >= t1 && x.arrivetime <= t2);
                }
                jsonResult.code = "200";
                jsonResult.msg = "查询成功";
                jsonResult.data = q.OrderByDescending(x=>x.addtime).ToList();
            }
        }
        private void GetOrderListByIsPay(HttpContext context)
        {
            var ispay = int.Parse(Getpost("ispay"));
            var type = int.Parse(Getpost("type"));
            using (var db = new yudingEntities())
            {
                var data = db.order_t.Where(x => x.ispay == ispay&&x.hotelcode==hotelcode && x.type==type).ToList();
                jsonResult.code = "200";
                jsonResult.msg = "获取成功";
                jsonResult.data = data;
 
            }
        }
        private void DelOpenId(HttpContext context)
        {
            var id =int.Parse( Getpost("id"));
            using (var db = new yudingEntities())
            {
                var data = db.openids.Where(x => x.id == id).FirstOrDefault();
                db.openids.Remove(data);
                db.SaveChanges();
                jsonResult.code = "200";
                jsonResult.msg = "删除成功";
            }
        }
        private void GetOrderBySessionid(HttpContext context)
        {
            var sessionlist = Getpost("sessionid").Split(',');
            
            using (var db = new yudingEntities())
            {
                var list = new List<object>();

                foreach (var item in sessionlist)
                {
                    var data = db.order_t.Where(x => x.sessionid == item).GroupBy(x=>x.sessionid==item);
                    foreach (var item1 in data)
                    {
                        var sess = item1.FirstOrDefault().sessionid;
                        var inc = db.increase_order.Where(x => x.sessionid == sess).ToList();
                        var ord =new List<object>();
                        foreach (var item2 in item1)
                        {
                            ord.Add(item2);
                        }
                        var s = new
                        {
                            order = ord,
                            increase = inc
                        };
                        list.Add(s);
                    }
                }
                jsonResult.code = "200";
                jsonResult.msg = "成功";
                jsonResult.data = list;

               
            }
        }
        private void GetSessionIdOrder(HttpContext context)
        {
            var sessionid = Getpost("sessionid");
            using (var db = new yudingEntities())
            {
                
                var data = db.order_t.Where(X => X.sessionid == sessionid).ToList();
                
                jsonResult.code = "200";
                jsonResult.msg = "成功";
                jsonResult.data = data;
            }
        }
        private void GetOrderListBySessionid(HttpContext context)
        {
            var type =int.Parse( Getpost("type"));
            var page = Getpost("page");
            var ispay = int.Parse(Getpost("ispay")!=""?Getpost("ispay"):"2");
            var addtime = Getpost("addtime");
            var list = new List<object>();
            using (var db = new yudingEntities())
            {
                    var q = (from a in db.order_t where a.hotelcode == hotelcode && a.type==type select a)
                        .GroupBy(x => new { x.sessionid })
                        .Select(group => new
                        {
                            ispay=group.FirstOrDefault().ispay,
                            sessionid = group.FirstOrDefault().sessionid,
                            //totalrate = group.FirstOrDefault().rate * group.FirstOrDefault().count,
                            totalrate = group.FirstOrDefault().rate * group.Sum(x => x.count),
                            yhmoney = group.Sum(x => x.yhmoney),
                            name = group.FirstOrDefault().contact_name,
                            addtime = group.FirstOrDefault().addtime,
                            count = group.Sum(x => x.count),
                            mobile = group.FirstOrDefault().contact_mobile,
                            arr = group.Min(x => x.arrivetime),
                            dep = group.Max(x => x.leavetime),
                            p1=group.ToList(),
                        });
                if (q.ToList().Count > 0)
                {
                    if (addtime != "")
                    {
                        var time = DateTime.Parse(addtime);
                        var time1 = time.AddDays(1);
                        q= q.Where(x => x.addtime >= time&&x.addtime<=time1);
                    }
                    if (ispay != 2)
                    {
                        q = q.Where(x => x.ispay == ispay);
                    }
                    var totalcount = q.ToList().Count.ToString();
                    int count = Convert.ToInt32(totalcount);
                    // totalcount = (count / rows + (count % rows > 0 ? 1 : 0)).ToString();
                    jsonResult.code = "200";
                    //json.pagecount = totalcount;
                    int pageindex = int.Parse(page);
                    jsonResult.data = q.OrderByDescending(x => x.addtime).Skip((pageindex - 1) * rows).Take(rows).ToList();
                    jsonResult.msg = totalcount;
                    //HttpHepler.ReturnJson<JsonReturnPagination>(json, context);
                }
                else
                {
                    jsonResult.code = "502";
                    jsonResult.msg = "无数据";
                }

            }
           
        }
        private void CancelOrder(HttpContext context)
        {
            var sessionid = Getpost("sessionid");
            using (var db = new yudingEntities())
            {
                var orderlist = db.order_t.Where(x => x.sessionid == sessionid).ToList();
                bool isCancel = true;
                
                foreach (var item in orderlist)
                {
                    var hotel = db.hotel_list.FirstOrDefault(x => x.hotelId == item.hotelcode);
                    if (hotel.pms == "1")
                    {
                        if (item.lvorder != "" && item.lvorder != null)
                        {
                            //绿云取消
                            ly ly = new ly("http://115.159.81.168:8100/ipmsgroup/router", hotelcode);
                            Dictionary<string, string> parms = new Dictionary<string, string>();
                            parms.Add("crsNo", item.lvorder);
                            parms.Add("cardNo", "");
                            var result = ly.Cancel(parms, "http://115.159.81.168:8100/ipmsgroup/CRS/cancelbook");
                            var resultCode = JsonHelper.GetJsonValue(result, "resultCode");
                            if (resultCode == "0")
                            {
                                var data = db.everydate_price_t.Where(x => x.everydate == item.arrivetime && x.xz_code == item.xz_code).FirstOrDefault();
                                data.ordernum -= 1;
                                item.state = "X";
                            }
                            else
                            {
                                var data = db.everydate_price_t.Where(x => x.everydate == item.arrivetime && x.xz_code == item.xz_code).FirstOrDefault();
                                data.ordernum -= 1;
                                item.state = "X";
                                isCancel = false;
                            }

                        }
                        else
                        {
                            for (var start = item.arrivetime; start < item.leavetime; start = start.AddDays(1))
                            {
                                var data = db.everydate_price_t.Where(x => x.everydate == start && x.xz_code == item.xz_code).FirstOrDefault();
                                data.ordernum -= 1;
                                item.state = "X";
                            }
                        }
                    }
                    if (hotel.pms == "0")
                    {
                        for (var start = item.arrivetime; start < item.leavetime; start = start.AddDays(1))
                        {
                            var data = db.everydate_price_t.Where(x => x.everydate == start && x.xz_code == item.xz_code).FirstOrDefault();
                            data.ordernum -= 1;
                            item.state = "X";
                        }
                    }
                    if (hotel.pms == "2")
                    {
                        var service = new WxAPI(item.hotelcode);
                        var result = service.ResCancel("10", DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss"), "1010", hotel.username, hotel.pwd, item.guestname, item.lvorder);
                        if (result.Head.retcode == "00001")
                        {
                            var data = db.everydate_price_t.Where(x => x.everydate == item.arrivetime && x.xz_code == item.xz_code).FirstOrDefault();
                            data.ordernum -= 1;
                            item.state = "X";
                        }
                        else
                        {
                            var data = db.everydate_price_t.Where(x => x.everydate == item.arrivetime && x.xz_code == item.xz_code).FirstOrDefault();
                            data.ordernum -= 1;
                            item.state = "X";
                            isCancel = false;
                        }
                    }
                    if (hotel.pms == "3")
                    {
                        var mp = new MPDBEntities();
                        var hotel1 = mp.MPConfigs.FirstOrDefault(x => x.ShopCode == item.hotelcode);
                        //var hotel1 = db.hotel_list.FirstOrDefault(x => x.hotelId == item.hotelcode);
                        // 平台提供的地址
                        string url = hotel1.orderUrl;
                        // 平台appkey
                        string appKey = "KSKJ";
                        // 平台授权码
                        string secret = hotel.pwd;
                        // 使用哪个酒店的身份接入 
                        string hotelId = "G000001";
                        var client = new XopClient(url, appKey, secret, hotelId);
                        // 开始登录平台
                        var rspLogin = client.login();

                        if (!XopClient.isResponseSuccess(rspLogin))
                        {
                            isCancel = false;
                        }
                        else
                        {
                            var c = client.cxlReservation("WX", hotel.username, item.lvorder, "客户取消", "T");
                            if (c.success)
                            {
                                var data = db.everydate_price_t.Where(x => x.everydate == item.arrivetime && x.xz_code == item.xz_code).FirstOrDefault();
                                data.ordernum -= 1;
                                item.state = "X";
                            }
                        }

                    }
                }
                if (isCancel)
                {
                    db.SaveChanges();
                    jsonResult.code = "200";
                    jsonResult.msg = "取消成功";
                    jsonResult.data = orderlist;
                }
                else
                {
                    jsonResult.code = "200";
                    jsonResult.msg = "绿云取消失败";
                    jsonResult.data = orderlist;
                }
            }
        }
        private void GetRoomByRoomtype(HttpContext context)
        {
            var roomtype = Getpost("roomtype");
            using (var db = new yudingEntities())
            {
                var data = db.newroom_t.Where(x => x.roomtype == roomtype).FirstOrDefault();
                jsonResult.code = "200";
                jsonResult.data = data;
            }

        }
        private void GetOrderById(HttpContext context)
        {
            var ordernum = Getpost("ordernum");
            using (var db = new yudingEntities())
            {
                var order = db.order_t.Where(x => x.ordernumber == ordernum).FirstOrDefault();
                jsonResult.code = "200";
                jsonResult.data = order;
 
            }
        }
        private void GetOrderListByState(HttpContext context)
        {
            
            var ispay =int.Parse( Getpost("ispay"));
            var bosscard = Getpost("bosscard");
            var type = int.Parse(Getpost("type"));
            using (var db = new yudingEntities())
            {
                if (ispay != 2)
                {
                    var time = DateTime.Now;
                    // var list = db.order_t.Where(x=>x.ispay==1&&x.bosscard==bosscard&&x.hotelcode==hotelcode&&x.leavetime>=time)
                    var data = db.order_t.Where(x => x.ispay == ispay && x.state == "R" && x.bosscard == bosscard && x.hotelcode == hotelcode && x.leavetime >=time&&x.type==type).OrderByDescending(x => x.addtime).ToList();
                    var increase = new List<object>();
                    foreach (var item in data.GroupBy(x => x.sessionid))
                    {
                        var sessionid = item.FirstOrDefault().sessionid;
                        var list = db.increase_order.Where(x => x.sessionid == sessionid).OrderBy(x => x.addtime).ToList();
                        if (list != null)
                        {
                            if (list.Count > 0)
                            {
                                increase.Add(list);
                            }
                        }
                    }
                    jsonResult.code = "200";
                    var result = new
                    {
                        order = data,
                        increase = increase != null ? increase : null,
                    };
                    jsonResult.data = result;

                }
                else
                {
                    var data = db.order_t.Where(x => x.state != "R" && x.bosscard == bosscard && x.hotelcode == hotelcode&&x.type==type).OrderByDescending(x => x.addtime).ToList();
                    jsonResult.code = "200";
                    var increase = new List<object>();
                    foreach (var item in data.GroupBy(x => x.sessionid))
                    {
                        var sessionid = item.FirstOrDefault().sessionid;
                        var list = db.increase_order.Where(x => x.sessionid == sessionid).ToList();
                        if (list != null)
                        {
                            if (list.Count > 0)
                            {
                                increase.Add(list);
                            }
                        }
                    }
                    jsonResult.code = "200";
                    var result = new
                    {
                        order = data,
                        increase = increase != null ? increase : null,
                    };
                    jsonResult.data = result;
                }
            }
        }
        private void UpdatePayState(HttpContext context)
        {
            var sessionid = Getpost("sessionid");
            using (var db = new yudingEntities())
            {
                var orderlist = db.order_t.Where(x => x.sessionid == sessionid).ToList();
                foreach (var item in orderlist)
                {
                    item.ispay = 1;
                }
                db.SaveChanges();
                jsonResult.code = "200";
                jsonResult.msg = "修改订单状态成功";
            }

        }
        private void UpdateOrder(HttpContext context)
        {
            var orderid = Getpost("ordernumber");
            var remark = Getpost("remark");
            using (var db = new yudingEntities())
            {
                var order = db.order_t.Where(x => x.ordernumber == orderid).FirstOrDefault();
                if (order.lvorder != null)
                {
                    ly ly = new ly("http://115.159.81.168:8100/ipmsgroup/router", hotelcode);
                    Dictionary<string, string> parms = new Dictionary<string, string>();
                    parms.Add("crsNo", order.lvorder );
                    parms.Add("remark", remark);
                    order.notes = order.notes + remark;
                    var result = ly.Change(parms, "http://115.159.81.168:8100/ipmsgroup/CRS/appendResrvBaseRemark");
                }
                else
                {
                    order.notes = order.notes + remark;
                }
               
                db.SaveChanges();
                jsonResult.code = "200";
                jsonResult.msg = "追加成功";
            }
        }
        private void XiaDan(HttpContext context)
        {
            var orderdata = Getpost("orderdata");
            var increasedata = Getpost("increasedata");
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
                            var roomxz = db.rateroom_xz.Where(x => x.roomtype == item.roomtype).FirstOrDefault();
                            var newroom = db.ratecode_t.Where(x => x.ratecode == roomxz.ratecode).FirstOrDefault();
                            var date = DateTime.Parse(item.arr);
                            var every = db.everydate_price_t.Where(x => x.everydate == date && x.xz_code == item.xz_code).FirstOrDefault();
                            var room = db.newroom_t.Where(x => x.roomtype == item.roomtype).FirstOrDefault();
                            var hotel = db.hotel_list.FirstOrDefault(x => x.hotelId == item.hotelcode);
                            if (every.ordernum <every.num)
                            {
                                if (roomxz.qudao != null && roomxz.qudao != "")
                                {
                                    //绿云下单
                                    if (!PlaceOrder(db, item,hotel.pms, room.pms, roomxz.pmsratecode, roomxz.qudao, true))
                                    {
                                        isorder = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (newroom.qudao != "" && newroom.qudao!=null)
                                    {
                                        //绿云下单
                                        if (!PlaceOrder(db, item, hotel.pms, room.pms, newroom.pmsratecode, newroom.qudao, true))
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
                            if (increaselist!=null)
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
        private bool PlaceOrder(yudingEntities db, order item,string pmscode="0",string pms="",string pmsratecode = "", string qudao = "", bool ispms = false)
        {
            var order = new order_t()
            {
                ptchannel=item.ptchannel,
                isSendMsg="0",
                hotelname=item.hotelname,
                City=item.City,
                channelid=item.channelid,
                guestname=item.guestname,
                arrivetime = DateTime.Parse(item.arr),
                baojia = item.baojia,
                bosscard = item.resby,
                contact_mobile = item.mobile,
                contact_name = item.name,
                count =int.Parse( item.count),
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
                rate =item.rate,
                tpid=item.tpid,
                type=int.Parse(item.type),
                Fmoney=item.Fmoney,
                TicketSn=item.TicketSn,
            };
            if (ispms&&item.type=="0")
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
                    parms.Add("everyDayRate", "[{\"date\":\"" + item.arr + " 12:00:00\",\"realRate\":\"" + (item.rate - int.Parse(item.yhmoney)).ToString() + "\"}]");
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
                        var res3 = service.Reservation("10", DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss"), "1014", hotel1.username, hotel1.pwd, item.guestname, "NA", "ADD", "6016913", "", "CHANGE", item.remark, "GUEST", "性别", item.name, "", "C", "NA", "ADD", "CHANGE", "RESERVED", "", pms, "", "DAY", item.arr + " 18:00:00", nightNum, "", "ADULT", "1", "CHANGE", pmsratecode,hotel.src, hotel.market, item.count, qudao,order.contact_mobile, order.contact_name, "");
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
        private void GetRoomInfo(HttpContext context)
        {
            var roomtype = Getpost("roomtype");
            using (var db = new yudingEntities())
            {
                var info = db.newroom_t.Where(x => x.roomtype == roomtype).FirstOrDefault();
                var pic = db.room_pic_t.Where(x => x.roomtypecode == roomtype).ToList();
                var increase = db.increase_link.Where(x => x.roomtype == roomtype).ToList();
                var facilities = db.roomtype_f_t.Where(x => x.roomtype == roomtype).ToList();
                var flist = new List<object>();
                var indata = new List<object>();
                if (increase.Count > 0)
                {
                    foreach (var item in increase)
                    {
                        var data = db.increases.Where(x => x.increase_code == item.increase).FirstOrDefault();
                        indata.Add(data);

                    }
                }
                if (facilities.Count > 0)
                {
                    foreach (var item in facilities)
                    {
                        var data = db.facilities_t.Where(x => x.code == item.fcode).FirstOrDefault();
                        flist.Add(data);
                    }
                }
                var result = new
                {
                    info = info,
                    pic = pic,
                    increase = indata,
                    facilities = flist,
                };
                jsonResult.code = "200";
                jsonResult.msg = "获取成功";
                jsonResult.data = result;
            }
        }

        private void GetRateCodexzByRoomtype(HttpContext context)
        {
            //using (var db = new yudingEntities())
            //{
                
                var roomtype = Getpost("roomtype");
                var time = Getpost("starttime");
                var end = Getpost("endtime");
                var type = int.Parse(Getpost("type"));
                var db = new yudingEntities();
                var week = (int)DateTime.Parse(time).DayOfWeek;
                var list = BookingWeb.GetRateCodexzByRoomtype(db, time, end, roomtype, hotelcode, type);
                //var list = bll.GetRateList(time, end, roomtype, hotelcode, week.ToString());
                //var roomtype = Getpost("roomtype");
                //var time = DateTime.Parse(Getpost("starttime"));
                //var end = GetTimeStamp(DateTime.Parse(Getpost("endtime")));
                //var xzlist = db.GetRateCodexzByRoomtypes.Where(x => x.roomtype == roomtype && x.flag == 0 && x.hotelid ==hotelcode&& x.everydate == time).GroupBy(x => x.xz_code).OrderByDescending(x=>x.FirstOrDefault().px).ToList();
                //var yuanjia = db.newroom_t.Where(x => x.roomtype == roomtype).FirstOrDefault().yuanjia;
                //var list = new List<object>();
                //var d = db.ratecode_t.Where(x => x.hotelid == hotelcode && x.scenario == 1 && x.flag == 0).FirstOrDefault().ratecode;
                //foreach (var item in xzlist)
                //{
                //    var xzcode = item.FirstOrDefault().xz_code;
                //    var rate = db.rateroom_xz.Where(x=>x.xz_code==xzcode).FirstOrDefault();
                //    var c = db.xztimestart_t.Where(x => x.startdate <= time &&x.enddate>=time&& x.xz_code == xzcode&&x.flag==0).FirstOrDefault();
                //    if(rate.ratecode==d)
                //    {
                //        if (c != null)
                //        {
                //            Nullable<int> sum = 0;
                //            foreach (var item1 in xzlist)
                //            {
                //                sum += item1.FirstOrDefault().ordernum;
                //            }
                //            var price = db.everydate_price_t.Where(x => x.xz_code == xzcode && x.everydate == time).FirstOrDefault();
                //            if (price != null)
                //            {
                //                var num = price.num;
                //                var ordernum = price.ordernum;
                //                var miaosha = db.miaosha_t.Where(x => x.xz_code == xzcode).FirstOrDefault();
                //                var starttime = "";
                //                var endtime = "";
                //                if (miaosha != null)
                //                {
                //                    starttime = GetTimeStamp(DateTime.Parse(miaosha.starttime)).ToString();
                //                    endtime = GetTimeStamp(DateTime.Parse(miaosha.endtime)).ToString();
                //                }
                //                var ass = db.associateds.Where(x => x.xz_code == xzcode).FirstOrDefault();
                //                var baojia = db.baojia_t.Where(x => x.xz_code == xzcode).FirstOrDefault();
                //                var baojiaid = "";
                //                if (baojia != null)
                //                {
                //                    baojiaid = baojia.formulaid.ToString();
                //                }
                //                object rules = null;
                //                object rulesname = null;
                //                if (ass != null)
                //                {
                //                    if (ass.rules == "early")
                //                    {
                //                        var id = int.Parse(ass.codeid);
                //                        rulesname = "early";
                //                        var early = db.earlies.Where(x => x.id == id).FirstOrDefault();
                //                        rules = early.code;
                //                    }
                //                    if (ass.rules == "live")
                //                    {
                //                        var id = int.Parse(ass.codeid);
                //                        rulesname = "live";
                //                        var live = db.lives.Where(x => x.id == id).FirstOrDefault();
                //                        rules = live.code;
                //                    }
                //                }
                //                var activty = db.activity_link.Where(x => x.xz_code == xzcode).FirstOrDefault();
                //                var dis = "";
                //                if (activty != null)
                //                {
                //                    dis = db.Activities.Where(x => x.Code == activty.activitycode).FirstOrDefault().discount;
                //                }
                //                var formula = db.price_formulaid.Where(x => x.xz_code == xzcode).ToList();
                //                List<System.String> listS = new List<System.String>();
                //                string str = "";
                //                if (formula.Count > 0)
                //                {
                //                    foreach (var item1 in formula)
                //                    {
                //                        listS.Add(item1.formulaid + ":" + item1.categoryid);
                //                    }
                //                    listS.ToArray();
                //                }
                //                str = string.Join(",", listS);
                //                Nullable<int> physicalnum = item.FirstOrDefault().pnum;
                //                var a = new
                //                {
                //                    xzcode = xzcode,
                //                    islock = item.FirstOrDefault().islock,
                //                    package = item.FirstOrDefault().package,
                //                    payway = item.FirstOrDefault().payway,
                //                    pay = item.FirstOrDefault().pay,
                //                    xzname = item.FirstOrDefault().xz_name,
                //                    yuanjia = item.FirstOrDefault().yuanjia,
                //                    ordersum = sum,
                //                    ordernum = ordernum,
                //                    pnum = physicalnum,
                //                    num = num,
                //                    baojia = baojiaid,
                //                    price = item.FirstOrDefault().price,
                //                    starttime = starttime,
                //                    endtime = endtime,
                //                    startdate = GetTimeStamp(time),
                //                    enddate = end,
                //                    rulesname = rulesname,
                //                    rules = rules,
                //                    activty = dis,
                //                    formula = str,
                //                    roomtype = roomtype,
                //                };
                //                list.Add(a);
                //            }
                //        }
                //    }
                //}
                jsonResult.code = "200";
                jsonResult.msg = "";
                jsonResult.data = list;
           // }
        }
        private void GetRoomsByDate(HttpContext context)
        {
            var r = Getpost("date");
            var time = DateTime.Parse(Getpost("date"));
            var week = (int)time.DayOfWeek;
            var type =int.Parse(Getpost("type"));
           // var client = new RedisClient("127.0.0.1", 6379);
            var str = hotelcode + time.ToString("yyyy-MM-dd")+type.ToString();
            //var roomListData = client.Get<List<info>>(str);
            //if (roomListData==null)
            //{
                using (var db = new yudingEntities())
                {
                    var ratecode1 = db.ratecode_t.Where(x => x.flag == 0 && x.scenario == 1 && x.hotelid == hotelcode && x.type == type).FirstOrDefault();
                    if (ratecode1 != null)
                    {
                        var a = ratecode1.ratecode;
                        var ratecodelist = db.newroom_t.Where(x => x.flag == 0 && x.hotelid == hotelcode && x.type == type).OrderByDescending(x=>x.pxid).ToList();
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
                       // client.Set<List<info>>(str, list);
                        jsonResult.code = "200";
                        jsonResult.msg = "查询成功";
                        jsonResult.data = list;
                    }
                    else
                    {
                        jsonResult.code = "500";
                        jsonResult.msg = "查询成功";
                    }
                }
            //}
            //else
            //{
            //    jsonResult.code = "200";
            //    jsonResult.msg = "查询成功";
            //    jsonResult.data = roomListData;
            //}
        }
        private void GetOrderListBySessionidQT(HttpContext context)
        {
            var type = int.Parse(Getpost("type"));
            var page = Getpost("page");
            var ispay = int.Parse(Getpost("ispay") != "" ? Getpost("ispay") : "2");
            var addtime = Getpost("addtime");
            var list = new List<object>();

            DataTable dt = new DataTable("cart");
            dt.Columns.Add(new DataColumn("ispay", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("sessionid", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("totalrate", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("yhmoney", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("name", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("addtime", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("count", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("mobile", Type.GetType("System.String")));
            dt.Columns.Add(new DataColumn("arr", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("dep", typeof(DateTime)));
            using (var db = new yudingEntities())
            {
                var q = db.order_t.Where(x => x.hotelcode == hotelcode && x.type == type && x.ispay == ispay )
                    .GroupBy(x => new { x.sessionid}).ToList();
                for (int i = 0; i < q.Count; i++)
			    {
                    var resultdata=q[i].ToList();
                    Decimal price = 0;
                    DataRow dr = dt.NewRow();
                    dr["ispay"] = resultdata[0].ispay;
                    dr["sessionid"] = resultdata[0].sessionid;
                    dr["name"] = resultdata[0].contact_name;
                    dr["addtime"] = resultdata[0].addtime;
                    dr["mobile"] = resultdata[0].contact_mobile;
                    for (int a = 0; a < resultdata.Count; a++)
                    {   
                        price=Convert.ToDecimal(resultdata[a].rate*resultdata[a].count)+price;
                        
                    }
                    dr["totalrate"] = price;
                    dr["yhmoney"] = resultdata.Sum(x => x.yhmoney);
                    dr["count"] = resultdata.Sum(x => x.count);
                    dr["arr"] = resultdata.Min(x => x.arrivetime);
                    dr["dep"] = resultdata.Max(x => x.leavetime);
                    dt.Rows.Add(dr);
                }
                if (q.Count > 0)
                {
                    var totalcount = q.Count.ToString();
                    jsonResult.code = "200";
                    int pageindex = int.Parse(page);
                    dt.DefaultView.Sort = "addtime desc";
                    DataTable dtSort = dt.DefaultView.ToTable();
                    DataTable sa = getOnePageTable(dtSort, pageindex, 10);
                    jsonResult.data = sa;
                    jsonResult.msg = totalcount;
                }
                else
                {
                    jsonResult.code = "502";
                    jsonResult.msg = "无数据";
                }

            }

        }
        #region 后台接口
        private void EditSpecialIsName(HttpContext context)
        {
            var isname = Getpost("isname");
            var bgpic = Getpost("bgimg");
            var isRecommend = Getpost("isRecommend");
            var isSendWeather = Getpost("isSendWeather");
            using (var db = new yudingEntities())
            {
                var result = db.specials.Where(x => x.hotelcode == hotelcode).FirstOrDefault();
                addLog(db, hotelcode, oid, yemian, operation, isname + "," + bgpic, result.isname + "," + result.bgimg);
                result.isSendWeather = isSendWeather;
                result.isname = isname;
                result.bgimg = bgpic;
                result.isRecommend = isRecommend;
                db.SaveChanges();
                jsonResult.code = "200";
                jsonResult.msg = "修改成功";

            }
        }
        private void EditSpecialminutes(HttpContext context)
        {

            var min = Getpost("minutes");
            
            using (var db = new yudingEntities())
            {
                var result = db.specials.Where(x => x.hotelcode == hotelcode).FirstOrDefault();
                addLog(db, hotelcode, oid, yemian, operation, min.ToString(), result.minutes.ToString());
                result.minutes =int.Parse(min);
                db.SaveChanges();
                jsonResult.code = "200";
                jsonResult.msg = "修改成功";

            }
        }
        private void GetSpecial(HttpContext context)
        {
        
            using (var db = new yudingEntities())
            {
                var result = db.specials.Where(x => x.hotelcode == hotelcode).FirstOrDefault();
                if (result != null)
                {
                    jsonResult.code = "200";
                    jsonResult.msg = "查询成功";
                    jsonResult.data = result;
                }
                else
                {
                    special s = new special()
                    {
                        hotelcode = hotelcode,
                        isSendWeather="0",
                    };
                    db.specials.Add(s);
                    db.SaveChanges();
                }
 
            }
        }
        private void UpdateOrderState(HttpContext context)
        {
            var ispay = Getpost("isdeal");
            var state = Getpost("state");
             var ordernumber = Getpost("ordernumber");
            using (var db = new yudingEntities())
            {
                var data = db.order_t.Where(x => x.ordernumber == ordernumber).FirstOrDefault();
                addLog(db, hotelcode, oid, yemian, operation, ispay + "," + state, data.ispay.ToString() + "," + data.state);
                data.isdeal = int.Parse(ispay);
                data.state = state;
                db.SaveChanges();
                
                jsonResult.code = "200";
                jsonResult.msg = "修改成功";
 
            }
            
        }
        private void BatchStopRatecode_xz(HttpContext context)
        {
            using (var db = new yudingEntities())
            {
                var xzcode = Getpost("xz_code");
                var flag = int.Parse(Getpost("islock"));
                var starttime = DateTime.Parse(Getpost("starttime"));
                 var endtime = DateTime.Parse(Getpost("endtime"));
                 var a =  db.BatchLockRoom(xzcode, flag, starttime, endtime);
                 //while (starttime <= endtime)
                 //{
                 //    BookingWeb.Refresh(db, xzcode, hotelcode, 0, starttime);
                 //    starttime = starttime.AddDays(1);
                 //}
                 jsonResult.code = "200";
                 jsonResult.msg = "锁房成功";
            }
        }
        private void UpdateEveryDate(HttpContext context)
        {
            var xzcode = Getpost("xz_code");
            var json = Getpost("postData");
            var type = int.Parse(Getpost("type"));
            var data = JsonConvert.DeserializeObject<List<every>>(json);
            using (var db = new yudingEntities())
            {
                foreach (var item in data)
                {
                    var time =DateTime.Parse(item.date);
                    var e = db.everydate_price_t.Where(x => x.everydate == time && x.xz_code == xzcode).FirstOrDefault();
                    if (e != null)
                    {
                        addLog(db, hotelcode, oid, yemian, operation, "细则代码：" + xzcode + ",价格：" + item.price + ",数量：" + item.num + ",含早：" + item.package
                            ,"细则代码：" + xzcode + ",价格：" + e.price + ",数量：" + e.num + ",含早：" + e.package);
                        e.price = item.price;
                        e.islock = item.islock;
                        e.num = item.num;
                        e.package = item.package;
                        db.SaveChanges();
                        //TODO:
                       
                    }
                    else
                    {
                        everydate_price_t ep = new everydate_price_t()
                        {
                            xz_code = xzcode,
                            price = item.price,
                            package = item.package,
                            num = item.num,
                            islock = item.islock,
                            everydate = DateTime.Parse(item.date),
                            hotelid = hotelcode,
                        };
                        //addLog(db,hotelcode,oid,yemian,operation)
                        addLog(db, hotelcode, oid, yemian, operation, "细则代码：" + xzcode + ",价格：" + item.price + ",数量：" + item.num + ",含早：" + item.package );
                        db.everydate_price_t.Add(ep);
                        db.SaveChanges();
                    }
                    //BookingWeb.Refresh(db, xzcode, hotelcode, type, time);
                }
               
                jsonResult.code = "200";
                jsonResult.msg = "修改成功";
            }
        }
        private void GetRateCodeByRoomtype_xz(HttpContext context)
        {
            var roomtype = Getpost("roomtype");
            using (var db = new yudingEntities())
            {
                var list = db.GetRateCode_xz.Where(x => x.roomtype == roomtype&&x.flag==0&&x.rateFlag==0).GroupBy(x => x.xz_code).ToList();
                var result= new List<GetRateCode_xz>();
                foreach (var item in list)
                {
                    result.Add(item.FirstOrDefault());
                }
                jsonResult.code = "200";
                jsonResult.msg = "查询成功";
                jsonResult.data = result;
            }
        }
        private void GetRateCode_xz_all(HttpContext context)
        {
            using (var db = new yudingEntities())
            {
                var ratecode = Getpost("ratecode");
                var a = db.GetRateCode_xz.Where(x => x.ratecode == ratecode&&x.flag==0&&x.roomFlag==0).GroupBy(x => x.roomtype).ToList();
                var list = new List<info>();
                foreach (var item in a)
                {
                    var b = new info()
                    {
                        xz = new List<xz>()
                    };
                    b.roomtype = item.FirstOrDefault().roomtype;
                    b.roomname = item.FirstOrDefault().descript;
                    var xz1 = new List<xz>();
                    foreach (var item1 in item.GroupBy(x => x.xz_code))
                    {
                        var s = new xz()
                            {
                                xz_code = item1.FirstOrDefault().xz_code.ToString(),
                                xz_name = item1.FirstOrDefault().xz_name.ToString(),
                            };
                        xz1.Add(s);
                    }
                    b.xz.AddRange(xz1);
                    list.Add(b);
                }
                jsonResult.code = "200";
                jsonResult.msg = "获取成功";
                jsonResult.data = list;
            }
        }
        private void DelRateCodeByBatch_xz(HttpContext context)
        {
            try
            {
                var batch = Getpost("batch");
                var xzcode = Getpost("xz_code");
                using (var db = new yudingEntities())
                {
                    var result = db.xztimestart_t.Where(x => x.batch == batch).ToList();
                    foreach (var item in result)
                    {
                        db.xztimestart_t.Remove(item);
                    }
                    if (db.SaveChanges() > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "删除成功";
                        var count = db.xztimestart_t.Where(x => x.xz_code== xzcode).ToList();
                        if (count.Count == 0)
                        {
                            var data = db.rateroom_xz.Where(x => x.xz_code == xzcode).ToList();
                            foreach (var item in data)
                            {
                                item.istime = 0;
                            }
                           // data.is = "0";
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void GetRateCodeByBatch_xz(HttpContext context)
        {
            try
            {
                var roomtype = Getpost("batch");
                using (var db = new yudingEntities())
                {

                    var result = db.xztimestart_t.Where(x => x.batch == roomtype).ToList();
                    if (result.Count > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "查询成功";
                        jsonResult.data = result;
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void GetRateCodeGroup_xz(HttpContext context)
        {
            try
            {
                var roomtype = Getpost("xz_code");
                using (var db = new yudingEntities())
                {
                    var result = db.xztimestart_t.Where(x => x.xz_code == roomtype).GroupBy(x => x.batch).ToList();
                    var a = new List<xztimestart_t>();
                    foreach (var item in result)
                    {
                        a.Add(item.FirstOrDefault());
                    }
                    if (result != null)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "查询成功";
                        jsonResult.data = a;
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }

            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void CopyRatecode_xz(HttpContext context)
        {
            using (var db = new yudingEntities())
            {

                var json = Getpost("CopyData");
                var roomtype = Getpost("roomtype");
                var ratecode = Getpost("ratecode");
                var list = JsonConvert.DeserializeObject<List<CopyRateCode_xz>>(json);
              
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                       
                            foreach (var item in list)
                            {
                                Random ran = new Random();
                                var xzcode = OrderHelper.GetRandom1(hotelcode);
                                var data = db.rateroom_xz.Where(x => x.xz_code == item.xz_code).FirstOrDefault();
                                foreach (var item1 in item.index)
                                {
                                    var xz = new rateroom_xz()
                                    {
                                        payway = data.payway,
                                        xz_code = xzcode,
                                        xz_name = data.xz_name,
                                        WeekIndex = item1.index,
                                        roomtypename = data.roomtypename,
                                        roomtype = roomtype,
                                        ratecodename = data.ratecodename,
                                        ratecode = ratecode,
                                        price = decimal.Parse(item1.price),
                                        pay = data.pay,
                                        package = int.Parse(item.package),
                                        num = int.Parse(item1.num),
                                        hotelid = hotelcode,
                                        flag = data.flag,
                                        duojian = data.duojian,
                                        px = data.px,
                                        state = data.state,
                                        istime = data.istime,
                                        chaifen=data.chaifen,
                                        cusno=data.cusno,
                                    };
                                    addLog(db, hotelcode, oid, yemian, operation, "细则名称:" + data.xz_name + ",细则代码:" + xzcode + ",原细则代码:" + data.xz_code);
                                    db.rateroom_xz.Add(xz);
                                }
                                var xztimelist = db.xztimestart_t.Where(x => x.xz_code == item.xz_code).ToList();
                                var batchnumber = hotelcode + GetTimeStamp() + ran.Next(1000, 9999) + "batch";
                                foreach (var item4 in xztimelist)
                                {
                                    var xztime = new xztimestart_t()
                                    {
                                        batch = batchnumber,
                                        enddate = item4.enddate,
                                        endtime = item4.endtime,
                                        flag = item4.flag,
                                        hotelid = hotelcode,
                                        px = item4.px,
                                        startdate = item4.startdate,
                                        starttime = item4.starttime,
                                        week = item4.week,
                                        xz_code = xzcode,
                                    };
                                    db.xztimestart_t.Add(xztime);
                                    addLog(db, hotelcode, oid, yemian, operation, "复制时间段", "起始时间:" + item4.startdate + "-" + item4.enddate + ",细则代码:" + xzcode + ",原细则代码:" + data.xz_code);
                                }
                                var baojia = db.baojia_t.Where(x => x.xz_code == item.xz_code).FirstOrDefault();
                                if (baojia != null)
                                {
                                    baojia_t b = new baojia_t()
                                    {
                                        xz_code = xzcode,
                                        hotelid = hotelcode,
                                        formulaid = baojia.formulaid,
                                    };
                                    addLog(db, hotelcode, oid, yemian, operation, "复制包价", "包价:" + baojia.formulaid + ",细则代码:" + xzcode + ",原细则代码:" + data.xz_code);
                                    db.baojia_t.Add(b);
                                }
                                var miaosha = db.miaosha_t.Where(x => x.xz_code == item.xz_code).FirstOrDefault();
                                if (miaosha != null)
                                {
                                    miaosha_t m = new miaosha_t()
                                    {
                                        xz_code = xzcode,
                                        starttime = miaosha.starttime,
                                        hotelid = hotelcode,
                                        endtime = miaosha.endtime,
                                    };
                                    addLog(db, hotelcode, oid, yemian, operation, "复制限时抢购", "时间:" + miaosha.starttime + "-" + miaosha.endtime + ",细则代码:" + xzcode + ",原细则代码:" + data.xz_code);
                                    db.miaosha_t.Add(m);
                                }
                                var ass = db.associateds.Where(x => x.xz_code == item.xz_code).FirstOrDefault();
                                if (ass != null)
                                {
                                    associated a = new associated()
                                    {
                                        xz_code = xzcode,
                                        rules = ass.rules,
                                        hotelid = hotelcode,
                                        flag = ass.flag,
                                        codeid = ass.codeid,
                                    };
                                    addLog(db, hotelcode, oid, yemian, "复制规则", operation, "规则:" + ass.rules + ",细则代码:" + xzcode + ",原细则代码:" + data.xz_code);
                                    db.associateds.Add(a);
                                }
                                var activity = db.activity_link.Where(x => x.xz_code == item.xz_code).FirstOrDefault();
                                if (activity != null)
                                {
                                    activity_link al = new activity_link()
                                    {
                                        xz_code = xzcode,
                                        hotelcode = hotelcode,
                                        activitycode = activity.activitycode,
                                    };
                                    addLog(db, hotelcode, oid, yemian, "复制规则", "活动:" + activity.activitycode + ",细则代码:" + xzcode + ",原细则代码:" + data.xz_code);
                                    db.activity_link.Add(al);
                                }
                                var formula = db.price_formulaid.Where(x => x.xz_code == item.xz_code).ToList();
                                if (formula.Count > 0)
                                {
                                    foreach (var item2 in formula)
                                    {
                                        var pf = new price_formulaid()
                                        {
                                            xz_code = xzcode,
                                            formulaid = item2.formulaid,
                                            categoryid = item2.categoryid,
                                            hotelid = hotelcode,
                                        };
                                        addLog(db, hotelcode, oid, yemian, "复制可用产品", "产品:" + item2.formulaid + ",子劵：" + item2.categoryid + ",细则代码:" + xzcode + ",原细则代码:" + data.xz_code);
                                        db.price_formulaid.Add(pf);
                                    }
                                }
                            }
                        
                        db.SaveChanges();
                        jsonResult.code = "200";
                        jsonResult.msg = "修改成功";
                    }
                }
            }
        }
        private void StopRateCode_xz(HttpContext context)
        {
            var roomtype = Getpost("xz_code");
            var flag = int.Parse(Getpost("flag"));
            using (var db = new yudingEntities())
            {
                var list = db.rateroom_xz.Where(x => x.xz_code == roomtype).ToList();
                foreach (var item in list)
                {
                    addLog(db, hotelcode, oid, yemian, operation, flag.ToString());
                    item.flag = flag;                   
                }
                db.SaveChanges();
                jsonResult.code = "200";
                jsonResult.msg = "修改成功";
            }

        }
        private void GetAdvanced(HttpContext context)
        {
            var xzcode = Getpost("xz_code");
            using (var db = new yudingEntities())
            {
                var miaosha = db.miaosha_t.Where(x => x.xz_code == xzcode).FirstOrDefault();
                var baojia = db.baojia_t.Where(x => x.xz_code == xzcode).FirstOrDefault();
                var ass = db.associateds.Where(x => x.xz_code == xzcode).FirstOrDefault();
                var activity_link = db.activity_link.Where(x => x.xz_code == xzcode).FirstOrDefault();
                var price_formula = db.price_formulaid.Where(x => x.xz_code == xzcode).ToList();
                var onsalecode = db.rateroom_xz.FirstOrDefault(x => x.xz_code == xzcode);
                var result = new
                {
                    miaosha = miaosha,
                    baojia = baojia,
                    associateds = ass,
                    activity_link = activity_link,
                    price_formula = price_formula,
                    onsalecode = onsalecode.onsalecode
                };
                jsonResult.code = "200";
                jsonResult.data = result;
                jsonResult.msg = "查询成功";
            }
        }
        private void GetRateCode_xz_List(HttpContext context)
        {
            var ratecode = Getpost("ratecode");
            var roomtype = Getpost("roomtype");
            try
            {
                using (var db = new yudingEntities())
                {
                    var result = db.GetRateCode_xz.Where(x => x.hotelid == hotelcode && x.ratecode == ratecode && x.roomtype == roomtype).GroupBy(x => x.xz_code).ToList();
                    if (result.Count > 0)
                    {
                        var list = new List<GetRateCode_xz>();
                        foreach (var item in result)
                        {
                            list.Add(item.FirstOrDefault());
                        }
                        jsonResult.code = "200";
                        jsonResult.msg = "获取成功";
                        jsonResult.data = list.OrderByDescending(x=>x.px);
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "没有该实体数据";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void GetRateCode_xz(HttpContext context)
        {
            try
            {
                var xzcode = Getpost("xz_code");
                using (var db = new yudingEntities())
                {
                    var result = db.rateroom_xz.Where(x => x.hotelid == hotelcode&&x.xz_code==xzcode).ToList();
                    if (result.Count > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "获取成功";
                        jsonResult.data = result;
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "没有该实体数据";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            } 
        }
        private void StopScenceCode(HttpContext context)
        {
            try
            {
                var id = context.Request.Form["ratecode"];
                var flag = int.Parse(context.Request.Form["flag"]);
                using (var db = new yudingEntities())
                {
                    var data = db.ratecode_t.FirstOrDefault(x => x.ratecode== id);
                    if (data != null)
                    {
                        addLog(db, hotelcode, oid, yemian, operation, flag.ToString(), data.flag.ToString());
                        data.flag = flag;
                        db.SaveChanges();
                        jsonResult.code = "200";
                        jsonResult.msg = "更新成功";
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "没有实体数据";

                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void GetHotelIncreaseList(HttpContext context)
        {
            try
            {
               
                using (var db = new yudingEntities())
                {
                    var result = db.increases.Where(x => x.hotelid == hotelcode ).ToList();
                    if (result.Count > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "获取成功";
                        jsonResult.data = result;
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "没有该实体数据";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void StopIncrease(HttpContext context)
        {
            try
            {
               
                var id = int.Parse(Getpost("id"));
                using (var db = new yudingEntities())
                {
                    var result = db.increase_link.Where(x => x.id == id).FirstOrDefault();
                    addLog(db, hotelcode, oid, yemian, operation, "", result.increase.ToString());
                    db.increase_link.Remove(result);
                    if (db.SaveChanges() > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "更新成功";
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void DelHotelPic(HttpContext context)
        {
            try
            {
                var id = int.Parse(context.Request.Form["id"]);
               
                using (var db = new yudingEntities())
                {
                    var data = db.hotelpic_t.FirstOrDefault(x => x.id == id);
                    if (data != null)
                    {
                        db.hotelpic_t.Remove(data);
                        addLog(db, hotelcode, oid, yemian, operation,"", data.picname);
                        
                        //var logJson = context.Request.Form["logJson"];
                        //var log = JsonConvert.DeserializeObject<newback>(logJson);
                        //log.hotelid = hotelcode;
                        // log.olddata = data.flag.ToString();
                        //log.newdata = data.flag.ToString();
                        //db.SaveChanges();
                        //db.newbacks.Add(log);
                        //addLog(db, hotelcode, oid, yemian, operation, flag.ToString(), data.flag.ToString());
                        db.SaveChanges();
                        jsonResult.code = "200";
                        jsonResult.msg = "删除成功";
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "没有实体数据";

                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void UpdateActivityFlag(HttpContext context)
        {
            try
            {
                var id = int.Parse(context.Request.Form["Id"]);
                var flag = int.Parse(context.Request.Form["flag"]);
                using (var db = new yudingEntities())
                {
                    var data = db.Activities.FirstOrDefault(x => x.Id== id);
                    if (data != null)
                    {
                        addLog(db, hotelcode, oid, yemian, operation, flag.ToString(), data.flag.ToString());
                        data.flag = flag;
                        //var logJson = context.Request.Form["logJson"];
                        //var log = JsonConvert.DeserializeObject<newback>(logJson);
                        //log.hotelid = hotelcode;
                        // log.olddata = data.flag.ToString();
                        //log.newdata = data.flag.ToString();
                        //db.SaveChanges();
                        //db.newbacks.Add(log);
                        
                        db.SaveChanges();
                        jsonResult.code = "200";
                        jsonResult.msg = "更新成功";
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "没有实体数据";

                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void YhCodeFlag(HttpContext context)
        {
            try
            {
                var id = int.Parse(context.Request.Form["id"]);
                var flag = int.Parse(context.Request.Form["flag"]);
                using (var db = new yudingEntities())
                {
                    var data = db.yhcode_t.FirstOrDefault(x => x.id == id);
                    if (data != null)
                    {
                        addLog(db, hotelcode, oid, yemian, operation, flag.ToString(), data.flag.ToString());
                        data.flag = flag;
                        
                        //var logJson = context.Request.Form["logJson"];
                        //var log = JsonConvert.DeserializeObject<newback>(logJson);
                        //log.hotelid = hotelcode;
                        // log.olddata = data.flag.ToString();
                        //log.newdata = data.flag.ToString();
                        //db.SaveChanges();
                        //db.newbacks.Add(log);
                        //addLog(db, hotelcode, oid, yemian, operation, flag.ToString(), data.flag.ToString());
                        db.SaveChanges();
                        jsonResult.code = "200";
                        jsonResult.msg = "更新成功";
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "没有实体数据";

                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void UpdateYhCode(HttpContext context)
        {
            var yhcodename = Getpost("yhcodename");
            var tmoney = Getpost("tmoney");
           
            try
            {
                var id = int.Parse(Getpost("id"));
                using (var db = new yudingEntities())
                {
                    var result = db.yhcode_t.FirstOrDefault(x => x.id == id);
                    addLog(db, hotelcode, yemian, operation, tmoney + "," + yhcodename, result.tmoney + "," + result.yhcodename);
                    result.yhcodename = yhcodename;
                    result.tmoney =decimal.Parse( tmoney);
                    
                    if (db.SaveChanges() > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "更新成功";
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
           
        }
        private void DelRoomPic(HttpContext context)
        {
            try
            {
                var id = int.Parse(Getpost("id"));
                using (var db = new yudingEntities())
                {
                    var result = db.room_pic_t.FirstOrDefault(x => x.id == id);
                    db.room_pic_t.Remove(result);
                    addLog(db, hotelcode, yemian, operation, "", result.imgurl);
                    if (db.SaveChanges() > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "删除成功";
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void UpdateRoomRemark(HttpContext context)
        {
            try
            {
                var id = int.Parse(Getpost("id"));
                var qxxz = Getpost("remark");
                using (var db = new yudingEntities())
                {
                    var result = db.newroom_t.FirstOrDefault(x => x.id == id);

                    addLog(db, hotelcode, oid, yemian, operation, qxxz, result.remark);
                    result.remark = qxxz;
                    if (db.SaveChanges() > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "更新成功";
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void UpdateYD(HttpContext context)
        {
            try
            {
                var id = int.Parse(Getpost("id"));
                var qxxz = Getpost("ydxz");
                using (var db = new yudingEntities())
                {
                    var result = db.newroom_t.FirstOrDefault(x => x.id == id);

                    addLog(db, hotelcode, oid, yemian, operation, qxxz, result.qxxz);
                    result.ydxz = qxxz;
                    if (db.SaveChanges() > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "更新成功";
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void UpdateQX(HttpContext context)
        {
            try
            {
                var id = int.Parse(Getpost("id"));
                var qxxz = Getpost("qxxz");
                using (var db = new yudingEntities())
                {
                    var result = db.newroom_t.FirstOrDefault(x => x.id == id);

                    addLog(db, hotelcode, oid, yemian, operation, qxxz, result.qxxz);
                    result.qxxz = qxxz;
                    if (db.SaveChanges() > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "更新成功";
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void DelHotelFacilities(HttpContext context)
        {
            try
            {
                var id = int.Parse(Getpost("id"));
                using (var db = new yudingEntities())
                {
                    var result = db.hotelfacilities_t.FirstOrDefault(x => x.id == id);
                    db.hotelfacilities_t.Remove(result);
                    addLog(db, hotelcode, oid, yemian, operation, "", result.codename);
                    if (db.SaveChanges() > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "删除成功";
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void DelPicDb(HttpContext context)
        {
            try
            {
                var id =int.Parse(Getpost("id"));
                using (var db = new yudingEntities())
                {
                    var result = db.facilities_t.FirstOrDefault(x => x.id == id);
                    db.facilities_t.Remove(result);
                    addLog(db, hotelcode, oid, yemian, operation, "", result.codename);
                    if (db.SaveChanges() > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "删除成功";
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void SetHotelFacilites(HttpContext context)
        {
            var FacilitiesJson = context.Request.Form["RoomFacilitiesJson"];
            if (FacilitiesJson != null)
            {
                var result = JsonConvert.DeserializeObject<List<roomtype_f_t>>(FacilitiesJson);
                using (var db = new yudingEntities())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            if (result != null)
                            {
                                foreach (var item in result)
                                {
                                    var data = db.roomtype_f_t.FirstOrDefault(x => x.id == item.id);
                                    if (data == null)
                                    {
                                        db.roomtype_f_t.AddOrUpdate(item);
                                        addLog(db, hotelcode, oid, yemian, operation, item.fcode);
                                    }
                                }
                                jsonResult.code = "200";
                                jsonResult.msg = "添加或修改成功";
                                transaction.Commit();
                            }
                        }
                        catch (Exception ex)
                        {
                            jsonResult.code = "504";
                            jsonResult.msg = ex.ToString();
                            transaction.Rollback();
                            HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
                        }
                    }
                }
            }
        }
        private void GetHotelFacilities(HttpContext context)
        {
            try
            {
                using (var db = new yudingEntities())
                {
                    var result = db.hotelfacilities_t.Where(x => x.hotelid == hotelcode).ToList();

                    if (result.Count > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "查询成功";
                        jsonResult.data = result;
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }

            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void GetRooms_detail(HttpContext context)
        {
            try
            {
                //var roomtype = Getpost("roomtype");
                var type =int.Parse( Getpost("type"));
                using (var db = new yudingEntities())
                {
                    var result = db.GetRooms.Where(x=> x.hotelid == hotelcode && x.type==type).ToList();
                        jsonResult.code = "200";
                        jsonResult.msg = "查询成功";
                        jsonResult.data = result;
                }

            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
            
        }
        private void GetPicDbByHotelcode(HttpContext context)
        {
            try
            {
                var type =Getpost("type");
                using (var db = new yudingEntities())
                {
                    if (type != "")
                    {
                        var typeParse = int.Parse(type);
                        var result = db.facilities_t.Where(x => x.hotelcode == hotelcode && x.type == typeParse).ToList();
                        if (result.Count > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "查询成功";
                            jsonResult.data = result;
                        }
                        else
                        {
                            jsonResult.code = "502";
                            jsonResult.msg = "查询成功";
                        }
                    }
                    else
                    {
                        var result = db.facilities_t.Where(x => x.hotelcode == hotelcode ).ToList();
                        if (result.Count > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "查询成功";
                            jsonResult.data = result;
                        }
                        else
                        {
                            jsonResult.code = "502";
                            jsonResult.msg = "查询成功";
                        }
                    }
                   
                  
                }

            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void AddPicDb(HttpContext context)
        {
            try
            {
                
                var type = Getpost("type");
                var addname = Getpost("addname");
                var codename = Getpost("codename");
                
                var piccode = Getpost("code");
                var picimg = Getpost("picimg");
               // var IncreaseData = context.Request.Form["PicDbData"];
                using (var db = new yudingEntities())
                {
                    facilities_t pic = new facilities_t()
                    {
                        img=picimg,
                        hotelcode=hotelcode,
                        code=piccode,
                        type=int.Parse(type),
                        codename = codename,
                        addname = addname,
                    };
                    db.facilities_t.Add(pic);
                    addLog(db, hotelcode, oid, yemian, operation, codename);
                        if (db.SaveChanges() > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "添加或修改成功";
                        }
                        else
                        {
                            jsonResult.code = "500";
                            jsonResult.msg = "添加失败";
                        }
                  
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void DelRoomDisplayByBatch(HttpContext context)
        {
            try
            {
                var batch = Getpost("batch");
                var roomtype = Getpost("roomtype");
                using (var db = new yudingEntities())
                {
                    var result = db.roomtimestart_t.Where(x => x.batch == batch).ToList();
                    foreach (var item in result)
                    {
                        db.roomtimestart_t.Remove(item);
                    }
                    if (db.SaveChanges()> 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "删除成功";
                        var count = db.roomtimestart_t.Where(x => x.roomtype == roomtype).ToList();
                        if (count.Count == 0)
                        {
                            var data = db.newroom_t.Where(x => x.roomtype == roomtype).FirstOrDefault();
                            data.state = "0";
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void GetRoomDisplayByBatch(HttpContext context)
        {
            try
            {
                var roomtype = Getpost("batch");
                using (var db = new yudingEntities())
                {

                    var result = db.roomtimestart_t.Where(x => x.batch == roomtype).ToList();
                    if (result.Count > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "查询成功";
                        jsonResult.data = result;
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void GetRoomDisplayGroup(HttpContext context)
        {
            try
            {
                var roomtype = Getpost("roomtype");
                using (var db = new yudingEntities())
                {
                    var result = db.roomtimestart_t.Where(x => x.roomtype == roomtype).GroupBy(x => x.batch).ToList();
                    var a = new List<roomtimestart_t>();
                    foreach (var item in result)
                    {
                        a.Add(item.FirstOrDefault());
                    }
                    if (result!=null)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "查询成功";
                        jsonResult.data = a;
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }

            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void StopRoomDisplay(HttpContext context)
        {
            try
            {
                var roomtype = Getpost("roomtype");
                var flag = int.Parse(context.Request.Form["flag"]);
                using (var db = new yudingEntities())
                {
                    var result = db.roomtimestart_t.Where(x => x.roomtype == roomtype).ToList();
                    foreach (var item in result)
                    {
                        item.flag = flag;
                    }
                    if (db.SaveChanges() >0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "更新成功";
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void StopRoom(HttpContext context)
        {
            try
            {
                var id = int.Parse(context.Request.Form["id"]);
                var flag = int.Parse(context.Request.Form["flag"]);
                using (var db = new yudingEntities())
                {
                    var data = db.newroom_t.FirstOrDefault(x => x.id == id);
                    if (data != null)
                    {
                        addLog(db, hotelcode, oid, yemian, operation, flag.ToString(), data.flag.ToString());
                        data.flag = flag;
                        //var logJson = context.Request.Form["logJson"];
                        //var log = JsonConvert.DeserializeObject<newback>(logJson);
                        //log.hotelid = hotelcode;
                        //log.olddata = data.flag.ToString();
                        //log.newdata = data.flag.ToString();
                        //db.SaveChanges();
                        //db.newbacks.Add(log);
                       
                        db.SaveChanges();
                        jsonResult.code = "200";
                        jsonResult.msg = "更新成功";
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "没有实体数据";
 
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void AddOrUpdateClassPic(HttpContext context)
        {
            try
            {
                var IncreaseData = context.Request.Form["ClassPicData"];
                using (var db = new yudingEntities())
                {
                    if (IncreaseData != null)
                    {
                        var increase = JsonConvert.DeserializeObject<PicClass>(IncreaseData);
                        var data = db.PicClasses.FirstOrDefault(x => x.id == increase.id);
                        if (data != null)
                        {
                            db.PicClasses.AddOrUpdate(increase);
                            addLog(db, hotelcode, oid, yemian, operation, increase.pic, data.pic);
                        }
                        else
                        {
                            db.PicClasses.AddOrUpdate(increase);
                            addLog(db, hotelcode, oid, yemian, operation, increase.pic);
                        }
                        if (db.SaveChanges() > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "添加或修改成功";
                        }
                        else
                        {
                            jsonResult.code = "500";
                            jsonResult.msg = "添加失败";
                        }
                    }
                    else
                    {
                        jsonResult.code = "500";
                        jsonResult.msg = "缺少参数";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void GetClassPicByType(HttpContext context)
        {
            try
            {
                var type = int.Parse(context.Request.Form["type"]);
                using (var db = new yudingEntities())
                {
                    var result = db.PicClasses.Where(x => x.hotelcode == hotelcode && type == x.type).ToList();
                    if (result.Count > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "获取成功";
                        jsonResult.data = result;
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "没有该实体数据";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }     
        private void everydate_price_t_one_list(HttpContext context)
        {
            try
            {
                var starttime =DateTime.Parse( Getpost("starttime"));
                var endtime =DateTime.Parse( Getpost("endtime"));
                var xzcode = Getpost("xz_code");
                using (var db = new yudingEntities())
                {
                    var result = db.everydate_price_t.Where(x => x.hotelid == hotelcode&&x.xz_code==xzcode&&x.everydate>=starttime&&x.everydate<=endtime).ToList();
                    if (result.Count > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "获取成功";
                        jsonResult.data = result;
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "没有该实体数据";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void AddOrUpdateActivity(HttpContext context)
        {
            try
            {
                var IncreaseData = context.Request.Form["ActivityData"];
                using (var db = new yudingEntities())
                {
                    if (IncreaseData != null)
                    {
                        var increase = JsonConvert.DeserializeObject<Activity>(IncreaseData);
                        var data = db.Activities.AsNoTracking().FirstOrDefault(x => x.Id == increase.Id);
                        if (data != null)
                        {
                            db.Activities.Attach(increase);
                            db.Activities.AddOrUpdate(increase);
                            var stateEntry = ((IObjectContextAdapter)db).ObjectContext.
                             ObjectStateManager.GetObjectStateEntry(increase);
                            stateEntry.SetModifiedProperty("Note");
                            stateEntry.SetModifiedProperty("discount");
                            stateEntry.SetModifiedProperty("pic");
                            stateEntry.SetModifiedProperty("px");
                            stateEntry.SetModifiedProperty("starttime");
                            stateEntry.SetModifiedProperty("endtime");
                            addLog(db, hotelcode, oid, yemian, operation, increase.Note, data.Note);
                        }
                        else
                        {
                            db.Activities.AddOrUpdate(increase);
                            addLog(db, hotelcode, oid, yemian, operation, increase.Note);
                        }
                        if (db.SaveChanges() > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "添加或修改成功";
                        }
                        else
                        {
                            jsonResult.code = "500";
                            jsonResult.msg = "添加失败";
                        }
                    }
                    else
                    {
                        jsonResult.code = "500";
                        jsonResult.msg = "缺少参数";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void GetActivityList(HttpContext context)
        {
            
            var page = context.Request.Form["page"];
            var flag =int.Parse( context.Request.Form["flag"]);
            var size = int.Parse(context.Request.Form["size"]);
            using (var db = new yudingEntities())
            {
                var q = (from a in db.Activities where a.hotelcode == hotelcode &&a.flag==flag select a).ToList();
                if (q.Count > 0)
                {
                    var totalcount = q.ToList().Count.ToString();
                    int count = Convert.ToInt32(totalcount);
                    totalcount = (count / size + (count % size > 0 ? 1 : 0)).ToString();
                    int pageindex = int.Parse(page);
                    jsonResult.data = q.OrderByDescending(x => x.px).Skip((pageindex - 1) * size).Take(size).ToList();
                    jsonResult.code = "200";
                    jsonResult.msg = totalcount;
                }
                else
                {
                    jsonResult.code = "200";
                    jsonResult.msg = "0";
                }
            }
        }
        private void SetEveryDate_Price(HttpContext context)
        {
            //{
            //        "starttime": "2017-08-09",
            //        "endtime": null,
            //        "xz_code": [
            //            "sss",
            //            "bbb"
            //        ]
            //    }
            var postStr = context.Request.Form["postStr"];
            var type = int.Parse(Getpost("type"));
            var data = JsonConvert.DeserializeObject<EveryDatePrice>(postStr);
            using (var db = new yudingEntities())
            {
                foreach (var item in data.xz_code)
                {
                    for (var start = DateTime.Parse(data.starttime); start < DateTime.Parse(data.endtime).AddDays(1); start = start.AddDays(1))
                    {
                        var index = (int)start.DayOfWeek;
                        var result = db.rateroom_xz.Where(x => x.hotelid == hotelcode && x.xz_code == item && x.WeekIndex == index).FirstOrDefault();
                        var price = result.price;
                        var everydate1 = db.everydate_price_t.Where(x => x.xz_code == item && x.everydate == start).FirstOrDefault();
                        if (everydate1 != null)
                        {
                            var ass = db.associateds.Where(x => x.xz_code == item && x.hotelid == hotelcode).FirstOrDefault();
                            if (ass != null)
                            {
                                var assParse = int.Parse(ass.codeid);
                                if (ass.rules == "early")
                                {
                                    var rule = db.earlies.Where(x => x.id == assParse && hotelcode == x.hotelid).FirstOrDefault();
                                    if (rule != null)
                                    {
                                        var discount = decimal.Parse(rule.discount);
                                        if (discount > 1)
                                        {
                                            price = price - discount;
                                        }
                                        else
                                        {
                                            price = price * discount;
                                        }
                                        
                                    }
                                }
                                if (ass.rules == "live")
                                {
                                    var rule = db.lives.Where(x => x.id == assParse && hotelcode == x.hotelid).FirstOrDefault();
                                    if (rule != null)
                                    {
                                        var discount = decimal.Parse(rule.discount);
                                        if (discount > 1)
                                        {
                                            price = price - discount;
                                        }
                                        else
                                        {
                                            price = price * discount;
                                        }
                                    }
                                }
                                addLog(db, hotelcode, oid, yemian, operation, "细则代码:" + item + "，细则名称:" + result.xz_name + "，价格：" + price + "," + "数量：" + "," + result.num + "," + "是否含早:" + result.package, "细则代码:" + item + "，细则名称:" + result.xz_name + "价格：" + everydate1.price + "," + "数量：" + "," + everydate1.num + "," + "是否含早:" + everydate1.package);
                                everydate1.price = price;
                                everydate1.num = result.num;
                                everydate1.package = result.package;
                                db.SaveChanges();
                            }
                            else
                            {
                                addLog(db, hotelcode, oid, yemian, operation, "细则代码:" + item + "，细则名称:" + result.xz_name + "价格：" + result.price + "," + "数量：" + "," + result.num + "," + "是否含早:" + result.package, "细则代码:" + item + "，细则名称:" + result.xz_name + "价格：" + everydate1.price + "," + "数量：" + "," + everydate1.num + "," + "是否含早:" + everydate1.package);
                                everydate1.price = result.price;
                                everydate1.num = result.num;
                                everydate1.package = result.package;
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            #region 为空
                            var ass = db.associateds.Where(x => x.xz_code == item && x.hotelid == hotelcode).FirstOrDefault();
                            if (ass != null)
                            {
                                var assParse = int.Parse(ass.codeid);
                                if (ass.rules == "early")
                                {
                                    var rule = db.earlies.Where(x => x.id == assParse && hotelcode == x.hotelid).FirstOrDefault();
                                    if (rule != null)
                                    {
                                        var discount = decimal.Parse(rule.discount);
                                        if (discount > 1)
                                        {
                                            price = price - discount;
                                        }
                                        else
                                        {
                                            price = price * discount;
                                        }
                                    }
                                }
                                if (ass.rules == "live")
                                {
                                    var rule = db.lives.Where(x => x.id == assParse && hotelcode == x.hotelid).FirstOrDefault();
                                    if (rule != null)
                                    {
                                        var discount = decimal.Parse(rule.discount);

                                        if (discount > 1)
                                        {
                                            price = price - discount;
                                        }
                                        else
                                        {
                                            price = price * discount;
                                        }
                                      
                                    }
                                }
                                var everydate = new everydate_price_t()
                                {
                                    xz_code = result.xz_code,
                                    price = price,
                                    package = result.package,
                                    num = result.num,
                                    everydate = start,
                                    hotelid = hotelcode,
                                    addname = oid,
                                    addtime = DateTime.Now,
                                };
                                addLog(db, hotelcode, oid, yemian, operation, "细则代码:" + item + "，细则名称:" + result.xz_name + "价格：" + price + "," + "数量：" + "," + result.num + "," + "是否含早:" + result.package);
                                db.everydate_price_t.Add(everydate);
                                db.SaveChanges();
                            }
                            else
                            {
                                var everydate = new everydate_price_t()
                                {
                                    xz_code = result.xz_code,
                                    price = result.price,
                                    package = result.package,
                                    num = result.num,
                                    everydate = start,
                                    hotelid = hotelcode,
                                    addname = oid,
                                    addtime = DateTime.Now,
                                };
                                addLog(db, hotelcode, oid, yemian, operation, "细则代码:" + item + "，细则名称:" + result.xz_name + "价格：" + result.price + "," + "数量：" + "," + result.num + "," + "是否含早:" + result.package);
                                db.everydate_price_t.Add(everydate);
                                db.SaveChanges();
                            }
                            #endregion
                        }
                        //BookingWeb.Refresh(db, item, hotelcode, type, start);
                    }                
                }
                jsonResult.code = "200";
                jsonResult.msg = "成功";
            }
        }
        private void GetOrderListByConditions(HttpContext context)
        {
            JsonReturnPagination json = new JsonReturnPagination();
            var page = context.Request.Form["page"];
            var orderid = context.Request.Form["orderid"];
            var tel = context.Request.Form["tel"];
            var name = context.Request.Form["name"];
            var ispay = context.Request.Form["ispay"];
            var arrivetime = context.Request.Form["arrivetime"];
            var channelid = context.Request.Form["channelid"];
            var addtime = Getpost("addtime");
            var Type =int.Parse(Getpost("Type"));
            var hotelname = context.Request.Form["hotelname"];
            using (var db = new yudingEntities())
            {
                var q = from a in db.order_t where a.type==Type select a;
                if (hotelcode == "")
                {
                  
                }
                else
                {
                    q = q.Where(x => x.hotelcode == hotelcode);
                }
                if (hotelname != "")
                {
                    q = q.Where(x => SqlFunctions.PatIndex("%" + hotelname + "%", x.hotelname) > 0);
                }
                if (addtime != "")
                {
                    var time = addtime.Split(',');
                    var t1 = DateTime.Parse(time[0]);
                    var t2= DateTime.Parse(time[1]);
                    q = q.Where(x => x.addtime >= t1 && x.addtime <= t2);
                }
                if (orderid != "")
                {
                    q = q.Where(x => x.ordernumber == orderid);
                }
                if (channelid != "")
                {
                    q = q.Where(x => x.channelid == channelid);
                }
                if (tel != "")
                {
                    q = q.Where(x => x.contact_mobile == tel);
                }
                if (name != "")
                {
                    q = q.Where(x => x.contact_name == name);
                }
                if (ispay != "")
                {
                    var ispayParse = int.Parse(ispay);
                    q = q.Where(x => x.ispay == ispayParse);
                }
                if (arrivetime != "")
                {
                    var time = arrivetime.Split(',');
                    var t1 = DateTime.Parse(time[0]);
                    var t2 = DateTime.Parse(time[1]);
                    q = q.Where(x => x.arrivetime>= t1&&x.arrivetime<=t2);
                }
                if (q.ToList().Count > 0)
                {
                    var totalcount = q.ToList().Count.ToString();
                    int count = Convert.ToInt32(totalcount);
                   // totalcount = (count / rows + (count % rows > 0 ? 1 : 0)).ToString();
                    jsonResult.code = "200";
                    json.pagecount = totalcount;
                    int pageindex = int.Parse(page);
                    if (pageindex == 0)
                    {
                        jsonResult.data = q.OrderByDescending(x => x.addtime).ToList();
                    }
                    else
                    {
                        jsonResult.data = q.OrderByDescending(x => x.addtime).Skip((pageindex - 1) * rows).Take(rows).ToList();
                        jsonResult.msg = totalcount;
                    }
                    //HttpHepler.ReturnJson<JsonReturnPagination>(json, context);
                }
                else
                {
                    jsonResult.code = "502";
                    jsonResult.msg = "没有该实体数据";
                }
            }
        }
        private void GetOrderList(HttpContext context)
        {
            JsonReturnPagination json = new JsonReturnPagination();
            var page = context.Request.Form["page"];
            using (var db = new yudingEntities())
            {
                var q = (from a in db.order_t where a.hotelcode == hotelcode select a).ToList();
                if (q.Count > 0)
                {
                    var totalcount = q.ToList().Count.ToString();
                    int count = Convert.ToInt32(totalcount);
                    totalcount = (count / rows + (count % rows > 0 ? 1 : 0)).ToString();
                    json.pagecount = totalcount;
                    int pageindex = int.Parse(page);
                    json.data = q.OrderByDescending(x => x.addtime).Skip((pageindex - 1) * rows).Take(rows);
                    json.pageindex = page;
                    HttpHepler.ReturnJson<JsonReturnPagination>(json, context);
                }
                else
                {
                    jsonResult.code = "502";
                    jsonResult.msg = "没有该实体数据";
                }
            }
        }
        private void RateCodeIncreaseList(HttpContext context)
        {
            try
            {
                var roomtype = context.Request.Form["roomtype"];
                using (var db = new yudingEntities())
                {
                    var p = (from b in db.increase_link where b.roomtype == roomtype select b.increase).ToList();
                    var result = db.increases.Where(c => p.Contains(c.increase_code)).ToList();
                    if (result.Count > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "获取成功";
                        jsonResult.data = result;
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "没有该实体数据";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void RelateIncreaseAndRateCode(HttpContext context)
        {
            try
            {
                var IncreaseData = context.Request.Form["IncreaseData"];
                using (var db = new yudingEntities())
                {
                    if (IncreaseData != null)
                    {
                        var increase = JsonConvert.DeserializeObject<increase_link>(IncreaseData);
                        var data = db.increase_link.AsNoTracking().FirstOrDefault(x => x.id == increase.id);
                        if (data != null)
                        {
                          
                        }
                        else
                        {
                            db.increase_link.AddOrUpdate(increase);
                            addLog(db, hotelcode, oid, yemian, operation, increase.roomtype+","+increase.increase);
                        }
                        if (db.SaveChanges() > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "添加或修改成功";
                        }
                        else
                        {
                            jsonResult.code = "500";
                            jsonResult.msg = "无修改或添加";
                        }
                    }
                    else
                    {
                        jsonResult.code = "500";
                        jsonResult.msg = "缺少参数";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
          
        }
        private void AddOrUpdateIncrease(HttpContext context)
        {
            try
            {
                var IncreaseData = context.Request.Form["IncreaseData"];
                using (var db = new yudingEntities())
                {
                    if (IncreaseData != null)
                    {
                        var increase = JsonConvert.DeserializeObject<increase>(IncreaseData);
                        var data = db.increases.AsNoTracking().FirstOrDefault(x => x.id == increase.id);
                        if (data != null)
                        {
                            db.increases.Attach(increase);
                            var stateEntry = ((IObjectContextAdapter)db).ObjectContext.
                          ObjectStateManager.GetObjectStateEntry(increase);
                            //stateEntry.SetModifiedProperty("increase_code");
                            stateEntry.SetModifiedProperty("describe");
                            stateEntry.SetModifiedProperty("money");
                            stateEntry.SetModifiedProperty("instructions");
                           
                            db.increases.AddOrUpdate(increase);
                            addLog(db, hotelcode, oid, yemian, operation, increase.money, data.money);
                        }
                        else
                        {
                            db.increases.AddOrUpdate(increase);
                            addLog(db, hotelcode, oid, yemian, operation, increase.money);
                        }
                        if (db.SaveChanges() > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "添加或修改成功";
                        }
                        else
                        {
                            jsonResult.code = "500";
                            jsonResult.msg = "添加失败";
                        }
                    }
                    else
                    {
                        jsonResult.code = "500";
                        jsonResult.msg = "缺少参数";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void GetIncreaseList(HttpContext context)
        {
            try
            {
                var type = Getpost("roomtype");
                using (var db = new yudingEntities())
                {
                    var result = db.GetIncreases.Where(x => x.hotelid == hotelcode&&x.roomtype==type).OrderByDescending(x=>x.px).ToList();
                    if (result.Count > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "获取成功";
                        jsonResult.data = result;
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "没有该实体数据";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void LockRoom(HttpContext context)
        {
            try
            {
                var strat = DateTime.Parse( Getpost("starttime"));
                var end =DateTime.Parse( Getpost("endtime"));
                var roomtype = context.Request.Form["roomtype"];
                var flag = context.Request.Form["flag"];
                if (roomtype != null && flag != null )
                {
                    using (var db = new yudingEntities())
                    {
                       
                        var roomlist = Getpost("roomtype").Split(',');
                        foreach (var item in roomlist)
                        {
                            var list = db.everydate_price_t.Where(x => x.everydate >= strat && x.everydate <= end).ToList();
                            foreach (var i in list)
                            {

                            }
                        }
                        
                       
                    }
                }
                else
                {
                    jsonResult.code = "503";
                    jsonResult.msg = "缺少参数";
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }          
        }
        private void AddOrUpdateOpenId(HttpContext context)
        {
            try
            {
                var OpenIdData = context.Request.Form["OpenIdData"];
                using (var db = new yudingEntities())
                {
                    if (OpenIdData != null)
                    {
                        var openid = JsonConvert.DeserializeObject<openid>(OpenIdData);
                        var data = db.openids.FirstOrDefault(x => x.id == openid.id);
                        if (data != null)
                        {
                            var stateEntry = ((IObjectContextAdapter)db).ObjectContext.
                      ObjectStateManager.GetObjectStateEntry(openid);
                            stateEntry.SetModifiedProperty("name");
                            stateEntry.SetModifiedProperty("tel");
                            db.openids.AddOrUpdate(openid);
                            addLog(db, hotelcode, oid, yemian, operation, openid.name,data.name);
                        }
                        else
                        {
                            db.openids.AddOrUpdate(openid);
                            addLog(db, hotelcode, oid, yemian, operation, openid.name);
                        }
                        if (db.SaveChanges() > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "添加或修改成功";
                        }
                        else
                        {
                            jsonResult.code = "500";
                            jsonResult.msg = "添加失败";
                        }
                    }
                    else
                    {
                        jsonResult.code = "500";
                        jsonResult.msg = "缺少参数";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
            
        }
        private void GetOpenIdList(HttpContext context)
        {
            try
            {
                var type =int.Parse( Getpost("type"));
                using (var db = new yudingEntities())
                { 
                        var result = db.openids.Where(x => x.hotelid == hotelcode&&x.type==type ).ToList();
                        if (result.Count > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "获取成功";
                            jsonResult.data = result;
                        }
                        else
                        {
                            jsonResult.code = "502";
                            jsonResult.msg = "没有该实体数据";
                        }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void AddOrUpdateYhCode(HttpContext context)
        {
            try
            {
                var yhcodename = Getpost("yhcodename");
                var yhcode = Getpost("yhcode");
                var tmoney = Getpost("tmoney");
                var flag = Getpost("flag");
                var type = Getpost("sysType");
                using (var db = new yudingEntities()) 
                {
                    var data = new yhcode_t
                    {
                        addname = oid,
                        flag = int.Parse(flag),
                        hotelcode = hotelcode,
                        tmoney = decimal.Parse(tmoney),
                        type = int.Parse(type),
                        yhcode = yhcode,
                        yhcodename = yhcodename,
                    };
                    addLog(db, hotelcode, oid, yemian, operation, yhcodename);
                    db.yhcode_t.Add(data);
                    db.SaveChanges();
                    jsonResult.code = "200";
                    jsonResult.msg = "成功";
                }
              
            }
            catch (Exception ex)
            {
                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void Getyhcode(HttpContext context)
        {
            try
            {
                using (var db = new yudingEntities())
                {
                    var type = context.Request.Form["type"];
                    if (type != null)
                    {
                        var typeParse = int.Parse(type);
                        var result = db.yhcode_t.Where(x => x.hotelcode == hotelcode && x.type == typeParse).ToList();
                        if (result.Count > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "获取成功";
                            jsonResult.data = result;
                        }
                        else
                        {
                            jsonResult.code = "502";
                            jsonResult.msg = "没有该实体数据";
                        }
                    }
                    else
                    {
                        jsonResult.code = "500";
                        jsonResult.msg = "缺少参数";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void AddOrUpdateRules(HttpContext context)
        {
            var table = context.Request.Form["table"];
            var postData = context.Request.Form["postData"];
            if (table != null)
            {
                using (var db = new yudingEntities())
                {
                    //多间
                    if (table == "duojian")
                    {
                        var result = JsonConvert.DeserializeObject<duojian_t>(postData);
                        var data = db.duojian_t.FirstOrDefault(x => x.id == result.id);
                        if (data != null)
                        {
                            db.duojian_t.AddOrUpdate(result);
                            jsonResult.code = "200";
                            jsonResult.msg = "成功";
                            //var logJson = context.Request.Form["logJson"];
                            //var log = JsonConvert.DeserializeObject<newback>(logJson);
                            //db.newbacks.Add(log);
                            //addLog(db, hotelcode, oid, yemian, operation, result.describe);
                        }
                        else
                        {
                            db.duojian_t.AddOrUpdate(result);
                            addLog(db, hotelcode, oid, yemian, operation, result.describe);
                            jsonResult.code = "200";
                            jsonResult.msg = "成功";
                        }
                        db.SaveChanges();
                    }
                    //连住
                    else if (table == "live")
                    {
                        var result = JsonConvert.DeserializeObject<live>(postData);
                        var data = db.lives.FirstOrDefault(x => x.id == result.id);
                        if (data != null)
                        {
                            result.hotelid = hotelcode;
                            db.lives.AddOrUpdate(result);
                            jsonResult.code = "200";
                            jsonResult.msg = "成功";
                        }
                        else
                        {
                            result.hotelid = hotelcode;
                            db.lives.AddOrUpdate(result);
                            addLog(db, hotelcode, oid, yemian, operation, result.note);
                            jsonResult.code = "200";
                            jsonResult.msg = "成功";
                        }
                        db.SaveChanges();
                    }
                    //提前
                    else if (table == "early")
                    {
                        var result = JsonConvert.DeserializeObject<early>(postData);
                        var data = db.earlies.FirstOrDefault(x => x.id == result.id);
                        if (data != null)
                        {
                            db.earlies.AddOrUpdate(result);
                            jsonResult.code = "200";
                            jsonResult.msg = "成功";
                        }
                        else
                        {
                            db.earlies.AddOrUpdate(result);
                            addLog(db, hotelcode, oid, yemian, operation, result.note);
                            jsonResult.code = "200";
                            jsonResult.msg = "成功";
                        }
                        db.SaveChanges();
                    }
                }
            }
        }
        private void GetRules_xz(HttpContext context)
        {
            var type = context.Request.Form["type"];
            var table = context.Request.Form["table"];
            try
            {
                using (var db = new yudingEntities())
                {
                    //提前
                    if(table=="early")
                    {
                        var typeParse=int.Parse(type);
                        var result = db.earlies.Where(x => x.hotelid == hotelcode && x.type == typeParse).ToList();
                        jsonResult.code = "200";
                        jsonResult.msg = "查询成功";
                        jsonResult.data = result;
                    }
                    //连住
                    else if (table == "live")
                    {
                        var typeParse = int.Parse(type);
                        var result = db.lives.Where(x => x.hotelid == hotelcode && x.type == typeParse).ToList();
                        jsonResult.code = "200";
                        jsonResult.msg = "查询成功";
                        jsonResult.data = result;
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }

        }
        private void GetRulesList(HttpContext context)
        {
            try
            {
                using (var db = new yudingEntities())
                {

                    var result = db.rules.ToList();
                    if (result.Count > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "查询成功";
                        jsonResult.data = result;
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }

            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void SetRateCodeDisplay_xz(HttpContext context) 
        {
            try
            {
                var data = context.Request.Form["index"];
                var result = JsonConvert.DeserializeObject<List<xztimestart_t>>(data);
                //var log = JsonConvert.DeserializeObject<newback>(logStr);
                if (result.Count > 0)
                {
                    string xzcode = "";
                    using (var db = new yudingEntities())
                    {
                        foreach (var item in result)
                        {
                            xzcode = item.xz_code;
                            addLog(db, hotelcode, oid, yemian, operation, item.xz_code);
                            db.xztimestart_t.Add(item);
                        }
                        db.SaveChanges();
                        jsonResult.code = "200";
                        jsonResult.msg = "添加成功";
                       
                        //foreach (var item in result)
                        //{
                        //    xzcode = item.xz_code;
                            
                        //    if (db.xztimestart_t.Any(x => x.batch == item.batch))
                        //    {
                        //        var xzitem = db.xztimestart_t.Where(x => x.batch == item.batch && x.xz_code == xzcode && x.week == item.week).FirstOrDefault();
                        //        addLog(db, hotelcode, oid, yemian, "修改房价码细则启用时间段", item.xz_code);
                        //        xzitem.startdate = item.startdate;
                        //        xzitem.enddate = item.enddate;
                        //        xzitem.flag = item.flag;

                        //    }
                        //    else
                        //    {
                        //        addLog(db, hotelcode, oid, yemian, operation, item.xz_code);
                        //        db.xztimestart_t.Add(item);
                        //    }
                            
                        //}
                        db.SaveChanges();
                        jsonResult.code = "200";
                        jsonResult.msg = "添加成功";
                        var istimeroom = db.xztimestart_t.Where(x => x.xz_code == xzcode).FirstOrDefault();
                        if (istimeroom != null)
                        {
                            var room = db.rateroom_xz.Where(x => x.xz_code == xzcode).ToList();
                            foreach (var item in room)
                            {
                                item.istime = 1;
                            }
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    jsonResult.code = "502";
                    jsonResult.msg = "无数据";
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void AddOrUpdateRateCode_Advanced(HttpContext context)
        {
            var baojia = int.Parse(Getpost("baojia") != "" ? Getpost("baojia") : "0");
            var rules = Getpost("rules");
            var codeid = Getpost("codeid");
            var activitycode = Getpost("activitycode");
            var categoryid = Getpost("formula");
            var xzcode = Getpost("xz_code");
            var starttime = Getpost("starttime");
            var endtime = Getpost("endtime");
            var onsalecode = Getpost("onsalecode");
            //state 0是可用票券，1可用订房
            var formulalist = JsonConvert.DeserializeObject<List<price_formulaid>>(categoryid);
            using (var db = new yudingEntities())
            {
                var activitydata = db.activity_link.Where(x => x.xz_code == xzcode).FirstOrDefault();
                if (activitydata == null)
                {
                    if (activitycode == "")
                    {
                    }
                    else
                    {
                        activity_link a = new activity_link()
                            {
                                xz_code = xzcode,
                                hotelcode = hotelcode,
                                activitycode = activitycode,
                            };
                        addLog(db, hotelcode, oid, yemian, "添加房价码细则的活动", "细则代码:" + xzcode + ",活动代码:" + activitycode);
                        db.activity_link.Add(a);
                        //addLog(db, hotelcode, oid, yemian, operation, xzcode + "," + activitycode);
                    }
                }
                else
                {
                    if (activitycode == "")
                    {
                        db.activity_link.Remove(activitydata);
                        addLog(db, hotelcode, oid, yemian, "删除活动", "", "细则代码:" + xzcode + ",活动代码:" + activitycode);
                    }
                    else
                    {
                        addLog(db, hotelcode, oid, yemian, operation, "细则代码:" + xzcode + ",活动代码:" + activitycode, "细则代码:" + xzcode + ",活动代码:" + activitydata.activitycode);
                        activitydata.activitycode = activitycode;
                    }
                }
                var baojiadata = db.baojia_t.Where(x => x.xz_code==xzcode).FirstOrDefault();
                if (baojiadata == null)
                {
                    if (baojia != 0)
                    {
                        baojia_t b = new baojia_t()
                        {
                            hotelid = hotelcode,
                            xz_code = xzcode,
                            formulaid = baojia,
                        };
                        db.baojia_t.Add(b);
                        addLog(db, hotelcode, oid, yemian, operation, "细则代码:" + xzcode+"包价："+ baojia.ToString());
                    }
                }
                else
                {
                    if (baojia == 0)
                    {
                        addLog(db, hotelcode, oid, yemian, "删除包价", "", "细则代码:" + xzcode+baojiadata.formulaid.ToString());
                        db.baojia_t.Remove(baojiadata);
                    }
                    else
                    {
                        addLog(db, hotelcode, oid, yemian, operation, "细则代码:" + xzcode + "包价:" + baojia.ToString(), "细则代码:" + xzcode + "包价:" + baojiadata.formulaid.ToString());
                        baojiadata.formulaid = baojia;
                    }
                   
                }
                var miaoshadata = db.miaosha_t.Where(x => x.xz_code == xzcode && x.hotelid == hotelcode).FirstOrDefault();
                if (miaoshadata == null)
                {
                    if (starttime != "" && endtime != "")
                    {
                        miaosha_t m = new miaosha_t()
                        {
                            hotelid = hotelcode,
                            starttime = starttime,
                            endtime = endtime,
                            xz_code = xzcode,
                        };
                        db.miaosha_t.Add(m);
                        addLog(db, hotelcode, oid, yemian, operation, "细则代码:" + xzcode + "抢购时间:"+starttime + "-" + endtime);
                    }
                }
                else
                {
                    if (starttime != "" && endtime != "")
                    {
                        addLog(db, hotelcode, oid, yemian, operation, "细则代码:" + xzcode + "抢购时间:" + starttime + "-" + endtime, "细则代码:" + xzcode + "抢购时间:" + miaoshadata.starttime + "-" + miaoshadata.endtime);
                        miaoshadata.starttime = starttime;
                        miaoshadata.endtime = endtime;
                    }
                    else
                    {
                        db.miaosha_t.Remove(miaoshadata);
                        addLog(db, hotelcode, oid, yemian, "删除抢购", "", "细则代码:" + xzcode + "抢购时间:" + miaoshadata.starttime + "-" + miaoshadata.endtime);
                    }

                }
                var rulesdata = db.associateds.Where(x => x.xz_code == xzcode).FirstOrDefault();
                if (rulesdata == null)
                {
                    if (rules != "")
                    {
                        associated a = new associated()
                        {
                            rules = rules,
                            xz_code = xzcode,
                            hotelid = hotelcode,
                            codeid = codeid,
                        };
                        db.associateds.Add(a);
                        addLog(db, hotelcode, oid, yemian, operation, "细则代码:" + xzcode +"规则:"+ rules + ",规则码" + codeid);
                    }
                }
                else
                {
                    if (codeid != "")
                    {
                        addLog(db, hotelcode, oid, yemian, operation, "细则代码:" + xzcode + "规则:" + rules + ",规则码" + codeid, "细则代码:" + xzcode + "规则:" + rulesdata.rules + ",规则码" + rulesdata.codeid);
                        rulesdata.rules = rules;
                        rulesdata.codeid = codeid;
                    }
                    else
                    {
                        db.associateds.Remove(rulesdata);
                        addLog(db, hotelcode, oid, yemian, operation, "", "细则代码:" + xzcode + "规则:" + rulesdata.rules + ",规则码" + rulesdata.codeid);
                    }

                }
                var list = db.price_formulaid.Where(x => x.xz_code == xzcode ).ToList();
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        db.price_formulaid.Remove(item);
                        addLog(db, hotelcode, oid, yemian, "删除可使用产品", "", "细则代码:" + xzcode + ",产品:" + item.formulaid + ",子劵:" + item.categoryid);
                    }
                    if (formulalist != null)
                    {
                        foreach (var item in formulalist)
                        {
                            addLog(db, hotelcode, oid, yemian, "添加可使用产品", "", "细则代码:" + xzcode + ",产品:" + item.formulaid + ",子劵:" + item.categoryid);
                            db.price_formulaid.Add(item);
                        }
                    }
                }
                else
                {
                    if (formulalist != null)
                    {
                        foreach (var item in formulalist)
                        {
                            addLog(db, hotelcode, oid, yemian, "添加可使用产品", "", "细则代码:" + xzcode + ",产品:" + item.formulaid + ",子劵:" + item.categoryid);
                            db.price_formulaid.Add(item);
                        }
                    }
                    else
                    {
                        
 
                    }
                }
              
                if (db.SaveChanges() > 0)
                {
                    var data = db.rateroom_xz.Where(x => x.xz_code == xzcode).ToList();
                    foreach (var item in data)
                    {
                        item.onsalecode = onsalecode;
                        item.state = "1";
                    }
                    db.SaveChanges();
                }
                //if (activitydata == null && baojiadata == null && starttime == "" && endtime == "" && rulesdata == null && list.Count == 0 )
                //{
                //    var data = db.rateroom_xz.Where(x => x.xz_code == xzcode).ToList();
                //    foreach (var item in data)
                //    {
                //        item.state = "0";
                //    }
                //    db.SaveChanges();
 
                //}
                if (activitycode == "" && baojia== 0 && rules == "" && starttime =="" &&endtime==""&&formulalist==null)
                {
                    var data = db.rateroom_xz.Where(x => x.xz_code == xzcode).ToList();
                    foreach (var item in data)
                    {
                        item.onsalecode = onsalecode;
                        item.state = "0";
                    }
                    db.SaveChanges();
                }
                jsonResult.code = "200";
                jsonResult.msg = "添加或修改成功";
            }
        }
        private void AddOrUpdateRateCode_xz(HttpContext context)
        {
            var id = int.Parse(Getpost("id"));
            var xz_name = Getpost("xz_name");
            var payway = Getpost("payway");
            var qudao = Getpost("qudao");
            var pmsratecode = Getpost("pmsratecode");
            var package = Getpost("package");
            var px = Getpost("px");
            var pay = Getpost("pay");
            var weekstr = Getpost("week");
            var roomtype = Getpost("roomtype");
            var ratecode = Getpost("ratecode");
            var xzcode = Getpost("xz_code");
            var chaifen =int.Parse(Getpost("chaifen"));
            var cusno = Getpost("cusno");
           // var onsalecode = Getpost("onsalecode");
            var result = JsonConvert.DeserializeObject<List<yuding.JsonRequest.week>>(weekstr);
            using (var db = new yudingEntities())
            {
                var data = db.rateroom_xz.Where(x => x.xz_code == xzcode).ToList();
                if (data.Count<= 0)
                {
                    foreach (var item in result)
                    {
                        rateroom_xz r = new rateroom_xz()
                        {
                            //onsalecode=onsalecode,
                            payway=payway,
                            hotelid = hotelcode,
                            num = int.Parse(item.num),
                            package = int.Parse(package),
                            pay = int.Parse(pay),
                            pmsratecode = pmsratecode,
                            qudao = qudao,
                            ratecode = ratecode,
                            roomtype = roomtype,
                            px = int.Parse(px),
                            xz_code=xzcode,
                            xz_name = xz_name,
                            WeekIndex = item.index,
                            price =decimal.Parse( item.price),
                            state="0",
                            istime=0,
                            chaifen = chaifen,
                            cusno = cusno,
                        };
                        addLog(db, hotelcode, oid, yemian, "生成房价码细则", "细则名称:" + xz_name + ",细则代码：" + xzcode + ",房型代码:" + roomtype + "," + "价格:" + item.price);
                       // addLog(db, hotelcode, oid, yemian, operation, xz_name);
                        db.rateroom_xz.Add(r);
                    }
                    db.SaveChanges();
                    jsonResult.code = "200";
                    jsonResult.msg = "添加成功";
                }
                else
                {
                    var state = "";
                    Nullable<int> istime = 0;
                    Nullable<int> flag = 0;
                    foreach (var item in data)
                    {
                        flag = item.flag;
                        istime = item.istime;
                        state = item.state;

                        addLog(db, hotelcode, oid, yemian, "删除细则", "", "细则名称:" + item.xz_name +",细则代码："+item.xz_code+ ",房型代码:" + item.roomtype+"," + "价格:" + item.price);
                        db.rateroom_xz.Remove(item);
                    }
                   
                    foreach (var item in result)
                    {
                        rateroom_xz r = new rateroom_xz()
                        {
                            payway = payway,
                            hotelid = hotelcode,
                            num = int.Parse(item.num),
                            package = int.Parse(package),
                            pay = int.Parse(pay),
                            pmsratecode = pmsratecode,
                            qudao = qudao,
                            ratecode = ratecode,
                            roomtype = roomtype,
                            px = int.Parse(px),
                            WeekIndex=item.index,
                            price=decimal .Parse( item.price),
                            xz_code = xzcode,
                            xz_name = xz_name,
                            state=state,
                            istime = istime,
                            flag=flag.Value,
                            chaifen=chaifen,
                            cusno = cusno,
                            //onsalecode = onsalecode

                        };
                        addLog(db, hotelcode, oid, yemian, "重新生成房价码细则", "细则名称:" + xz_name + ",细则代码：" + xzcode + ",房型代码:" + roomtype + "," + "价格:" + item.price);
                        db.rateroom_xz.Add(r);
                    }
                    db.SaveChanges();
                    jsonResult.code = "200";
                    jsonResult.msg = "修改成功";
                }
            }
        }
        private void GetScenceCode(HttpContext context)
        {
            try
            {
                    using (var db = new yudingEntities())
                    {
                        var type = int.Parse(Getpost("type"));
                        var result = db.ratecode_t.Where(x => x.hotelid == hotelcode &&x.type==type).ToList();
                        if (result.Count > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "查询成功";
                            jsonResult.data = result;
                        }
                        else
                        {
                            jsonResult.code = "502";
                            jsonResult.msg = "无数据";
                        }
                    }
                }
            catch (Exception)
            {

                throw;
            }
          
        }
        private void AddOrUpdateScenceCode(HttpContext context)
        {
            //微信是1，预定义是0，pc是2
            try
            {
                var ratecodename = Getpost("ratecodename");
                var ratecode = Getpost("ratecode");
                var qudao = Getpost("qudao");
                var pmsratecode = Getpost("pmsratecode");
                var scenario = int.Parse(Getpost("scenario"));
                var yh_code = Getpost("yhcode");
                //var type = int.Parse(Getpost("type"));
                var mobanratecode = Getpost("mobanratecode");
                var sysType =int.Parse( Getpost("sysType"));
                using (var db = new yudingEntities())
                {
                    var isexist = db.ratecode_t.FirstOrDefault(x => x.ratecode == ratecode);
                    if (isexist == null)
                    {
                        if (scenario == 1)
                        {
                            var ratecodelist = db.ratecode_t.Where(x=>x.scenario==1&&x.hotelid==hotelcode).FirstOrDefault();
                            if (ratecodelist != null)
                            {
                                ratecodelist.scenario = 0;
                                db.SaveChanges();
                            }
                        }
                        var data = new ratecode_t
                        {
                            hotelid = hotelcode,
                            qudao=qudao,
                            pmsratecode=pmsratecode,
                            ratecode = ratecode,
                            ratecodename = ratecodename,
                            scenario = scenario,
                            type = sysType
                        };
                        addLog(db, hotelcode, oid, yemian, operation, "情景：" + ratecodename);
                        db.ratecode_t.Add(data);
                        if (mobanratecode != "")
                        {
                            var tmoney = db.yhcode_t.Where(x => x.yhcode == yh_code).FirstOrDefault();
                            //var rate = db.ratecode_t.Where(x => x.ratecode == mobanratecode).FirstOrDefault();
                            var xz_list = db.rateroom_xz.Where(x => x.ratecode == mobanratecode).GroupBy(x => x.xz_code).ToList();
                           
                                foreach (var item in xz_list)
                                {
                                    var xzcode = OrderHelper.GetRandom1(hotelcode);

                                    foreach (var item1 in item)
                                    {
                                        if (tmoney != null)
                                        {
                                            var xz_data = new rateroom_xz
                                            {
                                                duojian = item1.duojian,
                                                flag = item1.flag,
                                                hotelid = item1.hotelid,
                                                num = item1.num,
                                                package = item1.package,
                                                pay = item1.pay,
                                                price = tmoney.tmoney > 1 ? item1.price - tmoney.tmoney : tmoney.tmoney * item1.price,
                                                px = item1.px,
                                                qxxz = item1.qxxz,
                                                ratecode = ratecode,
                                                ratecodename = item1.ratecodename,
                                                roomtype = item1.roomtype,
                                                roomtypename = item1.roomtypename,
                                                WeekIndex = item1.WeekIndex,
                                                xz_code = xzcode,
                                                xz_name = item1.xz_name,
                                                ydxz = item1.ydxz,
                                                state = item1.state,
                                                istime = item1.istime,
                                                chaifen=item1.chaifen,
                                                cusno=item1.cusno,
                                            };
                                            addLog(db, hotelcode, oid, yemian, "复制情景码下的房价码细则", item1.xz_name);
                                            db.rateroom_xz.Add(xz_data);
                                        }
                                        else
                                        {
                                            var xz_data1 = new rateroom_xz
                                            {
                                                duojian = item1.duojian,
                                                flag = item1.flag,
                                                hotelid = item1.hotelid,
                                                num = item1.num,
                                                package = item1.package,
                                                pay = item1.pay,
                                                price = item1.price,
                                                px = item1.px,
                                                qxxz = item1.qxxz,
                                                ratecode = ratecode,
                                                ratecodename = item1.ratecodename,
                                                roomtype = item1.roomtype,
                                                roomtypename = item1.roomtypename,
                                                WeekIndex = item1.WeekIndex,
                                                xz_code = xzcode,
                                                xz_name = item1.xz_name,
                                                ydxz = item1.ydxz,
                                                state = item1.state,
                                                istime = item1.istime,
                                                chaifen=item1.chaifen,
                                                cusno=item1.cusno,
                                            };
                                            addLog(db, hotelcode, oid, yemian, "复制情景码下的房价码细则", item1.xz_name);
                                            db.rateroom_xz.Add(xz_data1);
                                        }
                                    }
                                    var oldxzcode = item.FirstOrDefault().xz_code;
                                    var associate = db.associateds.Where(x => x.xz_code == oldxzcode && x.hotelid == hotelcode).ToList();
                                    if (associate.Count > 0)
                                    {
                                        foreach (var ass in associate)
                                        {
                                            associated ass1 = new associated()
                                            {
                                                xz_code = xzcode,
                                                rules = ass.rules,
                                                hotelid = hotelcode,
                                                codeid = ass.codeid,
                                                flag = ass.flag,
                                            };
                                            addLog(db, hotelcode, oid, yemian, "复制情景码下的规则", "房价码:" + xzcode + ",规则：" + ass.rules);
                                            db.associateds.Add(ass1);
                                        }
                                    }
                                    var baojia = db.baojia_t.Where(x => x.xz_code == oldxzcode && x.hotelid == hotelcode).ToList();
                                    if (baojia.Count > 0)
                                    {
                                        foreach (var bj in baojia)
                                        {
                                            baojia_t b = new baojia_t()
                                            {
                                                xz_code = xzcode,
                                                hotelid = hotelcode,
                                                formulaid = bj.formulaid,
                                            };
                                            addLog(db, hotelcode, oid, yemian, "复制情景码下的包价", "房价码:" + xzcode + ",包价：" + bj.formulaid);
                                            db.baojia_t.Add(b);
                                        }
                                    }
                                    var miaosha = db.miaosha_t.Where(x => x.xz_code == oldxzcode && x.hotelid == hotelcode).ToList();
                                    if (miaosha.Count > 0)
                                    {
                                        foreach (var ms in miaosha)
                                        {
                                            miaosha_t m = new miaosha_t()
                                            {
                                                xz_code = xzcode,
                                                starttime = ms.starttime,
                                                endtime = ms.endtime,
                                                hotelid = hotelcode,
                                            };
                                            addLog(db, hotelcode, oid, yemian, "复制情景码下的限时抢购", "房价码:" + xzcode + ",限时抢购：" + ms.starttime + "-" + ms.endtime);
                                            db.miaosha_t.Add(m);
                                        }
                                    }
                                    var price_formulaid_list = db.price_formulaid.Where(x => x.xz_code == oldxzcode && x.hotelid == hotelcode).ToList();
                                    if (price_formulaid_list.Count > 0)
                                    {
                                        foreach (var pf in price_formulaid_list)
                                        {
                                            price_formulaid p = new price_formulaid()
                                            {
                                                xz_code = xzcode,
                                                hotelid = hotelcode,
                                                formulaid = pf.formulaid,
                                                categoryid = pf.categoryid,
                                            };
                                            addLog(db, hotelcode, oid, yemian, "复制情景码下的可使用票券", "房价码:" + xzcode + ",产品包：" + pf.formulaid + ",子劵：" + pf.categoryid);
                                            db.price_formulaid.Add(p);
                                        }
                                    }

                                }
                            
                            db.SaveChanges();
                            jsonResult.code = "200";
                            jsonResult.msg = "添加成功";
                        }
                        else
                        {
                            db.SaveChanges();
                            jsonResult.code = "200";
                            jsonResult.msg = "添加成功";

                        }
                    }
                    //修改
                    else
                    {
                        addLog(db, hotelcode, oid, yemian, "修改情景码", "情景码:" + ratecodename + ",绿云渠道码:" + qudao + ",绿云房价码:" + pmsratecode, "情景码:" + isexist.ratecodename + ",绿云渠道码:" + isexist.qudao + ",绿云房价码:" + isexist.pmsratecode);
                        if (scenario == 1)
                        {
                            var ratecodelist = db.ratecode_t.Where(x => x.scenario == 1&&x.hotelid==hotelcode).FirstOrDefault();
                            if (ratecodelist != null)
                            {
                                ratecodelist.scenario = 0;
                            }
                            //db.SaveChanges();
                        }
                        isexist.ratecodename = ratecodename;
                        isexist.scenario = scenario;
                        isexist.qudao = qudao;
                        isexist.pmsratecode = pmsratecode;
                        db.SaveChanges();
                        jsonResult.code = "200";
                        jsonResult.msg = "修改成功";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
           
        }
        private void SetRoomDisplay(HttpContext context)
        {
            try
            {
                var data = context.Request.Form["index"];
                var result = JsonConvert.DeserializeObject<List<roomtimestart_t>>(data);
                //var log = JsonConvert.DeserializeObject<newback>(logStr);
                if (result.Count > 0)
                {
                    string roomstr = "";
                    using (var db = new yudingEntities())
                    {
                        foreach (var item in result)
                        {
                            roomstr = item.roomtype;
                            addLog(db, hotelcode, oid, yemian, operation, "房型："+item.roomtype+",起始时间："+item.startdate+":"+item.starttime +"-"+item.enddate+":"+item.endtime);
                            db.roomtimestart_t.Add(item);
                            //var room = db.roomtimestart_t.Where(x => x.roomtype == item.roomtype&&x.batch==item.batch).FirstOrDefault();
                            //if (room == null)
                            //{
                            //    roomstr = item.roomtype;
                            //    addLog(db, hotelcode, oid, yemian, operation, item.roomtype);
                            //    db.roomtimestart_t.Add(item);
                            //}
                            //else
                            //{
                            //    room.px = item.px;
                            //    room.week = item.week;
                            //    room.startdate = item.startdate;
                            //    room.enddate = item.enddate;
                            //    room.starttime = item.starttime;
                            //    room.endtime = item.endtime;
                            //    room.flag = item.flag;
                            //}
                        }
                        db.SaveChanges();
                        jsonResult.code = "200";
                        jsonResult.msg = "添加成功";
                        var istimeroom = db.roomtimestart_t.Where(x => x.roomtype == roomstr).FirstOrDefault();
                        if (istimeroom != null)
                        {
                            var room = db.newroom_t.FirstOrDefault(x => x.roomtype == roomstr);
                            room.state = "1";
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    jsonResult.code = "502";
                    jsonResult.msg = "无数据";
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        //private void SetRoomDisplay(HttpContext context)
        //{
        //    var roomdisplay = context.Request.Form["roomdisplay"];
        //    var roomtype = Getpost("roomtype");
        //    var startdate = Getpost("startdate");
        //    var enddate = Getpost("enddate");
        //    var starttime = Getpost("starttime");
        //    var endtime = Getpost("endtime");
        //    var px = Getpost("px");
        //    var week = Getpost("week");
        //    var batch = Getpost("batch");
        //    if (batch != null)
        //    {
        //        try
        //        {
        //           // var result = JsonConvert.DeserializeObject<RoomDisplay>(roomdisplay);
        //            using (var db = new yudingEntities())
        //            {
        //                using (var transaction = db.Database.BeginTransaction())
        //                {
        //                    try
        //                    {
        //                        if (batch!="")
        //                        {
        //                            var delroomdisplay = db.roomtimestart_t.Where(x => x.batch == batch && x.hotelid == hotelcode).ToList();
        //                            foreach (var item in delroomdisplay)
        //                            {
        //                                db.roomtimestart_t.Remove(item);
        //                            }
        //                            var weeklist = JsonConvert.DeserializeObject<List<yuding.JsonRequest.week>>(week);
        //                            foreach (var item in weeklist)
        //                            {
        //                                if (starttime != "" || endtime != "")
        //                                {
        //                                    var displaydata = new roomtimestart_t
        //                                    {
        //                                        hotelid = hotelcode,
        //                                        startdate =startdate,
        //                                        enddate = enddate,
        //                                        px = int.Parse(px),
        //                                        week = item.index.ToString(),
        //                                        batch = batch,
        //                                        starttime =starttime,
        //                                        endtime = endtime,
        //                                    };
        //                                    db.roomtimestart_t.Add(displaydata);
        //                                }
        //                                else
        //                                {
        //                                    var displaydata = new roomtimestart_t
        //                                    {
        //                                        hotelid = hotelcode,
                                                
        //                                        startdate = startdate,
        //                                        enddate =enddate,
        //                                        px =int.Parse(px),
        //                                        week = item.index.ToString(),
        //                                        batch = batch,

        //                                    };
        //                                    db.roomtimestart_t.Add(displaydata);
        //                                }
        //                                addLog(db, hotelcode, oid, yemian, operation, batch);
        //                            }
        //                        }
        //                        jsonResult.code = "200";
        //                        jsonResult.msg = "添加成功";
        //                        db.SaveChanges();
        //                        var istimeroom = db.roomtimestart_t.Where(x => x.roomtype == roomtype).FirstOrDefault();
        //                        if (istimeroom != null)
        //                        {
        //                            var room = db.newroom_t.FirstOrDefault(x => x.roomtype == roomtype);
        //                            room.state = "1";
        //                            db.SaveChanges();
        //                        }
        //                        transaction.Commit();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        jsonResult.code = "504";
        //                        jsonResult.msg = ex.ToString();
        //                        transaction.Rollback();
        //                        HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //            jsonResult.code = "504";
        //            jsonResult.msg = ex.ToString();
                   
        //            HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
        //        }
                
        //    }
        //}
        private void GetRoomDisplay(HttpContext context)
        {
            try
            {
                var roomcode = context.Request.Form["roomcode"];//房型代码
                var batch = context.Request.Form["batch"];//图片类型 slide轮播，cover封面，facilities设施
                if (batch != null&&roomcode!=null)
                {
                    using (var db = new yudingEntities())
                    {
                        var result = db.roomtimestart_t.Where(x => x.hotelid == hotelcode && x.batch == batch).ToList();
                        if (result.Count > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "查询成功";
                            jsonResult.data = result;
                        }
                        else
                        {
                            jsonResult.code = "502";
                            jsonResult.msg = "无数据";
                        }

                    }
                }
                else
                {
                    jsonResult.code = "500";
                    jsonResult.msg = "缺少参数";
                }

            }
            catch (Exception)
            {
                
                throw;
            }
        }
        private void AddOrUpdateRoomFacilities(HttpContext context)
        {
            try
            {
                var arr = Getpost("code").Split(',');
                var roomtype = Getpost("roomtype");
                var type = Getpost("type");
                using (var db = new yudingEntities())
                {
                    var f = db.roomtype_f_t.Where(x => x.roomtype == roomtype && x.hotelcode == hotelcode).ToList();
                    foreach (var item in f)
                    {
                        db.roomtype_f_t.Remove(item);
                    }
                    if (arr.Count() > 0)
                    {
                        foreach (var item in arr)
                        {
                            roomtype_f_t pic = new roomtype_f_t
                            {
                                addname = oid,
                                fcode = item,
                                hotelcode = hotelcode,
                                roomtype = roomtype,
                                type = type,
                            };
                            db.roomtype_f_t.Add(pic);
                        }
                    }
                    db.SaveChanges();
                    jsonResult.code = "200";
                    jsonResult.msg = "添加或修改成功";
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
            
        }
        private void GetRoomFacilities(HttpContext context)
        {
            try
            {
                var type = Getpost("roomtype");
                using (var db = new yudingEntities())
                {
                    var result =( from facilities in db.roomtype_f_t
                                  where facilities.hotelcode == hotelcode && facilities.roomtype == type
                                 select facilities).ToList();
                    if (result.Count() > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "查询成功";
                        jsonResult.data = result;
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "无数据";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            } 
        }
        private void AddOrUpdateRoomPic(HttpContext context)
        {
           
                var room = context.Request.Form["RoomPicJson"];
                if (room != null)
                {
                    var result = JsonConvert.DeserializeObject<RoomPic>(room);
                    using (var db = new yudingEntities())
                    {
                        using (var transaction = db.Database.BeginTransaction())
                        {
                            try
                            {
                                if (result != null)
                                {
                                    foreach (var item in result.piclist)
                                    {
                                       
                                            item.addname = oid;
                                            item.hotelcode = hotelcode;
                                            
                                            db.room_pic_t.AddOrUpdate(item);
                                            addLog(db, hotelcode, oid, yemian, operation, item.imgurl);
                                        
                                    }
                                }
                                    jsonResult.code = "200";
                                    jsonResult.msg = "添加或修改成功";
                                    db.SaveChanges();
                                    transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                jsonResult.code = "504";
                                jsonResult.msg = ex.ToString();
                                transaction.Rollback();
                                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
                            }
                        }
                    }
                }
        }
        private void GetRoomPic(HttpContext context)
        {
            var roomcode = context.Request.Form["roomtype"];//房型代码
            //var type = context.Request.Form["pictype"];//图片类型 slide轮播，cover封面，facilities设施
            if (roomcode != null)
            {
                using (var db = new yudingEntities())
                {
                    var result = db.room_pic_t.Where(x => x.roomtypecode == roomcode).ToList();
                    if (result.Count > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "查询成功";
                        jsonResult.data = result;
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "无数据";
                    }
                   
                }
            }
            else
            {
                jsonResult.code = "500";
                jsonResult.msg = "缺少参数";
            }
        }
        public static void ForeachClassProperties<T>(T model)
        {
            Type t = model.GetType();
            PropertyInfo[] PropertyList = t.GetProperties();
            foreach (PropertyInfo item in PropertyList)
            {
                string name = item.Name;
                object value = item.GetValue(model, null);
            }
        }
        private void AddOrUpdateRoom(HttpContext context)
        {
            try
            {
                var id = int.Parse(context.Request.Form["id"]);
                var pms = context.Request.Form["pms"];
                var descript = context.Request.Form["descript"];
                var yuanjia = context.Request.Form["yuanjia"];
                var px = int.Parse(context.Request.Form["pxid"]);
                var num = int.Parse(context.Request.Form["num"]);
                var roomtype = context.Request.Form["roomtype"];
                var qxxz = context.Request.Form["qxxz"];
                var ydxz = context.Request.Form["ydxz"];
                var sysType = Getpost("sysType");
                    using (var db = new yudingEntities())
                    {
                        var data = db.newroom_t.FirstOrDefault(x => x.id ==id);
                        if (data != null)
                        {
                            data.pms = pms;
                            data.descript = descript;
                            data.yuanjia = yuanjia;
                            data.pxid = px;
                            data.num = num;
                                
                            //var logJson = context.Request.Form["logJson"];
                            //var log = JsonConvert.DeserializeObject<newback>(logJson);
                            //db.SaveChanges();
                           // db.newbacks.Add(log);
                        }
                        else
                        {
                            var result = new newroom_t()
                            {
                                pms = pms,
                                descript = descript,
                                hotelid = hotelcode,
                                roomtype = roomtype,
                                yuanjia = yuanjia,
                                pxid = px,
                                num = num,
                                type=int.Parse( sysType)
                                
                            };
                            db.newroom_t.AddOrUpdate(result);//新增
                           // var logJson = context.Request.Form["logJson"];
                            //var log = JsonConvert.DeserializeObject<newback>(logJson);
                            //log.hotelid = hotelcode;
                           // db.newbacks.Add(log);
                        }
                        if (db.SaveChanges() > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "添加或修改成功";
                            //db.SaveChanges();
                        }
                        else
                        {
                            jsonResult.code = "503";
                            jsonResult.msg = "无修改或添加";
                        }
                    }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void GetRooms(HttpContext context)
        {
            var type =int.Parse( Getpost("type"));
            try
            {
                using (var db = new yudingEntities())
                {
                    var result = from rooms in db.newroom_t
                                 where rooms.hotelid ==hotelcode && rooms.type==type   
                                 select rooms;
                    if (result.Count() > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "查询成功";
                        jsonResult.data = result.OrderByDescending(x=>x.pxid).ToList();
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "无数据";
                    }
                    //HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            } 
        }
        private void UpdateRoomsCoverPic(HttpContext context)
        {
            try
            {
                var pic = context.Request.Form["pic"];
                var id =int.Parse(Getpost("id"));
                if (pic != null)
                {
                    using (var db = new yudingEntities())
                    {
                        var data = db.newroom_t.FirstOrDefault(x => x.id == id);
                        data.img = pic;
                        addLog(db, hotelcode, oid, yemian, operation,pic);
                            //var logJson = context.Request.Form["logJson"];
                            //var log = JsonConvert.DeserializeObject<newback>(logJson);
                           // log.hotelid = hotelcode;
                           // log.newdata = pic;
                          //  log.olddata = data.img;
                          //  db.newbacks.Add(log);
                        if (db.SaveChanges() > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "添加或修改成功";
                        }
                        else
                        {
                            jsonResult.code = "503";
                            jsonResult.msg = "无修改或添加";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
 
        }
        private void AddOrUpdateHotelPresentation(HttpContext context)
        {
            try
            {
                var presentation = context.Request.Form["HotelDataJson"];
                if (presentation != null)
                {
                    var result = JsonConvert.DeserializeObject<hotel_list>(presentation);
                    using (var db = new yudingEntities())
                    {
                        var data = db.hotel_list.AsNoTracking().FirstOrDefault(x => x.hotelId == result.hotelId);
                        if (data != null)
                        {
                            db.hotel_list.Attach(result);
                            var stateEntry = ((IObjectContextAdapter)db).ObjectContext.
                              ObjectStateManager.GetObjectStateEntry(result);
                            stateEntry.SetModifiedProperty("content");
                            stateEntry.SetModifiedProperty("tel");
                            stateEntry.SetModifiedProperty("address");
                            stateEntry.SetModifiedProperty("map");
                            stateEntry.SetModifiedProperty("roomService");
                            stateEntry.SetModifiedProperty("url");
                            stateEntry.SetModifiedProperty("hotelName");
                            stateEntry.SetModifiedProperty("MeetingTel");
                            stateEntry.SetModifiedProperty("RestaurantTel");
                            stateEntry.SetModifiedProperty("RoomTel");
                            stateEntry.SetModifiedProperty("City");
                            stateEntry.SetModifiedProperty("HotelGroupCode");
                            stateEntry.SetModifiedProperty("HotelGroupName");
                            stateEntry.SetModifiedProperty("pms");
                            stateEntry.SetModifiedProperty("onsalecode");
                            addLog(db, hotelcode, oid, operation, yemian, presentation, JsonConvert.SerializeObject(data));
                            db.SaveChanges();
                        }
                        else
                        {
                            db.hotel_list.Add(result);
                            addLog(db, hotelcode, oid, operation, yemian, result.hotelName);
                            db.SaveChanges();
                        }
                        jsonResult.code = "200";
                        jsonResult.msg = "添加或修改成功";
                        
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void GetHotelPresentation(HttpContext context)
        {
            try
            {
                using (var db = new yudingEntities())
                {
                    var result = db.hotel_list.Where(x => x.hotelId == hotelcode).FirstOrDefault();
                    if (result!=null)
                    {
                        var f = new List<object>();
                        if (result.roomService != null)
                        {
                            var list = result.roomService.Split(',');
                            
                            if (list.Count() > 0)
                            {
                                foreach (var item in list)
                                {

                                    var code = db.facilities_t.Where(x => x.codename == item).FirstOrDefault();
                                    if (code != null)
                                    {
                                        f.Add(code);
                                    }
                                }
                            }
                        }
                            var fahui = new
                            {
                                facilities = f,
                                info = result,
                            };
                            jsonResult.code = "200";
                            jsonResult.msg = "查询成功";
                            jsonResult.data = fahui;
                    }
                    else
                    {
                        var hotelname = Getpost("hotelname");
                        var hotelGroupCode = Getpost("hotelGroupCode");
                        var hotelGroupName = Getpost("hotelGroupName");
                        if (hotelname != "")
                        {
                            var hotel = new hotel_list
                            {
                                HotelGroupCode=hotelGroupCode,
                                HotelGroupName=hotelGroupName,
                                hotelId = hotelcode,
                                hotelName = hotelname,
                            };
                            db.hotel_list.Add(hotel);
                            db.SaveChanges();
                            var fahui = new
                            {
                                facilities = "",
                                info = hotel,
                            };
                            jsonResult.code = "200";
                            jsonResult.msg = "查询成功";
                            jsonResult.data =fahui;
                        }
                        else
                        {
                            jsonResult.code = "500";
                            jsonResult.msg = "没有实体数据";
                        }         
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            } 
        }
        private void AddOrUpdateHotelPic(HttpContext context)
        {
             try
            {
                var IncreaseData = context.Request.Form["HotelPicJson"];
                using (var db = new yudingEntities())
                {
                    if (IncreaseData != null)
                    {
                        var increase = JsonConvert.DeserializeObject<hotelpic_t>(IncreaseData);
                        var data = db.hotelpic_t.AsNoTracking().FirstOrDefault(x => x.id == increase.id);
                        if (data != null)
                        {
                            db.hotelpic_t.Attach(increase);
                            db.hotelpic_t.AddOrUpdate(increase);
                            var stateEntry = ((IObjectContextAdapter)db).ObjectContext.
                             ObjectStateManager.GetObjectStateEntry(increase);
                            stateEntry.SetModifiedProperty("picname");
                            stateEntry.SetModifiedProperty("picurl");
                            
                            stateEntry.SetModifiedProperty("px");
                            addLog(db, hotelcode, oid, yemian, operation, increase.picurl, data.picurl);
                        }
                        else
                        {
                            db.hotelpic_t.AddOrUpdate(increase);
                            addLog(db, hotelcode, oid, yemian, operation, increase.picurl);
                        }
                        if (db.SaveChanges() > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "添加或修改成功";
                        }
                        else
                        {
                            jsonResult.code = "500";
                            jsonResult.msg = "添加失败";
                        }
                    }
                    else
                    {
                        jsonResult.code = "500";
                        jsonResult.msg = "缺少参数";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
           
        }
        private void GetHotelPic(HttpContext context)
        {
            try
            {
                using (var db = new yudingEntities())
                {
                    var result = db.hotelpic_t.Where(x => x.hotelcode == hotelcode).OrderByDescending(x=>x.px).ToList();
                    if (result.Count > 0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "查询成功";
                        jsonResult.data = result;
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void AddOrUpdateSystemPic(HttpContext context)
        {
            try
            {
                var pic = context.Request.Form["picJson"];
                if (pic != null)
                {
                    var picEntity = JsonConvert.DeserializeObject<xitongpic_t>(pic);
                    using (var db = new yudingEntities())
                    {
                        db.xitongpic_t.AddOrUpdate(picEntity);
                        if (db.SaveChanges() > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "添加或修改成功";
                            var logJson = context.Request.Form["logJson"];
                            var log = JsonConvert.DeserializeObject<newback>(logJson);
                            db.newbacks.Add(log);
                            db.SaveChanges();
                        }
                        else
                        {
                            jsonResult.code = "503";
                            jsonResult.msg = "无修改或添加";
 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }
        private void GetSystemPic(HttpContext context)
        {
            try
            {
                using (var db = new yudingEntities())
                {
                    var result = db.xitongpic_t.Where(x => x.hotelid == hotelcode).ToList();
                    if (result.Count>0)
                    {
                        jsonResult.code = "200";
                        jsonResult.msg = "查询成功";
                        jsonResult.data = result;
                    }
                    else
                    {
                        jsonResult.code = "502";
                        jsonResult.msg = "查询成功";
                    }
                  
                }
                
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }    
        private void addLog(yudingEntities db, string hotelid, string oid, string yemian, string operation, string newdata, string olddata = "")
        {
            var log = new newback()
            {
                hotelid = hotelid,
                oid = oid,
                type = logtype,
                yemian = yemian,
                operation = operation,
                olddata = olddata,
                newdata = newdata,
                addtime=DateTime.Now.ToString(),
            };
            db.newbacks.Add(log);
        }
        /// <summary>
        /// 获取页面地址的参数值，相当于 Request.QueryString
        /// </summary>
        public static string Get(string name)
        {

            string value = HttpContext.Current.Request.QueryString[name];
            return value == null ? string.Empty : value.Trim();
        }
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        } 
        /// <summary>
        /// 获取post的值
        /// </summary>
        public static string Getpost(string name)
        {
            string value = HttpContext.Current.Request.Form[name];
            //string value = HttpContext.Current.Request.QueryString[name];
            return value == null ? string.Empty : value.Trim();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion
        private int GetTimeStamp(DateTime dt)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            int timeStamp = Convert.ToInt32((dt - dateStart).TotalSeconds);
            return timeStamp;
        }
        //自定义表分页
        private DataTable getOnePageTable(DataTable dtAll, int pageNo, int pageSize)
        {
            var totalCount = dtAll.Rows.Count;
            var totalPage = getTotalPage(totalCount, pageSize);
            var currentPage = pageNo;
            currentPage = (currentPage > totalPage ? totalPage : currentPage);//如果PageNo过大，则较正PageNo=PageCount
            currentPage = (currentPage <= 0 ? 1 : currentPage);//如果PageNo<=0，则改为首页
            //----克隆表结构到新表
            var onePageTable = dtAll.Clone();
            //----取出1页数据到新表
            var rowBegin = (currentPage - 1) * pageSize;
            var rowEnd = currentPage * pageSize;
            rowEnd = (rowEnd > totalCount ? totalCount : rowEnd);
            for (var i = rowBegin; i <= rowEnd - 1; i++)
            {
                var newRow = onePageTable.NewRow();
                var oldRow = dtAll.Rows[i];
                foreach (DataColumn column in dtAll.Columns)
                {
                    newRow[column.ColumnName] = oldRow[column.ColumnName];
                }
                onePageTable.Rows.Add(newRow);
            }
            return onePageTable;
        }
        /// <summary>
        /// 返回分页后的总页数
        /// </summary>
        /// <param name="totalCount">总记录条数</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns>总页数</returns>
        public int getTotalPage(int totalCount, int pageSize)
        {
            var totalPage = (totalCount / pageSize) + (totalCount % pageSize > 0 ? 1 : 0);
            return totalPage;
        }
    }
}