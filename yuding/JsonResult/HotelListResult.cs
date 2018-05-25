using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yuding.JsonResult
{
    public class HotelList
    {
        /// <summary>
        /// 
        /// </summary>
        public string srcHotelGroupCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hotelGroupCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hotelCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public GcHotel gcHotel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal minPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isSelf { get; set; }
    }
    public class GcHotel
    {
        /// <summary>
        /// 伊美酒店集团
        /// </summary>
        public string hotelGroupCodeDescript { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hotelGroupCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sta { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string audit { get; set; }
        /// <summary>
        /// 重庆伊美大酒店
        /// </summary>
        public string descript { get; set; }
        /// <summary>
        /// 重庆伊美大酒店
        /// </summary>
        public string descriptEn { get; set; }
        /// <summary>
        /// 重庆伊美大酒店
        /// </summary>
        public string descriptShort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 重庆
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 重庆
        /// </summary>
        public string address1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string address2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string phoneRsv { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string website { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dns { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string photo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string htmlInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int listOrder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createDatetime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string modifyUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string modifyDatetime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string provinceCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cityCode { get; set; }
        /// <summary>
        /// 重庆伊美大酒店
        /// </summary>
        public string districtCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string townCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shoppingDistrictCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scenicSpotCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bookListOrder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string startLevelCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string brandCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string categoryCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string manageType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pairPic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string appType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string onlineCheck { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string clientType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string wechatName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pmsBrand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string resrvSync { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string starLevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double minPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double hotelLongitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double hotelLatitude { get; set; }
    }
 
    public class HotelListResult
    {

        /// <summary>
        /// 
        /// </summary>
        public int resultCode { get; set; }
        /// <summary>
        /// 返回成功
        /// </summary>
        public string resultMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<HotelList> resultInfos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int totalRows { get; set; }
    }
}