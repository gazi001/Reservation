using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuding.JsonResult;

namespace yuding.IBLL
{
  public  interface IBookingWeb
    {
      List<RateXzByRmType> GetRateCodexzByRoomtype(string starttimeStr, string endtimeStr, string roomtype, string hotelcode);
    }
}
