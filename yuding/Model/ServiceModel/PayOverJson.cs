using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yuding.Model.ServiceModel
{
    public class PayOverJson
    {
        public string Sessionid { get; set; }
        public string InvoiceId { get; set; }
        public string PayWay { get; set; }
        public string Transaction_id { get; set; }
        public string Transaction_Ali_id { get; set; }
    }
}