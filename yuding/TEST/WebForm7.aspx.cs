using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using RM.Common.DotNetEncrypt;
using RM.Common.DotNetHttp;
using yuding.JsonResult;

namespace yuding.TEST
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("v", "3.0");
            p.Add("hotelGroupCode", "YMJDJTG");
            p.Add("usercode", "wubiaoteng");
            p.Add("method", "user.login");
            p.Add("local", "zh_CN");
            p.Add("format", "json");
            p.Add("password", "r3qt8fY5fHj9s");
            p.Add("appKey", "10029");
            var sign = DESEncrypt.getSign(p, "5a993a1588a30b48e476bab058c34e22");
            p.Add("sign", sign);
            var post = "";
            foreach (var item in p)
            {
                post += item.Key + "=" + item.Value + "&";
            }
            Response.Write(post);
           // post = post.Substring(0, post.Length - 1);
            var lvresult = HttpHepler.SendPost("http://115.159.81.168:8100/ipmsgroup/router", post);
            //var SessionId = JsonConvert.DeserializeObject<SessionIdJson>(lvresult).resultInfo;
        }
    }
}