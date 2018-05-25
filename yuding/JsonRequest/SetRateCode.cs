using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using yuding.Model;

namespace yuding.JsonRequest
{
    public class SetRateCode
    {
        public lvyun_link qudao { get; set; }
        public ly_ratecode_link pmsratecode { get; set; }
        public ratecode_t ratecode { get; set; }
    }
}