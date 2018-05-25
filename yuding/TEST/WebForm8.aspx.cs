using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RM.Common.DotNetJson;
using yuding.API;

namespace yuding.TEST
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {//W1709120003
            ly ly = new ly("http://115.159.81.168:8100/ipmsgroup/router", "XYGMS");
            Dictionary<string, string> parms = new Dictionary<string, string>();

            parms.Add("arr","2017-9-12" + " 12:00:00");
            parms.Add("dep", "2017-9-13" + " 12:00:00");
            parms.Add("rmtype", "DR");
            parms.Add("channel", "WXB");
            parms.Add("rateCode", "WEB");
            parms.Add("rmNum","1");
            parms.Add("rsvMan", "测试");
            parms.Add("sex", "1");
            parms.Add("mobile", "15158726168");
            parms.Add("idType", "");
            parms.Add("idNo", "");
            parms.Add("email", "");
            parms.Add("cardType", "");
            parms.Add("cardNo", "");
            parms.Add("adult", "1");
            parms.Add("resultCode", "");
            parms.Add("remark", "含双早");
            parms.Add("everyDayRate", "[{\"date\":\"2017-9-12 \",\"realRate\":\"398\"}]");

            var result = ly.xiadan(parms, "http://115.159.81.168:8100/ipmsgroup/CRS/book");
            //success:
            var crsNo = JsonHelper.GetJsonValue(result, "crsNo");
        }
    }
}