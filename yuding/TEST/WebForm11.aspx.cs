using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using yuding.BLL;
using yuding.DAL;
using yuding.Model;

namespace yuding.TEST
{
    public partial class WebForm11 : System.Web.UI.Page
    {
        net.kuaishun.fx.SendSms s = new net.kuaishun.fx.SendSms();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                using (yudingEntities db = new yudingEntities())
                {
                    var weatherData = new Root();
                    var tomorrow = Convert.ToDateTime(DateTime.Now.AddDays(+1).ToString("yyyy-MM-dd"));
                    var today = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    var list = db.order_t.Where(x => x.ispay == 1 && x.state == "R" &&(x.arrivetime == tomorrow ||x.arrivetime==today)&& x.isSendMsg == "0").GroupBy(x => x.sessionid).ToList();
                    var city = "";
                    if (list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            var mobile = item.FirstOrDefault().contact_mobile;
                            var sessionid = item.FirstOrDefault().sessionid;
                            string roomname = "";
                            foreach (var item1 in item)
                            {
                                roomname += "【" + item.FirstOrDefault().hotelname + "】" + item1.arrivetime.ToString() + item1.roomname + item1.count + "间，";
                            }
                            if (item.FirstOrDefault().City == city)
                            {
                                if (item.FirstOrDefault().City != "" && item.FirstOrDefault().City != null)
                                {
                                    item.FirstOrDefault().City = city;
                                    var shopcode = item.FirstOrDefault().hotelcode;
                                    var modelid = HttpPost("http://fx.kuaishun.net/Sms/Api/GetTemplate.ashx", "hotelcode=" + shopcode + "&type=TQYB");
                                    var modelxx = JsonConvert.DeserializeObject<GetModelId>(modelid);
                                    var SendMsg = s.SendSameSms(mobile, modelxx.data.TemplateId, sessionid + "," + roomname + "," + city + "," + weatherData.result.today.weather + "," + weatherData.result.today.temperature, "", shopcode);
                                    var result = JsonConvert.DeserializeObject<SendMsgResult>(SendMsg);
                                    if (result.Msg == "00")
                                    {
                                        var data = db.order_t.Where(x => x.sessionid == sessionid).ToList();
                                        foreach (var item2 in data)
                                        {
                                            item2.isSendMsg = "1";
                                        }
                                        db.SaveChanges();
                                        //WritenLog(DateTime.Now.ToString() + ":后台服务发送一条天气预报短信，发送给" + mobile + "," + "批次号：" + sessionid, CurrentPath);
                                    }
                                }
                            }
                            else
                            {
                                city = item.FirstOrDefault().City;
                                if (city != null && city != "")
                                {
                                    weatherData = JsonConvert.DeserializeObject<Root>(GetFunction(city));
                                    var shopcode = item.FirstOrDefault().hotelcode;
                                    var modelid = HttpPost("http://fx.kuaishun.net/Sms/Api/GetTemplate.ashx", "hotelcode=" + shopcode + "&type=TQYB");
                                    var modelxx = JsonConvert.DeserializeObject<GetModelId>(modelid);
                                    var SendMsg = s.SendSameSms(mobile, modelxx.data.TemplateId, sessionid + "," + roomname + "," + city + "," + weatherData.result.today.weather + "," + weatherData.result.today.temperature, "", shopcode);
                                    var result = JsonConvert.DeserializeObject<SendMsgResult>(SendMsg);
                                    if (result.Msg == "00")
                                    {
                                        var data = db.order_t.Where(x => x.sessionid == sessionid).ToList();
                                        foreach (var item2 in data)
                                        {
                                            item2.isSendMsg = "1";
                                        }
                                        db.SaveChanges();
                                       // WritenLog(DateTime.Now.ToString() + ":后台服务发送一条天气预报短信，发送给" + mobile + "," + "批次号：" + sessionid, CurrentPath);
                                    }
                                }
                            }
                            // var SendMsg = s.SendSameSms(item.FirstOrDefault().contact_mobile, "38425", "" + tq.result.today.weather + "," + tq.result.today.temperature + "," + tq.result.today.dressing_advice + "", "", "KSHZ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //WritenLog(ex + "-------" + DateTime.Now.ToString(), CurrentPath);

            }
        }
        public static string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataStr.Length;
            StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);

            writer.Write(postDataStr);
            writer.Flush();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码 
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            return retString;
        }
        public string GetFunction(string place)
        {
            string key = "abfbbff02d2d5d52ac6d188c08bfb229";
            string serviceAddress = "http://v.juhe.cn/weather/index?cityname=" + place + "&dtype=json&format=2&key=" + key + "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
            //Response.Write(retString);
        }
    }
    public class SendMsgResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Body { get; set; }
    }
    public class DataResult
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TemplateId { get; set; }
        /// <summary>
        /// 行程提醒服务 订单{0}，您已预订{1}酒店城市：{2}，{3}，{4}。祝您入住愉快！
        /// </summary>
        public string TemplateContent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HotelCode { get; set; }
        /// <summary>
        /// 【快顺科技】
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ParamCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Epid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PassWord { get; set; }
    }
    public class Sk
    {
        /// <summary>
        /// 
        /// </summary>
        public string temp { get; set; }
        /// <summary>
        /// 东南风
        /// </summary>
        public string wind_direction { get; set; }
        /// <summary>
        /// 1级
        /// </summary>
        public string wind_strength { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string humidity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string time { get; set; }
    }
    public class GetModelId
    {
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 获取成功
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataResult data { get; set; }
    }
    public class Weather_id
    {
        /// <summary>
        /// 
        /// </summary>
        public string fa { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fb { get; set; }
    }

    public class Today
    {
        /// <summary>
        /// 
        /// </summary>
        public string temperature { get; set; }
        /// <summary>
        /// 多云转小雨
        /// </summary>
        public string weather { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Weather_id weather_id { get; set; }
        /// <summary>
        /// 东南风微风
        /// </summary>
        public string wind { get; set; }
        /// <summary>
        /// 星期二
        /// </summary>
        public string week { get; set; }
        /// <summary>
        /// 杭州
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 2017年10月10日
        /// </summary>
        public string date_y { get; set; }
        /// <summary>
        /// 炎热
        /// </summary>
        public string dressing_index { get; set; }
        /// <summary>
        /// 天气炎热，建议着短衫、短裙、短裤、薄型T恤衫等清凉夏季服装。
        /// </summary>
        public string dressing_advice { get; set; }
        /// <summary>
        /// 弱
        /// </summary>
        public string uv_index { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string comfort_index { get; set; }
        /// <summary>
        /// 不宜
        /// </summary>
        public string wash_index { get; set; }
        /// <summary>
        /// 较适宜
        /// </summary>
        public string travel_index { get; set; }
        /// <summary>
        /// 较适宜
        /// </summary>
        public string exercise_index { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string drying_index { get; set; }
    }

    public class Weather_Id
    {
        /// <summary>
        /// 
        /// </summary>
        public string fa { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fb { get; set; }
    }

    public class FutureItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string temperature { get; set; }
        /// <summary>
        /// 多云转小雨
        /// </summary>
        public string weather { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Weather_id weather_id { get; set; }
        /// <summary>
        /// 东南风微风
        /// </summary>
        public string wind { get; set; }
        /// <summary>
        /// 星期二
        /// </summary>
        public string week { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string date { get; set; }
    }

    public class Result
    {
        /// <summary>
        /// 
        /// </summary>
        public Sk sk { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Today today { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<FutureItem> future { get; set; }
    }

    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public string resultcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reason { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Result result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int error_code { get; set; }
    }
}