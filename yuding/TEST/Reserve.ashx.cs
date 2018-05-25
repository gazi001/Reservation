using Newtonsoft.Json;

using RM.Common.DotNetHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using yuding.JsonResult;
using yuding.Model;

namespace ReserveAPI.API
{
    /// <summary>
    /// Reserve 的摘要说明
    /// </summary>
    public class Reserve : IHttpHandler
    {
        public string oid;
        JsonReturn jsonResult = new JsonReturn();
        public void ProcessRequest(HttpContext context)
        {
            var action = context.Request.QueryString["action"];
            oid = context.Request.Form["oid"];
            //var action = "GetOplog";
            //oid = "111";
            if (oid != null)
            {
                switch (action)
                {
                    #region 
                    case "setHotelpic":
                        setHotelpic(context);//添加海报的轮播图（*）
                        break;
                    case "getHotel_List":
                        getHotel_List(context);//获取酒店基本信息
                        break;
                    case "getHotelpic":
                        getHotelpic(context);//获取酒店轮播图
                        break;
                    case "delHotelpic":
                        delHotelpic(context);//酒店海报删除接口（*）
                        break;
                    case "updateHotelpic":
                        updateHotelpic(context);//酒店海报修改接口（*）
                        break;
                    case "Stopnewroom_t":
                        Stopnewroom_t(context);//房型锁房接口
                        break;
                    case "Getfacilities":
                        Getfacilities(context);//设施获取接口（有问题）
                        break;
                    #endregion
                    case "Getroom_pic":
                        Getroom_pic(context);//获取房型轮播图
                        break;
                    case "GetOplog":
                        GetOplog(context);//获取操作日志
                        break;
                    case "GetFacilities":
                        GetFacilities(context);//获取酒店设施
                        break;
                    case "AddFacilities":
                        AddFacilities(context);//添加酒店设施
                        break;
                    case "UpdateSystemPic":
                        UpdateSystemPic(context);//修改系统图片
                        break;
                    case "GetSystemPic":
                        GetSystemPic(context);//获取系统图片
                        break;
                    case "AddSystemPic":
                        AddSysyemPic(context);//新增系统图片
                        break;
                    case "GetXzcodeRule":
                        GetXzcodeRule(context);//查看单条明细的细则
                        break;
                    case "UpdateVals":
                        UpdateVals(context);//停用启用关联的增值服务
                        break;
                    case "UpdateTQYRules":
                        UpdateTQYRules(context);//停用启用多间规则
                        break;
                    case "UpdatePLRoomRules":
                        UpdatePLRoomRules(context);//修改多间规则
                        break;
                    case "GetPLRoomRules":
                        GetPLRoomRules(context);//获取多间规则
                        break;
                    case "DelRoomtypeRules":
                        DelRoomtypeRules(context);//删除房型显示细则明细
                        break;
                    case "GetRoomtypeRules":
                        DelRoomtypeRules(context);//查看房价明细码显示规则明细
                        break;
                    case "GetRoomtypeRulesTime":
                        GetRoomtypeRulesTime(context);//查看房价明细码显示时间段
                        break;
                    case "DelRoomtypeRulesGZ":
                        DelRoomtypeRulesGZ(context);//删除房型显示规则
                        break;
                    case "GetRoomtypeRulesGZ":
                        GetRoomtypeRulesGZ(context);//查看房型显示规则
                        break;
                    case "GetRoomXsTime":
                        GetRoomXsTime(context);//查看房型显示时间段
                        break;
                    case "UpdateWXOrder":
                        UpdateWXOrder(context);//订单支付成功写入微信支付单号
                        break;
                    case "increase_order_list":
                        increase_order_list(context);//查看增值服务订单列
                        break;
                    case "increase_order_edit":
                        increase_order_edit(context);//查看增值服务订单列
                        break;
                    case "baojia_list":
                        baojia_list(context);//查看煲价订单列
                        break;
                    //case "increase_link_list":
                    //    increase_link_list(context);//查看煲价订单列
                    //    break;
                    case "increase_update":
                        increase_update(context);//增值服务修改
                        break;
                    case "increase_list":
                        increase_list(context);//查看增值服务列表
                        break;
                    case "increase_edit":
                        increase_edit(context);//添加增值服务
                        break;
                     ///////   //////
                    case "openid_del":
                        openid_del(context);//删除OPENID
                        break;
                    case "openid_list":
                        openid_list(context);//获取OPENID
                        break;
                    case "openid_edit":
                        openid_edit(context);//添加OPENID
                        break;
                    case "rateroom_xz_dan_list":
                        rateroom_xz_dan_list(context);//查找单条房价码明细
                        break;
                    case "weixin_time":
                        weixin_time(context);//获取当前时间戳
                        break;
                    case "weixin_orderispay_update"://修改订单是否支付
                        weixin_orderispay_update(context);
                        break;
                    case "weixin_ordersessionid_list"://查询同一批次的订单
                        weixin_ordersessionid_list(context);
                        break;
                    case "weixin_ordernum_list"://查询同一批次的订单
                        weixin_ordernum_list(context);
                        break;
                        
                }
            }
            else
            {
                jsonResult.code = "503";
                jsonResult.msg = "缺少参数";
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
            
        }
        #region
        private void Getroom_pic(HttpContext context)
        {
            try
            {
                using (var db = new yudingEntities())
                {
                    
                    jsonResult.code = "200";
                    jsonResult.msg = "添加成功";
                    
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "504";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
            jsonResult.code = "500";
            jsonResult.msg = "缺少参数";
        }

        private void Getfacilities(HttpContext context)
        {
            try
            {
                using (var db = new yudingEntities())
                {
                    var result = db.facilities_t.Where(x => x.flag == 0).ToList();
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

        private void Stopnewroom_t(HttpContext context)
        {
            try
            {
                var flag = context.Request.Form["flag"];
                var hotelcode = context.Request.Form["hotelid"];
                var roomtype = context.Request.Form["roomtype"];
                if (flag != null && hotelcode != null&&roomtype!=null)
                {
                    using (var db = new yudingEntities())
                    {
                        var result = db.newroom_t.Where(x => x.hotelid == hotelcode && x.roomtype == roomtype).ToList();
                        var flagParse = int.Parse(flag);
                        foreach (var item in result)
                        {
                            item.flag = flagParse;
                        }
                        if (db.SaveChanges() > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "更新成功";
                        }
                        else
                        {
                            jsonResult.code = "501";
                            jsonResult.msg = "更新失败";
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

        private void updateHotelpic(HttpContext context)
        {
            try
            {
                var id = context.Request.Form["id"];
                var picname = context.Request.Form["picname"];
                var px = context.Request.Form["px"];
                if (id != null&&picname!=null&px!=null)
                {
                    using (var db = new yudingEntities())
                    {
                        var idParse = int.Parse(id);
                        var pic = db.hotelpic_t.Where(x => x.id == idParse).FirstOrDefault();
                        if (pic != null)
                        {
                            pic.px = int.Parse(px);
                            pic.picname = picname;
                            if (db.SaveChanges() > 0)
                            {
                                jsonResult.code = "200";
                                jsonResult.msg = "更新成功";
                            }
                            else
                            {
                                jsonResult.code = "501";
                                jsonResult.msg = "更新失败";
                            }
                        }
                        else
                        {
                            jsonResult.code = "502";
                            jsonResult.msg = "没有该实体数据";
 
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

        private void delHotelpic(HttpContext context)
        {
         
            try
            {
                using (var db = new yudingEntities())
                {
                    var id = context.Request.Form["id"];
                    if (id != null)
                    {
                        var idParse = int.Parse(id);
                        var pic = db.hotelpic_t.Where(x => x.id == idParse).FirstOrDefault();
                        db.hotelpic_t.Remove(pic);
                        if (db.SaveChanges() > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "删除成功";
                            //TODO:日志添加
                          
                        }
                        else
                        {
                            jsonResult.code = "500";
                            jsonResult.msg = "删除失败";
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

        private void getHotelpic(HttpContext context)
        {
            try
            {
                using (var db = new yudingEntities())
                {
                    var hotelcode = context.Request.Form["hotelcode"];
                    if (hotelcode != null)
                    {
                        var result = db.hotelpic_t.Where(x => x.hotelcode == hotelcode).OrderBy(x => x.px).ToList();
                        jsonResult.code = "200";
                        jsonResult.msg = "查询成功";
                        jsonResult.data = result;
                    }
                    else
                    {
                        jsonResult.code = "500";
                        jsonResult.msg = "参数缺少";
                    }
                }
            }
            catch (Exception ex)
            {
                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
                
            }
        }

        private void getHotel_List(HttpContext context)
        {
             try
             {
                 using (var db = new yudingEntities())
                 {
                     var hotelcode = context.Request.Form["hotelcode"];
                     if(hotelcode!=null)
                     {
                         var result = db.hotel_list.Where(x => x.hotelId == hotelcode).FirstOrDefault();
                         jsonResult.code = "200";
                         jsonResult.msg = "查询成功";
                         jsonResult.data = result;
                     }
                     else
                     {
                         jsonResult.code = "500";
                         jsonResult.msg = "参数缺少";
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
#endregion
        private void setHotelpic(HttpContext context)
        {
            try
            {
                using (var db = new yudingEntities())
                {
                    var postStr = context.Request.Form["param"];
                    if (postStr != null)
                    {
                        var pic = JsonConvert.DeserializeObject<hotelpic_t>(postStr);
                        db.hotelpic_t.Add(pic);
                        if (db.SaveChanges() > 0)
                        {
                            jsonResult.code = "200";
                            jsonResult.msg = "添加成功";

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
            catch (Exception ex )
            {
                jsonResult.code = "500";
                jsonResult.msg = ex.ToString();
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, context);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 获取页面地址的参数值，相当于 Request.QueryString
        /// </summary>
        public static string Get(string name)
        {

            string value = HttpContext.Current.Request.QueryString[name];
            return value == null ? string.Empty : value.Trim();
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
        /// <summary>
        /// 操作日志插入
        /// </summary>
        private void addLog(yudingEntities db, string hotelid, string oid, string yemian, string operation, string newdata, string olddata = "", string type = "客房")
        {
            var log = new newback()
            {
                hotelid = hotelid,
                oid = oid,
                type = type,
                yemian = yemian,
                operation = operation,
                olddata = olddata,
                newdata = newdata
            };
            db.newbacks.Add(log);
        }



        public void GetOplog(HttpContext context)
        {
            string json = "";
            string shopcode = Getpost("shopcode");
            int size = Convert.ToInt32(Getpost("size"));
            int page = Convert.ToInt32(Getpost("page"));
            //string shopcode = "KSHZ";
            //int size = 10;
            //int page = 1;
            string count = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var totalcount = db.newbacks.Where(x => x.hotelid==shopcode).ToList().Count.ToString();
                    var data = (from a in db.newbacks where a.hotelid==shopcode select a ).OrderBy(x=>x.addtime).Skip((page - 1) * size).Take(size).ToList();
                    //var data = db.newbacks.Where(x => x.hotelid == shopcode).Skip((page - 1) * size).Take(size).ToList();
                    json = JsonConvert.SerializeObject(data);
                    count=totalcount;
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":" + json + ",\"count\":\"" + count + "\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"获取失败\",\"count\":\"" + count + "\"}");
            }
        }

        public void GetFacilities(HttpContext context)
        {
            string shopcode = Getpost("hotelcode");
            string term = Getpost("term");
            string json = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    //var config = db.hotelfacilities_t.Where(x => x.hotelid == shopcode).ToList();
                    var config = db.Database.SqlQuery<hotelfacilities_t>("select * from hotelfacilities_t where hotelid='" + shopcode + "' " + term + "");
                    json = JsonConvert.SerializeObject(config);
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":" + json + "}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"获取失败\"}");
            }
        }

        public void AddFacilities(HttpContext context)
        {
            string oid = Getpost("oid");
            string shopcode=Getpost("hotelid");
            string logtype = Getpost("logtype");
            string yemian = Getpost("yemian");
            string code=Getpost("code");
            string codename=Getpost("codename");
            string img=Getpost("img");
            int Type=Convert.ToInt32(Getpost("type"));
            
            try
            {
                using (var db = new yudingEntities())
                {
                    hotelfacilities_t mp = new hotelfacilities_t()
                    {
                        hotelid = shopcode,
                        code = code,
                        codename = codename,
                        img = img,
                        type = Type,
                    };
                    db.hotelfacilities_t.Add(mp);
                    addLog(db, shopcode, oid, yemian, "添加", img,"",logtype);
                    db.SaveChanges();
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":\"增加成功\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"增加失败\"}");
            }
        }

        public void UpdateSystemPic(HttpContext context)
        {
            string oid = Getpost("oid");
            string shopcode = Getpost("hotelid");
            string logtype = Getpost("logtype");
            string yemian = Getpost("yemian");
            int id = Convert.ToInt32(Getpost("id"));
            string img=Getpost("img");
            string pp = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.xitongpic_t.Where(x => x.id==id && x.hotelid == shopcode).ToList();
                    if (config.Count>0)
                    {
                        foreach (var item in config)
                        {
                            pp = item.image;
                            item.image = img;
                            addLog(db, shopcode, oid, yemian, "修改", img, pp, logtype);
                        }
                    }
                    //db.Entry(config).State = System.Data.EntityState.Modified;
                    
                    db.SaveChanges();
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":\"修改成功\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"修改失败\"}");
            }
        }

        public void GetSystemPic(HttpContext context)
        {
            string shopcode = Getpost("hotelid");
            string json = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.xitongpic_t.Where(x => x.hotelid == shopcode).ToList();
                    json = JsonConvert.SerializeObject(config);
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":" + json + "}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"获取失败\"}");
            }
        }

        public void AddSysyemPic(HttpContext context)
        {
            string oid = Getpost("oid");
            string shopcode = Getpost("hotelid");
            string logtype = Getpost("logtype");
            string yemian = Getpost("yemian");
            int xt = Convert.ToInt32(Getpost("xt"));
            string img = Getpost("img");
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.xitongpic_t.Where(x => x.hotelid == shopcode&&x.xt==xt).ToList();
                    if (config.Count > 0)
                    {
                        context.Response.Write("{\"code\":\"500\",\"msg\":\"已有系统图片\"}");
                    }
                    else
                    {
                        xitongpic_t mp = new xitongpic_t()
                        {
                            hotelid = shopcode,
                            image=img,
                            xt=xt,
                        };
                        db.xitongpic_t.Add(mp);
                        addLog(db, shopcode, oid, yemian, "添加", img, "", logtype);
                    }
                    db.SaveChanges();
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":\"增加成功\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"增加失败\"}");
            }
        }

        public void GetXzcodeRule(HttpContext context)
        {
            string xzcode = Getpost("xzcode");
            string json = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.associateds.Where(x => x.xz_code == xzcode).ToList();
                    if (config.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(config);
                        context.Response.Write("{\"code\":\"200\",\"msg\":" + json + "}");
                    }
                    else
                    {
                        context.Response.Write("{\"code\":\"500\",\"msg\":\"未找到数据\"}");
                    }
                }
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"异常\"}");
            }
        }

        public void UpdateVals(HttpContext context)
        {
            string oid = Getpost("oid");
            string shopcode = Getpost("hotelid");
            string logtype = Getpost("logtype");
            string yemian = Getpost("yemian");

            string increase = Getpost("increase");
            string roomtype = Getpost("roomtype");
            string flag=Getpost("flag");
            string pp = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.increase_link.Where(x => x.increase == increase && x.hotelid == shopcode && x.roomtype == roomtype).ToList();
                    if (config.Count>0)
                    {
                        foreach (var item in config)
                        {
                            pp = item.flag.ToString();
                            item.flag = Convert.ToInt32(flag);
                            addLog(db, shopcode, oid, yemian, "修改", flag, pp, logtype);
                        }
                    }
                    //db.Entry(config).State = System.Data.EntityState.Modified;
                    
                    db.SaveChanges();
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":\"修改成功\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"修改失败\"}");
            }
        }

        public void UpdateTQYRules(HttpContext context)
        {
            string oid = Getpost("oid");
            string shopcode = Getpost("hotelid");
            string logtype = Getpost("logtype");
            string yemian = Getpost("yemian");
            string djcode = Getpost("djcode");
            string flag = Getpost("flag");
            string pp = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.duojian_t.Where(x => x.djcode == djcode && x.hotelid == shopcode).ToList();
                    if (config.Count > 0)
                    {
                        foreach (var item in config)
                        {
                            pp = item.flag.ToString();
                            item.flag = Convert.ToInt32(flag);
                            addLog(db, shopcode, oid, yemian, "修改", flag, pp, logtype);
                        }
                    }
                    //db.Entry(config).State = System.Data.EntityState.Modified;

                    db.SaveChanges();
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":\"修改成功\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"修改失败\"}");
            }
        }

        public void UpdatePLRoomRules(HttpContext context)
        {
            string oid = Getpost("oid");
            string shopcode = Getpost("hotelid");
            string logtype = Getpost("logtype");
            string yemian = Getpost("yemian");

            string describe = Getpost("describe");
            string num = Getpost("num");
            string money = Getpost("money");
            string djcode = Getpost("djcode");
            string pp = "";
            string qq = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.duojian_t.Where(x => x.djcode == djcode && x.hotelid == shopcode).ToList();
                    if (config.Count > 0)
                    {
                        foreach (var item in config)
                        {
                            pp = item.describe.ToString();
                            qq = item.num.ToString();
                            item.describe = describe;
                            item.num =Convert.ToInt32(num);
                            addLog(db, shopcode, oid, yemian, "修改", describe+""+num, pp+""+qq, logtype);
                        }
                    }
                    //db.Entry(config).State = System.Data.EntityState.Modified;

                    db.SaveChanges();
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":\"修改成功\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"修改失败\"}");
            }
        }

        public void AddPLRoomRules(HttpContext context)
        {
            string oid = Getpost("oid");
            string shopcode = Getpost("hotelid");
            string logtype = Getpost("logtype");
            string yemian = Getpost("yemian");

            string describe = Getpost("describe");
            string num = Getpost("num");
            string money = Getpost("money");
            string djcode = Getpost("djcode");
            string type = Getpost("type");
            try
            {
                using (var db = new yudingEntities())
                {
                        duojian_t mp = new duojian_t()
                        {
                            hotelid = shopcode,
                            describe=describe,
                            num=Convert.ToInt32(num),
                            money=Convert.ToInt32(money),
                            djcode=djcode,
                            type=Convert.ToInt32(type),
                        };
                        db.duojian_t.Add(mp);
                        addLog(db, shopcode, oid, yemian, "添加", describe+","+num+","+money, "", logtype);
                    
                    db.SaveChanges();
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":\"增加成功\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"增加失败\"}");
            }
        }

        public void GetPLRoomRules(HttpContext context)
        {
            string shopcode = Getpost("shopcode");
            int type =Convert.ToInt32(Getpost("type"));
            string json = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.duojian_t.Where(x => x.hotelid== shopcode&&x.type==type).ToList();
                    if (config.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(config);
                        context.Response.Write("{\"code\":\"200\",\"msg\":" + json + "}");
                    }
                    else
                    {
                        context.Response.Write("{\"code\":\"500\",\"msg\":\"未找到数据\"}");
                    }
                }
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"异常\"}");
            }
        }

        public void DelRoomtypeRules(HttpContext context)
        {
            string oid = Getpost("oid");
            string shopcode = Getpost("hotelid");
            string logtype = Getpost("logtype");
            string yemian = Getpost("yemian");

            string xzcode = Getpost("xzcode");
            string batch = Getpost("batch");
            string json = "";
                
            try
            {
                using (var db = new yudingEntities())
                {
                    //var config = db.hotelfacilities_t.Where(x => x.hotelid == shopcode).ToList();
                    var config = db.Database.SqlQuery<hotelfacilities_t>("Delete from xztimestart_t where xzcode='" + xzcode + "' and hotelid='"+shopcode+"' and batch='"+batch+"' ");
                    addLog(db, shopcode, oid, yemian, "删除", "", "", logtype);
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":\"删除成功\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"删除失败\"}");
            }
        }

        public void GetRoomtypeRules(HttpContext context)
        {
            string shopcode = Getpost("shopcode");
            string batch = Getpost("batch");
            string json = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.xztimestart_t.Where(x => x.hotelid == shopcode && x.batch==batch).ToList();
                    if (config.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(config);
                        context.Response.Write("{\"code\":\"200\",\"msg\":" + json + "}");
                    }
                    else
                    {
                        context.Response.Write("{\"code\":\"500\",\"msg\":\"未找到数据\"}");
                    }
                }
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"异常\"}");
            }
        }

        public void GetRoomtypeRulesTime(HttpContext context)
        {
            string shopcode = Getpost("shopcode");
            string batch = Getpost("batch");
            string json = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.xztimestart_t.Where(x => x.hotelid == shopcode && x.batch == batch).GroupBy(x => x.batch).ToList();
                    if (config.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(config);
                        context.Response.Write("{\"code\":\"200\",\"msg\":" + json + "}");
                    }
                    else
                    {
                        context.Response.Write("{\"code\":\"500\",\"msg\":\"未找到数据\"}");
                    }
                }
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"异常\"}");
            }
        }

        public void DelRoomtypeRulesGZ(HttpContext context)
        {
            string oid = Getpost("oid");
            string shopcode = Getpost("hotelid");
            string logtype = Getpost("logtype");
            string yemian = Getpost("yemian");

            string roomtype = Getpost("roomtype");
            string batch = Getpost("batch");
            string json = "";

            try
            {
                using (var db = new yudingEntities())
                {
                    //var config = db.hotelfacilities_t.Where(x => x.hotelid == shopcode).ToList();
                    var config = db.Database.SqlQuery<hotelfacilities_t>("Delete from roomtimestart_t where roomtype='" + roomtype + "' and hotelid='" + shopcode + "' and batch='" + batch + "' ");
                    addLog(db, shopcode, oid, yemian, "删除", "", roomtype+"的"+batch, logtype);
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":\"删除成功\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"删除失败\"}");
            }
        }

        public void GetRoomtypeRulesGZ(HttpContext context)
        {
            string shopcode = Getpost("shopcode");
            string batch = Getpost("batch");
            string json = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.roomtimestart_t.Where(x => x.hotelid == shopcode && x.batch == batch).ToList();
                    if (config.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(config);
                        context.Response.Write("{\"code\":\"200\",\"msg\":" + json + "}");
                    }
                    else
                    {
                        context.Response.Write("{\"code\":\"500\",\"msg\":\"未找到数据\"}");
                    }
                }
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"异常\"}");
            }
        }

        public void GetRoomXsTime(HttpContext context)
        {
            string roomtype = Getpost("roomtype");
            string shopcode = Getpost("shopcode");
            string json = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.roomtimestart_t.Where(x => x.hotelid == shopcode && x.roomtype == roomtype).ToList();
                    if (config.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(config);
                        context.Response.Write("{\"code\":\"200\",\"msg\":" + json + "}");
                    }
                    else
                    {
                        context.Response.Write("{\"code\":\"500\",\"msg\":\"未找到数据\"}");
                    }
                }
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"异常\"}");
            }
        }

        public void UpdateWXOrder(HttpContext context)
        {
            string oid = Getpost("oid");
            string shopcode = Getpost("hotelid");
            string logtype = Getpost("logtype");
            string yemian = Getpost("yemian");

            string ordernumber = Getpost("ordernumber");
            string pp = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.order_t.Where(x => x.ordernumber == ordernumber && x.hotelcode == shopcode).ToList();
                    if (config.Count > 0)
                    {
                        foreach (var item in config)
                        {
                            item.ordernumber = ordernumber;
                            addLog(db, shopcode, oid, yemian, "修改", ordernumber, "", logtype);
                        }
                    }
                    //db.Entry(config).State = System.Data.EntityState.Modified;

                    db.SaveChanges();
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":\"修改成功\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"修改失败\"}");
            }
        }

        public void increase_order_list(HttpContext context)
        {
            string sessionid = Getpost("sessionid");
            string shopcode = Getpost("hotelid");
            string json = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.increase_order.Where(x => x.hotelid == shopcode && x.sessionid == sessionid).ToList();
                    if (config.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(config);
                        context.Response.Write("{\"code\":\"200\",\"msg\":" + json + "}");
                    }
                    else
                    {
                        context.Response.Write("{\"code\":\"500\",\"msg\":\"未找到数据\"}");
                    }
                }
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"异常\"}");
            }
        }

        public void increase_order_edit(HttpContext context)
        {
            string oid = Getpost("oid");
            string shopcode = Getpost("hotelid");
            string logtype = Getpost("logtype");
            string yemian = Getpost("yemian");

            string increaseprice = Getpost("increaseprice");
            string sessionid = Getpost("sessionid");
            string roomtype = Getpost("roomtype");
            string descript = Getpost("descript");
            string increasenum = Getpost("increasenum");
            string increasediscribe = Getpost("increasediscribe");
            string price = Getpost("price");
            string pay = Getpost("pay");
            try
            {
                using (var db = new yudingEntities())
                {
                    increase_order mp = new increase_order()
                    {
                        hotelid = shopcode,
                        increaseprice=Convert.ToInt32(increaseprice),
                        sessionid=sessionid,
                        roomtype=roomtype,
                        descript=descript,
                        increasenum=Convert.ToInt32(increasenum),
                        increasedescribe=increasediscribe,
                        price=Convert.ToInt32(price),
                        pay=Convert.ToInt32(pay),
                    };
                    db.increase_order.Add(mp);
                    addLog(db, shopcode, oid, yemian, "添加", sessionid, "", logtype);

                    db.SaveChanges();
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":\"增加成功\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"增加失败\"}");
            }
        }

        public void baojia_list(HttpContext context)
        {
            string xzcode = Getpost("xzcode");
            string start = Getpost("start");
            string end = Getpost("end");
            string shopcode = Getpost("shopcode");
            string json = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.everydate_price_t.Where(x => x.everydate >= Convert.ToDateTime(start) && x.everydate <= Convert.ToDateTime(end) && x.xz_code == xzcode).ToList();
                    if (config.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(config);
                        context.Response.Write("{\"code\":\"200\",\"msg\":" + json + "}");
                    }
                    else
                    {
                        context.Response.Write("{\"code\":\"500\",\"msg\":\"未找到数据\"}");
                    }
                }
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"异常\"}");
            }
        }

        //public void increase_link_list(HttpContext context)
        //{
        //    string roomtype = Getpost("roomtype");
        //    string json = "";
        //    try
        //    {
        //        using (var db = new yudingEntities())
        //        {
        //            var config = db.increase_link.Where(x => x.roomtype == roomtype).ToList();
        //            if (config.Count > 0)
        //            {
        //                //foreach (int i in config)
        //                //{
        //                //    var ppo = db.increases.Where(x => x.increase_code == config[i].increase).ToList();
        //                //}
                        
        //                json = JsonConvert.SerializeObject(config);
        //                context.Response.Write("{\"code\":\"200\",\"msg\":" + json + "}");
        //            }
        //            else
        //            {
        //                context.Response.Write("{\"code\":\"500\",\"msg\":\"未找到数据\"}");
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        context.Response.Write("{\"code\":\"500\",\"msg\":\"异常\"}");
        //    }
        //}

        public void increase_update(HttpContext context)
        {
            string oid = Getpost("oid");
            string shopcode = Getpost("hotelid");
            string logtype = Getpost("logtype");
            string yemian = Getpost("yemian");

            string describe = Getpost("describe");
            string money = Getpost("money");
            string instruction = Getpost("instruction");
            string increasecode = Getpost("increasecode");
            string pp = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.increases.Where(x => x.increase_code == increasecode ).ToList();
                    if (config.Count > 0)
                    {
                        foreach (var item in config)
                        {
                            pp = item.instructions.ToString();
                            item.describe = describe;
                            item.money = money;
                            item.instructions = instruction;
                            addLog(db, shopcode, oid, yemian, "修改", instruction, pp, logtype);
                        }
                    }db.SaveChanges();
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":\"修改成功\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"修改失败\"}");
            }
        }

        public void increase_list(HttpContext context)
        {
            int type = Convert.ToInt32(Getpost("type"));
            string shopcode = Getpost("shopcode");
            string json = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.increases.Where(x => x.hotelid == shopcode && x.type == type).ToList();
                    if (config.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(config);
                        context.Response.Write("{\"code\":\"200\",\"msg\":" + json + "}");
                    }
                    else
                    {
                        context.Response.Write("{\"code\":\"500\",\"msg\":\"未找到数据\"}");
                    }
                }
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"异常\"}");
            }
        }

        public void increase_edit(HttpContext context)
        {
            string oid = Getpost("oid");
            string shopcode = Getpost("hotelid");
            string logtype = Getpost("logtype");
            string yemian = Getpost("yemian");

            string increasecode = Getpost("increasecode");
            string describe = Getpost("decribe");
            string money = Getpost("money");
            string instruction = Getpost("instruction");
            int type =Convert.ToInt32(Getpost("type"));
            try
            {
                using (var db = new yudingEntities())
                {
                    increase mp = new increase()
                    {
                        hotelid = shopcode,
                        increase_code=increasecode,
                        describe=describe,
                        money=money,
                        instructions=instruction,
                        type=type,
                    };
                    db.increases.Add(mp);
                    addLog(db, shopcode, oid, yemian, "添加", "", "", logtype);

                    db.SaveChanges();
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":\"增加成功\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"增加失败\"}");
            }
        }

        //

        public void openid_del(HttpContext context)
        {
            string oid = Getpost("oid");
            string shopcode = Getpost("hotelid");
            string logtype = Getpost("logtype");
            string yemian = Getpost("yemian");

            string id = Getpost("id");
            string json = "";

            try
            {
                using (var db = new yudingEntities())
                {
                    //var config = db.hotelfacilities_t.Where(x => x.hotelid == shopcode).ToList();
                    var config = db.Database.SqlQuery<hotelfacilities_t>("Delete from openid where id='"+id+"' ");
                    addLog(db, shopcode, oid, yemian, "删除", "",id, logtype);
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":\"删除成功\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"删除失败\"}");
            }
        }

        public void openid_list(HttpContext context)
        {
            string shopcode = Getpost("hotelid");

            int type = Convert.ToInt32(Getpost("type"));

            string json = "";

            try
            {
                using (var db = new yudingEntities())
                {
                    //var config = db.hotelfacilities_t.Where(x => x.hotelid == shopcode).ToList();
                    var config = db.openids.Where(x => x.hotelid == shopcode && x.type == type).ToList();
                    if (config.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(config);
                        context.Response.Write("{\"code\":\"200\",\"msg\":" + json + "}");
                    }
                    else
                    {
                        context.Response.Write("{\"code\":\"500\",\"msg\":\"未找到数据\"}");
                    }
                }
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"出错\"}");
            }
        }

        public void openid_edit(HttpContext context)
        {
            string oid = Getpost("oid");
            string shopcode = Getpost("hotelid");
            string logtype = Getpost("logtype");
            string yemian = Getpost("yemian");

            string openid = Getpost("openid");
            string name = Getpost("name");
            string tel = Getpost("tel");
            DateTime addtime = DateTime.Now;
            int type =Convert.ToInt32(Getpost("type"));
            try
            {
                using (var db = new yudingEntities())
                {
                    openid mp = new openid()
                    {
                        hotelid = shopcode,
                        openid1=openid,
                        name=name,
                        tel=tel,
                        
                        type=type,

                    };
                    db.openids.Add(mp);
                    addLog(db, shopcode, oid, yemian, "添加", "", "", logtype);

                    db.SaveChanges();
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":\"增加成功\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"增加失败\"}");
            }
        }

        public void rateroom_xz_dan_list(HttpContext context)
        {
            string xzcode = Getpost("xzcode");
            string json = "";

            try
            {
                using (var db = new yudingEntities())
                {
                    //var config = db.hotelfacilities_t.Where(x => x.hotelid == shopcode).ToList();
                    var config = db.rateroom_xz.Where(x => x.xz_code==xzcode).ToList();
                    if (config.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(config);
                        context.Response.Write("{\"code\":\"200\",\"msg\":" + json + "}");
                    }
                    else
                    {
                        context.Response.Write("{\"code\":\"500\",\"msg\":\"未找到数据\"}");
                    }
                }
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"出错\"}");
            }
        }

        public void weixin_time(HttpContext context)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            string time= ((long)(DateTime.Now- startTime).TotalSeconds).ToString();
            context.Response.Write("{\"code\":\"500\",\"msg\":\""+time+"\"}");
        }

        public void weixin_orderispay_update(HttpContext context)
        {
            string oid = Getpost("oid");
            string shopcode = Getpost("hotelid");
            string logtype = Getpost("logtype");
            string yemian = Getpost("yemian");

            int ispay =Convert.ToInt32(Getpost("ispay"));
            string ordernumber = Getpost("ordernumber");
            string pp = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.order_t.Where(x => x.ordernumber==ordernumber).ToList();
                    if (config.Count > 0)
                    {
                        foreach (var item in config)
                        {
                            pp = item.ispay.ToString();
                            item.ispay = ispay;

                            addLog(db, shopcode, oid, yemian, "修改", ispay.ToString(), Convert.ToString(pp), logtype);
                        }
                    } db.SaveChanges();
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":\"修改成功\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"修改失败\"}");
            }
        }

        public void weixin_ordersessionid_list(HttpContext context)
        {
            string sessionid = Getpost("sessionid");
            string hotelcode = Getpost("hotelcode");
            string json = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    //var config = db.hotelfacilities_t.Where(x => x.hotelid == shopcode).ToList();
                    var config = db.order_t.Where(x => x.sessionid == sessionid && x.hotelcode==hotelcode).ToList();
                    if (config.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(config);
                        context.Response.Write("{\"code\":\"200\",\"msg\":" + json + "}");
                    }
                    else
                    {
                        context.Response.Write("{\"code\":\"500\",\"msg\":\"未找到数据\"}");
                    }
                }
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"出错\"}");
            }
        }

        public void weixin_ordernum_list(HttpContext context)
        {
            string ordernumber = Getpost("ordernumber");
            string json = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    //var config = db.hotelfacilities_t.Where(x => x.hotelid == shopcode).ToList();
                    var config = db.order_t.Where(x => x.ordernumber == ordernumber ).ToList();
                    if (config.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(config);
                        context.Response.Write("{\"code\":\"200\",\"msg\":" + json + "}");
                    }
                    else
                    {
                        context.Response.Write("{\"code\":\"500\",\"msg\":\"未找到数据\"}");
                    }
                }
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"出错\"}");
            }
        }

        public void getHotelpicflag(HttpContext context)
        {
            string oid = Getpost("oid");
            string shopcode = Getpost("hotelid");
            string logtype = Getpost("logtype");
            string yemian = Getpost("yemian");

            int flag = Convert.ToInt32(Getpost("flag"));
            int id =Convert.ToInt32(Getpost("id"));
            string pp = "";
            try
            {
                using (var db = new yudingEntities())
                {
                    var config = db.hotelpic_t.Where(x => x.hotelcode == shopcode&&x.id==id).ToList();
                    if (config.Count > 0)
                    {
                        foreach (var item in config)
                        {
                            pp = item.flag.ToString();
                            item.flag = flag;
                            addLog(db, shopcode, oid, yemian, "修改", flag.ToString(), pp, logtype);
                        }
                    } db.SaveChanges();
                }
                context.Response.Write("{\"code\":\"200\",\"msg\":\"修改成功\"}");
            }
            catch
            {
                context.Response.Write("{\"code\":\"500\",\"msg\":\"修改失败\"}");
            }
        }
    }
}