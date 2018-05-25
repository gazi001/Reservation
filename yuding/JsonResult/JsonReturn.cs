using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yuding.JsonResult
{
    public class JsonReturn : IJsonResult
    {
        public string code { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }
    public class JsonReturnPagination : IPaginationJsonResult
    {

        public string pageindex{ get; set; }
        

        public string pagecount{ get; set; }
      

        public object data{ get; set; }
      

        public string code{ get; set; }


        public string msg { get; set; }
        
    }
     
}