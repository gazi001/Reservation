using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yuding
{
    public static class Config
    {
        public static string RedisUrl = "180.153.57.186";
        /// <summary>
        /// 查询微信订单接口地址
        /// </summary>
        public static string CheckWxPayUrl = "https://ks.kuaishun.net/WxAPI/API/GetPayOrder.ashx";

        /// <summary>
        /// 预订接口地址
        /// </summary>
        public static string BookingUrl = "https://ks.kuaishun.net/reservation/API/Reserve.ashx";

        /// <summary>
        ///发送模板消息接口地址
        /// </summary>
        public static string SendTemplateUrl = "https://ks.kuaishun.net/WxAPI/API/Template/SendTemplateMsg.ashx";

        public static string WxAPIUrl = "https://ks.kuaishun.net/WxAPI";


        public static string PTPSUrl = "http://180.153.57.186:9007/API/BackStage.ashx";
    }
}