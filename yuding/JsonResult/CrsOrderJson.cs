using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yuding.JsonResult
{
    public class CrsOrderJson
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
        public ResultInfo resultInfo { get; set; }
    }
    public class GuestListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int resrvId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gcRsvNo { get; set; }
        /// <summary>
        /// 李滕,李正贵
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 李滕,李正贵
        /// </summary>
        public string lastName { get; set; }
        /// <summary>
        /// 李滕,李正贵
        /// </summary>
        public string firstName { get; set; }
        /// <summary>
        /// 李滕,李正贵
        /// </summary>
        public string name2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name3 { get; set; }
        /// <summary>
        /// 李滕,李正贵李滕,李正贵
        /// </summary>
        public string nameCombine { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string race { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string division { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string street { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zipcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string vip { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string idCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string idNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string enterPort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
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
    }
    public class RateListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int resrvId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gcRsvNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int rsvSrcId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rmtype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rsvDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int rmnum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int adult { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal negoRate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal realRate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal costRate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
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
    }
    public class ResultInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string rsvClass { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string srcHotelGroupCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string srcHotelGroupDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string srcMemberGroupCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string srcHotelCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string srcHotelDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string otaChannel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string productChannel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string otaRsvNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string otherRsvNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gcRsvNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string crsNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rsvNo { get; set; }
        /// <summary>
        /// 李滕,李正贵
        /// </summary>
        public string rsvMan { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rsvCompany { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sta { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ratecode { get; set; }
        /// <summary>
        /// 简美大床房
        /// </summary>
        public string rmDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rmtype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int rmnum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string specials { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string amenities { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hotelGroupCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hotelCode { get; set; }
        /// <summary>
        /// 安吉铭谷屋设计师酒店
        /// </summary>
        public string hotelDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string arr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dep { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string arrStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string depStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string earlyArrTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lastArrTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int adult { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int children { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal rateSum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string balanceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string memberType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string memberDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string memberNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string srcMemberLevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string companyType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int companyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string companyDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int agentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string agentDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sourceId { get; set; }
        /// <summary>
        /// 携程预付
        /// </summary>
        public string sourceDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pmsInnerNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string guarantee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal guaranteeMoeny { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string payCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string paySta { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal charge { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal pay { get; set; }
        /// <summary>
        /// 携程预付，双早，房价250元/间夜
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mktactDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<GuestListItem> guestList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> couponList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string productCode { get; set; }
        /// <summary>
        /// 预付
        /// </summary>
        public string productDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RateListItem> rateList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int prepayHoldTimeMax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isHide { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int breakfastNum { get; set; }
        /// <summary>
        /// Ctrip携程,4587675888
        /// </summary>
        public string otaRemark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string srcMemberNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal rateSumAfterCoupon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int cardId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int memberId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createDatetime { get; set; }
    }
}