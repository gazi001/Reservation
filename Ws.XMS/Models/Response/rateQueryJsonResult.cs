using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws.XMS.Models.Response
{
  public  class rateQueryJsonResult:BaseJsonResult
    {
      public List<RateQuery> results { get; set; }

    }
  public class RateQuery
  {
      /// <summary>
      /// 
      /// </summary>
      public string ratecodetag { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string ratecode { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string des { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string packages { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string pkgdes { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string cxlrules { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string detailinfo1 { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string rmtype { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string descript { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string avail { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string cxldes { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string cxldes2 { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string descript2 { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string cxldes1 { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string descript1 { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string guades { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string guaranteerules { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string rate { get; set; }
      /// <summary>
      /// Dailyrates
      /// </summary>
      public List<DailyRates> dailyrates { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string hotelid { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string seq { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string guades1 { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string guades2 { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string detailinfo { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string isvip { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string ratecodeinfo { get; set; }
  }
  public class DailyRates
  {
      /// <summary>
      /// 
      /// </summary>
      public string rate { get; set; }
      /// <summary>
      /// 
      /// </summary>
      public string date { get; set; }
  }
}
