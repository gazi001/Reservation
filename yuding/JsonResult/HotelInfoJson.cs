using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yuding.JsonResult
{
    public class HotelInfoJson
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
        public List<DataItem> data { get; set; }
    }
    public class DataItem
    {
        public string src { get; set; }
        public string market { get; set; }
        public string orderUrl { get; set; }
        public string rsvType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ShopCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MchId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MchKEY { get; set; }
        /// <summary>
        /// 快顺信息
        /// </summary>
        public string HotelName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tenPayV3Notify { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string addtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LvyunHotelgroupId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LvyunHotelDm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LvyunHotelId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LvyunHotelname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string YQTHotelgrouptype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string YQTGiftId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string YQTGiftSonId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string YQTregisterId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string YQTOperatorId { get; set; }
        /// <summary>
        /// 快顺信息
        /// </summary>
        public string gzh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string WxNO { get; set; }
    }

   
}