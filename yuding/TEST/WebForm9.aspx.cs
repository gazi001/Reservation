using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using RM.Common.DotNetHttp;
using yuding.API;
using yuding.Model;

namespace yuding.TEST
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ly ly = new ly("http://115.159.81.168:8100/ipmsgroup/router", "XYGMS");
            //Dictionary<string, string> parms = new Dictionary<string, string>();
            //parms.Add("crsNo", "W1709120025");
            //parms.Add("hotelGroupId", "4");
            //parms.Add("mobile", "18989841642");
            //parms.Add("rsvman", "快顺测试");
            //var a = ly.GetOrderInfo(parms, " http://192.168.1.8:8101/ipmsgroup/CRS/findResrvGuestWithoutCardNo");

           //lytest ly = new lytest("http://183.129.215.114:7312/ipmsgroup/router", "GCBZG");

         //  Dictionary<string, string> parms = new Dictionary<string, string>();

          // var a = ly.GetOrderInfo(parms, "http://183.129.215.114:7310/ipms/hep/WebConnector");
            ly ly = new ly("http://115.159.81.168:8100/ipmsgroup/router", "CQYMDJD");
            Dictionary<string, string> parms = new Dictionary<string, string>();
            parms.Add("crsNo", "W1712270007");
            parms.Add("cardNo", "");
            var result = ly.Cancel(parms, "http://115.159.81.168:8100/ipmsgroup/CRS/cancelbook");
          
            //var a =  ly.GetOrderInfo("http://183.129.215.114:7310/ipms/hep/WebConnector");
          
            //success:

           // var db = new yudingEntities();
            //var q = (from a in db.order_t where a.hotelcode == "XYGMS" && a.sessionid == "XYGMS201709151902194718" select a)
            //         .GroupBy(x => new { x.sessionid })
            //         .Select(group => new
            //         {
            //             ispay = group.FirstOrDefault().ispay,
            //             sessionid = group.FirstOrDefault().sessionid,
            //             totalrate = group.Sum(x => x.rate),
            //             yhmoney = group.Sum(x => x.yhmoney),
            //             name = group.FirstOrDefault().contact_name,
            //             addtime = group.FirstOrDefault().addtime,
            //             count = group.Sum(x => x.count),
            //             mobile = group.FirstOrDefault().contact_mobile,
            //             arr = group.Min(x => x.arrivetime),
            //             dep = group.Max(x => x.leavetime),
            //         }).ToList();
            //var order = db.order_t.Where(x => x.ispay == 0).GroupBy(x => x.sessionid).ToList();
            //ly ly = new ly("http://115.159.81.168:8100/ipmsgroup/router", "XYGMS");
            //Dictionary<string, string> parms = new Dictionary<string, string>();
            //parms.Add("crsNo", "W1709130039");
            //parms.Add("cardNo", "");
            //var result = ly.Cancel(parms, "http://115.159.81.168:8100/ipmsgroup/CRS/cancelbook");
           // var paramData = new
           // {
           //     first = new
           //     {
           //         value = "尊敬的赵正豪阁下，您的订单超过支付时间已取消",
           //         color = "#173177",
           //     },
           //     keyword1 = new
           //     {
           //         value = "1234566",
           //         color = "#173177",
           //     },
           //     keyword2 = new
           //     {
           //         value ="逍遥谷民宿",
           //         color = "#173177",
           //     },
           //     keyword3 = new
           //     {
           //         value ="2017-9-13",
           //         color = "#173177",
           //     },
           //     keyword4 = new
           //     {
           //         value = "2017-9-14",
           //         color = "#173177",
           //     },
           //     keyword5 = new
           //     {
           //         value = "大床房",
           //         color = "#173177",
           //     },
           //     reamrk = new
           //     {
           //         value = "期待您的下次光临！",
           //         color = "#173177",
           //     },
           // };
           // var json = JsonConvert.SerializeObject(paramData);
           // var postData = "hotelcode=XYGMS&openid=oLAULwLRGMJgC-vDy7uxVb2rNeGw&param=" + json + "&templateName=neworder";
           //var a= HttpHepler.SendPost("http://183.134.78.209:9090/API/Template/SendTemplateMsg.ashx", postData);
        }
    }
}