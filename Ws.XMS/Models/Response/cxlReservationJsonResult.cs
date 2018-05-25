using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ws.XMS.Models.Response
{
   public class cxlReservationJsonResult:BaseJsonResult
    {
       public List<cancelInfo> results { get; set; }

    }
   public class cancelInfo
   {
       /// <summary>
       /// 
       /// </summary>
       public string resno { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string sta { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string err { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string msg { get; set; }
   }
}
