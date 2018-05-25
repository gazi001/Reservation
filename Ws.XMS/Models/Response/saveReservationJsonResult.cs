using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws.XMS.Models.Response
{
   public class saveReservationJsonResult:BaseJsonResult
    {
       public List<reservationInfo> results { get; set; }
       
    }
   public class reservationInfo
   {
       /// <summary>
       /// 
       /// </summary>
       public string pay_money { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string money { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string rsvno { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string accnt { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string msg { get; set; }
   }

}
