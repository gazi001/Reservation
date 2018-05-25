using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yuding.API
{
    public enum Operation
    {
        添加=0,
        删除=1,
        修改=2,
    }
    public enum Pay
    {
        卡券支付=0,
        微信预付=1,
        前台现付=2,
        票券核销=3,
 
    }
}