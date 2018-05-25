using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using RM.Common.DotNetHttp;

namespace yuding.TEST
{
    public partial class WebForm12 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var res = new net.kuaishun.ticketmk.Service();
            var s = res.Getmember_bymobile_json("","","15","18989841642");

            var paramData = new
            {
                first = new
                {
                    value = "ceshi",
                    color = "#173177",
                },
                keyword1 = new
                {
                    value = "ceshi",
                    color = "#173177",
                },
                keyword2 = new
                {
                    value = "ceshi",
                    color = "#173177",
                },
                keyword3 = new
                {
                    value = DateTime.Now.ToString("yyyy-MM-dd"),
                    color = "#173177",
                },
                reamrk = new
                {
                    value = "如有疑问请及时联系我们:" ,
                    color = "#173177",
                },
            };
            var json = JsonConvert.SerializeObject(paramData);
            var a = "hotelcode=HZKYJD&openid=oCVOQjvJtjbRR2nQ4o-8Gw1Z3OcI&param=" + json + "&templateName=PaySuccess";
            var r = HttpHepler.SendPost("https://ks.kuaishun.net/WxAPI/API/Template/SendTemplateMsg.ashx", a);
        }
    }
}