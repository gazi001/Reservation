using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yuding.Model.ServiceModel
{
    public class OrderQueryResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string device_info { get; set; }
        /// <summary>
        /// oypMdv1H5RuTAQafJzjQ6SJ4glpw
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// Y
        /// </summary>
        public string is_subscribe { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sub_openid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sub_is_subscribe { get; set; }
        /// <summary>
        /// JSAPI
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// SUCCESS
        /// </summary>
        public string trade_state { get; set; }
        /// <summary>
        /// CFT
        /// </summary>
        public string bank_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string detail { get; set; }
        /// <summary>
        /// 1
        /// </summary>
        public string total_fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string settlement_total_fee { get; set; }
        /// <summary>
        /// CNY
        /// </summary>
        public string fee_type { get; set; }
        /// <summary>
        /// 1
        /// </summary>
        public string cash_fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cash_fee_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coupon_fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coupon_count { get; set; }
        /// <summary>
        /// Coupon_type_values
        /// </summary>
        public List<string> coupon_type_values { get; set; }
        /// <summary>
        /// Coupon_id_values
        /// </summary>
        public List<string> coupon_id_values { get; set; }
        /// <summary>
        /// Coupon_fee_values
        /// </summary>
        public List<string> coupon_fee_values { get; set; }
        /// <summary>
        /// 4200000011201711236540912135
        /// </summary>
        public string transaction_id { get; set; }
        /// <summary>
        /// KSHZ201711231638472789
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string attach { get; set; }
        /// <summary>
        /// 20171123164400
        /// </summary>
        public string time_end { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trade_state_desc { get; set; }
        /// <summary>
        /// wxe4871415a01e8b24
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 1348381901
        /// </summary>
        public string mch_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sub_appid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sub_mch_id { get; set; }
        /// <summary>
        /// fDhbCEiVnjnO1JpV
        /// </summary>
        public string nonce_str { get; set; }
        /// <summary>
        /// 7DE1803F31EE2FD52D75AB0B6AC3213D
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// SUCCESS
        /// </summary>
        public string result_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string err_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string err_code_des { get; set; }
        /// <summary>
        /// SUCCESS
        /// </summary>
        public string return_code { get; set; }
        /// <summary>
        /// Return_msg
        /// </summary>
        public string return_msg { get; set; }
    }
}