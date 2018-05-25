using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuding.JsonResult
{
    public interface IJsonResult
    {
        string code { get; set; }
        string msg { get; set; }

    }
    public interface IPaginationJsonResult : IJsonResult
    {
        string pageindex { get; set; }
        string pagecount { get; set; }
        object data { get; set; }
    }
}
