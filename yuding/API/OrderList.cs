using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yuding.API
{
    public class OrderList
    {
        /// <summary>
        /// ResultCode
        /// </summary>
        public int resultCode { get; set; }
        /// <summary>
        /// 返回成功
        /// </summary>
        public string resultMessage { get; set; }
        /// <summary>
        /// ResultInfo
        /// </summary>
        public ResultInfo resultInfo { get; set; }
    }
    public class GuestList
    {
        /// <summary>
        /// ResrvId
        /// </summary>
        public int resrvId { get; set; }
        /// <summary>
        /// CRS2017092901094765
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
        /// 
        /// </summary>
        public string name2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nameCombine { get; set; }
        /// <summary>
        /// 1
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// C
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// HA
        /// </summary>
        public string race { get; set; }
        /// <summary>
        /// CN
        /// </summary>
        public string nation { get; set; }
        /// <summary>
        /// CN
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
        /// 0
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
        /// 01
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
        /// Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// LANDISON
        /// </summary>
        public string hotelGroupCode { get; set; }
        /// <summary>
        /// MGWSJSJD
        /// </summary>
        public string hotelCode { get; set; }
        /// <summary>
        /// MARK05
        /// </summary>
        public string createUser { get; set; }
        /// <summary>
        /// 2017-09-29
        /// </summary>
        public DateTime createDatetime { get; set; }
        /// <summary>
        /// MARK05
        /// </summary>
        public string modifyUser { get; set; }
        /// <summary>
        /// 2017-09-29
        /// </summary>
        public DateTime modifyDatetime { get; set; }
    }

    public class RateList
    {
        /// <summary>
        /// ResrvId
        /// </summary>
        public int resrvId { get; set; }
        /// <summary>
        /// CRS2017092901094765
        /// </summary>
        public string gcRsvNo { get; set; }
        /// <summary>
        /// RsvSrcId
        /// </summary>
        public int rsvSrcId { get; set; }
        /// <summary>
        /// RMFEE
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// JJDC
        /// </summary>
        public string rmtype { get; set; }
        /// <summary>
        /// 2017-09-30
        /// </summary>
        public DateTime rsvDate { get; set; }
        /// <summary>
        /// Rmnum
        /// </summary>
        public int rmnum { get; set; }
        /// <summary>
        /// Adult
        /// </summary>
        public int adult { get; set; }
        /// <summary>
        /// NegoRate
        /// </summary>
        public int negoRate { get; set; }
        /// <summary>
        /// RealRate
        /// </summary>
        public int realRate { get; set; }
        /// <summary>
        /// CostRate
        /// </summary>
        public int costRate { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public int value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// LANDISON
        /// </summary>
        public string hotelGroupCode { get; set; }
        /// <summary>
        /// MGWSJSJD
        /// </summary>
        public string hotelCode { get; set; }
        /// <summary>
        /// MARK05
        /// </summary>
        public string createUser { get; set; }
        /// <summary>
        /// 2017-09-29
        /// </summary>
        public DateTime createDatetime { get; set; }
        /// <summary>
        /// MARK05
        /// </summary>
        public string modifyUser { get; set; }
        /// <summary>
        /// 2017-09-29
        /// </summary>
        public DateTime modifyDatetime { get; set; }
    }

    public class ResultInfo
    {
        /// <summary>
        /// F
        /// </summary>
        public string rsvClass { get; set; }
        /// <summary>
        /// MKYL
        /// </summary>
        public string srcHotelGroupCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string srcHotelGroupDesc { get; set; }
        /// <summary>
        /// MKYL
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
        /// CRS
        /// </summary>
        public string otaChannel { get; set; }
        /// <summary>
        /// CRS
        /// </summary>
        public string productChannel { get; set; }
        /// <summary>
        /// 7c1522ec-a490-4d44-913d-542dc3854650
        /// </summary>
        public string otaRsvNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string otherRsvNo { get; set; }
        /// <summary>
        /// CRS2017092901094765
        /// </summary>
        public string gcRsvNo { get; set; }
        /// <summary>
        /// P1709290054
        /// </summary>
        public string crsNo { get; set; }
        /// <summary>
        /// 1709290010
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
        /// R
        /// </summary>
        public string sta { get; set; }
        /// <summary>
        /// MARK-A
        /// </summary>
        public string ratecode { get; set; }
        /// <summary>
        /// 简美大床房
        /// </summary>
        public string rmDesc { get; set; }
        /// <summary>
        /// JJDC
        /// </summary>
        public string rmtype { get; set; }
        /// <summary>
        /// Rmnum
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
        /// LANDISON
        /// </summary>
        public string hotelGroupCode { get; set; }
        /// <summary>
        /// MGWSJSJD
        /// </summary>
        public string hotelCode { get; set; }
        /// <summary>
        /// 安吉铭谷屋设计师酒店
        /// </summary>
        public string hotelDesc { get; set; }
        /// <summary>
        /// 2017-09-30
        /// </summary>
        public DateTime arr { get; set; }
        /// <summary>
        /// 2017-10-01
        /// </summary>
        public DateTime dep { get; set; }
        /// <summary>
        /// 2017-09-30 14:00
        /// </summary>
        public DateTime arrStr { get; set; }
        /// <summary>
        /// 2017-10-01 12:00
        /// </summary>
        public DateTime depStr { get; set; }
        /// <summary>
        /// 2017-09-30T14:00:00
        /// </summary>
        public string earlyArrTime { get; set; }
        /// <summary>
        /// 2017-10-01T12:00:00
        /// </summary>
        public string lastArrTime { get; set; }
        /// <summary>
        /// Adult
        /// </summary>
        public int adult { get; set; }
        /// <summary>
        /// Children
        /// </summary>
        public int children { get; set; }
        /// <summary>
        /// RMB
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// RateSum
        /// </summary>
        public int rateSum { get; set; }
        /// <summary>
        /// CS
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
        /// S
        /// </summary>
        public string companyType { get; set; }
        /// <summary>
        /// CompanyId
        /// </summary>
        public int companyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string companyDesc { get; set; }
        /// <summary>
        /// AgentId
        /// </summary>
        public int agentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string agentDesc { get; set; }
        /// <summary>
        /// SourceId
        /// </summary>
        public int sourceId { get; set; }
        /// <summary>
        /// 携程预付
        /// </summary>
        public string sourceDesc { get; set; }
        /// <summary>
        /// 53935
        /// </summary>
        public string pmsInnerNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string guarantee { get; set; }
        /// <summary>
        /// GuaranteeMoeny
        /// </summary>
        public int guaranteeMoeny { get; set; }
        /// <summary>
        /// CL
        /// </summary>
        public string payCode { get; set; }
        /// <summary>
        /// 0
        /// </summary>
        public string paySta { get; set; }
        /// <summary>
        /// Charge
        /// </summary>
        public int charge { get; set; }
        /// <summary>
        /// Pay
        /// </summary>
        public int pay { get; set; }
        /// <summary>
        /// 携程预付，双早，房价250元/间夜
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mktactDesc { get; set; }
        /// <summary>
        /// GuestList
        /// </summary>
        public List<GuestList> guestList { get; set; }
        /// <summary>
        /// CouponList
        /// </summary>
        public List<string> couponList { get; set; }
        /// <summary>
        /// LANDISON_MGWSJSJD_CRS_OTA_JJDC
        /// </summary>
        public string productCode { get; set; }
        /// <summary>
        /// 预付
        /// </summary>
        public string productDesc { get; set; }
        /// <summary>
        /// RateList
        /// </summary>
        public List<RateList> rateList { get; set; }
        /// <summary>
        /// PrepayHoldTimeMax
        /// </summary>
        public int prepayHoldTimeMax { get; set; }
        /// <summary>
        /// F
        /// </summary>
        public string isHide { get; set; }
        /// <summary>
        /// BreakfastNum
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
        /// RateSumAfterCoupon
        /// </summary>
        public int rateSumAfterCoupon { get; set; }
        /// <summary>
        /// CardId
        /// </summary>
        public int cardId { get; set; }
        /// <summary>
        /// MemberId
        /// </summary>
        public int memberId { get; set; }
        /// <summary>
        /// 2017-09-29 13:54:40
        /// </summary>
        public DateTime createDatetime { get; set; }
    }

    public class OrderRequest
    {
        /// <summary>
        /// LANDISON
        /// </summary>
        public string hotelGroupCode { get; set; }
        /// <summary>
        /// MGWSJSJD
        /// </summary>
        public string hotelCode { get; set; }
        /// <summary>
        /// CRS2017092901094765
        /// </summary>
        public string gcRsvNo { get; set; }
        /// <summary>
        /// CRS
        /// </summary>
        public string clientChannel { get; set; }
    }


}