using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using RM.Common.DotNetEncrypt;
using RM.Common.DotNetHttp;
using yuding.JsonResult;


namespace yuding.API
{
    public class ly
    {
        public static string URL = Config.WxAPIUrl+"/API/GetWX.ashx?action=GetGZHxx";
        public static string APPSECRET = "5a993a1588a30b48e476bab058c34e22";
        public  string SessionId = "";
        public  HotelInfoJson info;
        public ly(string url,string hotelcode)
        {
            var postData = "hotelcode=" + hotelcode;
            var result = HttpHepler.SendPost(URL, postData);
            info = JsonConvert.DeserializeObject<HotelInfoJson>(result);
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("v", "3.0");
            p.Add("hotelGroupCode",info.data.FirstOrDefault().LvyunHotelDm);
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
            post = post.Substring(0, post.Length - 1);
            var lvresult = HttpHepler.SendPost(url, post);
            SessionId = JsonConvert.DeserializeObject<SessionIdJson>(lvresult).resultInfo;
        }
        public string xiadan(Dictionary<string, string> parms, string url)
        {
            parms.Add("hotelGroupId", info.data.FirstOrDefault().LvyunHotelgroupId);
            parms.Add("hotelId", info.data.FirstOrDefault().LvyunHotelId);
            parms.Add("sessionid", SessionId);
            parms.Add("appKey", "10029");
            parms.Add("rsvType",  info.data.FirstOrDefault().rsvType);
            parms.Add("market", info.data.FirstOrDefault().market);
            parms.Add("src",info.data.FirstOrDefault().src);
            var sign = DESEncrypt.getSign(parms, APPSECRET);
            parms.Add("sign", sign);
          
            var post = "";
            foreach (var item in parms)
            {
                post += item.Key + "=" + item.Value + "&";
            }
            //return post;
            return HttpHepler.SendPost(info.data.FirstOrDefault().orderUrl + "ipmsgroup/CRS/book", post);
        }
        public string Change(Dictionary<string, string> parms, string url)
        {
            parms.Add("hotelGroupId", info.data.FirstOrDefault().LvyunHotelgroupId);
            parms.Add("sessionid", SessionId);
            var sign = DESEncrypt.getSign(parms, APPSECRET);
            parms.Add("sign", sign);
           
            var post = "";
            foreach (var item in parms)
            {
                post += item.Key + "=" + item.Value + "&";
            }
            return HttpHepler.SendPost(info.data.FirstOrDefault().orderUrl + "ipmsgroup/CRS/appendResrvBaseRemark", post);
        }
        public string Cancel(Dictionary<string, string> parms, string url)
        {
            parms.Add("hotelGroupId", info.data.FirstOrDefault().LvyunHotelgroupId);
            parms.Add("sessionid", SessionId);
            parms.Add("appKey", "10029");
            var sign = DESEncrypt.getSign(parms, APPSECRET);
            parms.Add("sign", sign);
            var post = "";
            foreach (var item in parms)
            {
                post += item.Key + "=" + item.Value + "&";
            }
            return HttpHepler.SendPost(info.data.FirstOrDefault().orderUrl + "ipmsgroup/CRS/cancelbook", post);
        }
        public string GetOrderInfo(Dictionary<string, string> parms, string url)
        {
            parms.Add("sessionid", SessionId);
            parms.Add("v","");
            parms.Add("n", "");
            parms.Add("f", "800");
            parms.Add("q", ",,XYGMS,W1709120025");
            parms.Add("s", "");
            parms.Add("c", "c");
            var sign = DESEncrypt.getSign(parms, APPSECRET);
            parms.Add("sign", sign);
            var post = "";
            foreach (var item in parms)
            {
                post += item.Key + "=" + item.Value + "&";
            }
            return HttpHepler.SendPost(url, post);
        }

        
    }

    public class lytest
    {
        public static string URL = "http://183.134.78.209:9090/API/GetWX.ashx?action=GetGZHxx";
        public static string APPSECRET = "8b3d727f1ba1cde61cef63143ebab5e5";
        public static string SessionId = "";
        public static HotelInfoJson info;
        public lytest(string url, string hotelcode)
        {
            //var postData = "hotelcode=" + hotelcode;
            //var result = HttpHepler.SendPost(URL, postData);
           // info = JsonConvert.DeserializeObject<HotelInfoJson>(result);
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("v", "3.0");
            p.Add("hotelGroupCode", "GCBZG");
            p.Add("usercode", "gcbzg0");
            p.Add("method", "user.login");
            p.Add("local", "zh_CN");
            p.Add("format", "json");
            p.Add("password", "89kjanJD1k02b");
            p.Add("appKey", "10003");
            var sign = DESEncrypt.getSign(p, APPSECRET);
            p.Add("sign", sign);
            var post = "";
            foreach (var item in p)
            {
                post += item.Key + "=" + item.Value + "&";
            }
            post = post.Substring(0, post.Length - 1);
            var lvresult = HttpHepler.SendPost(url, post);
            SessionId = JsonConvert.DeserializeObject<SessionIdJson>(lvresult).resultInfo;
        }
        public string xiadan(Dictionary<string, string> parms, string url)
        {
            parms.Add("hotelGroupId", info.data.FirstOrDefault().LvyunHotelgroupId);
            parms.Add("hotelId", info.data.FirstOrDefault().LvyunHotelId);
            parms.Add("sessionid", SessionId);
            parms.Add("appKey", "10029");
            parms.Add("rsvType", "010");
            var sign = DESEncrypt.getSign(parms, APPSECRET);
            parms.Add("sign", sign);
          
            var post = "";
            foreach (var item in parms)
            {
                post += item.Key + "=" + item.Value + "&";
            }
            //return post;
           return HttpHepler.SendPost(url, post);
        }
        public string GetOrderInfo(string url)
        {
            Dictionary<string, string> parms = new Dictionary<string, string>();
            parms.Add("v", "");
            parms.Add("n", "");
            parms.Add("f", "800");
            parms.Add("q", ",,GCBZ,W1709220020");
            parms.Add("s", "");
            parms.Add("c", "c");

            var sign = DESEncrypt.getSign(parms, APPSECRET);
            parms.Add("sign", sign);
            var post = "";
            foreach (var item in parms)
            {
                post += item.Key + "=" + item.Value + "&";
            }
            return HttpHepler.SendPost(url, post);
        }
    }
}