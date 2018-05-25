using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using yuding.Model;

using System.Data;
using System.Data.Entity.Infrastructure;
using yuding.JsonRequest;
using RM.Common.DotNetHttp;
namespace yuding.TEST
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                var postData = "hotelcode=KSHZ";
                var result = HttpHepler.SendPost(URL, postData);
                Response.Write(result);
            }

        }
        public string URL = "https://ks.kuaishun.net/WxAPI/API/Config/HotelMappingApi.ashx?action=GetMapping";
        private void GetRoomList()
        {
            var postData = "hotelcode=KSHZ";
            var result = HttpHepler.SendPost(URL, postData);
 
        }
    }
 
}