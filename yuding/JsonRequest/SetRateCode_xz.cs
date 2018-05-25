using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using yuding.Model;

namespace yuding.JsonRequest
{
    public class SetRateCode_xz
    {
        public rateroom_xz base_info { get; set; }
        public List<week> weekInfo { get; set; }
    }
    public class Advanced
    {
        public baojia_t baojia { get; set; }
        public miaosha_t xianshi { get; set; }
        public associated codeid { get; set; }
       
        public List<price_formulaid> categoryid { get; set; }
    }
    public class EveryDatePrice
    {
        public string starttime { get; set; }
        public string endtime { get; set; }
        public List<string> xz_code { get; set; }
    }
    public class CopyRateCode_xz
    {
        public string xz_code { get; set; }
        public string package { get; set; }
        public List<week> index { get; set; }
    }
    public class info
    {
        public string pic { get; set; }
        public string roomtype { get; set; }
        public string roomname { get; set; }
        public string price { get; set; }
        public Nullable<decimal> minprice { get; set; }
        public List<xz> xz { get; set; }
       // public List<rateroom_xz> xz { get; set; }
    }
    public class xz
    {
        public string xz_name { get; set; }
        public string xz_code { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<int> package { get; set; }
    }
    public class every
    {
        public decimal price { get; set; }
        public string date { get; set; }
        public int num { get; set; }
        public int islock { get; set; }
        public int package { get; set; }
    }
    public class roomlist
    {
        public string roomtype { get; set; }
        public string roomname { get; set; }
        public List<price> pricelist { get; set; }
    }
    public class price
    {
        public string xz_name { get; set; }
        public string xz_code { get; set; }
        public Nullable<decimal> price1 { get; set; }
        public Nullable<int> package { get; set; }
    }

    public class order
    {
        public string ptchannel { get; set; }
        public string City { get; set; }
        public string channelid { get; set; }
        public string hotelname { get; set; }
        public string tpid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roomtype { get; set; }
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
        public string hotelcode { get; set; }
        /// <summary>
        /// zhu+测试
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string count { get; set; }
        /// <summary>
        /// 标准房
        /// </summary>
        public string roomname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int increasemoney { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 按人数 
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ordernumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string truerate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal rate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string resby { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string formula { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string payway { get; set; }
        /// <summary>
        /// 前台现付
        /// </summary>
        public string xz_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xz_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string baojia { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sessionid { get; set; }
        public string yhmoney { get; set; }
        public string formulaid { get; set; }
        public string categoryid { get; set; }
        public string guestname { get; set; }
        public string type { get; set; }
        public string TicketSn { get; set;}
        public decimal Fmoney { get; set; }
        public List<everyDayRate> everydate { get; set; }
    }
    public class everyDayRate
    {
        public string date { get; set; }
        public decimal realRate { get; set; }
        public decimal yhmoney { get; set; }
    }
    public class order_increase
    {
        /// <summary>
        /// 加床
        /// </summary>
        public string increasedescribe { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string increasenum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string increaseprice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roomtype { get; set; }
        /// <summary>
        /// 标准房
        /// </summary>
        public string roomname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hotelcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int tian { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xz_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sessionid { get; set; }
    }
}