using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws.XMS.Models.Response
{
   public class getRoomTypeJsonRsult:BaseJsonResult
    {
       public List<roomTypeJson> results { get; set; }
   
    }
   public class roomTypeJson
   {
       /// <summary>
       /// 
       /// </summary>
       public string des1 { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string bed_type { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string des { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string window_type { get; set; }
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
       public string bedtype1 { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string people { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string smoke { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string floor1 { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string addbed { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string floor { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string hoteldes { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string square1 { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string square { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string windowtype1 { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string addbed1 { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string smoke1 { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string detailinfo { get; set; }
   }
}
