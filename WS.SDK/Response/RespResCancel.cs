using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ks_Model
{
    public class RespResCancel
    {
        public RespResCancel_Head Head { get; set; }
        public RespResCancel_Body Body { get; set; }
    }
    public class RespResCancel_Head
    {
        public string retcode { get; set; }
        public string retmsg { get; set; }
    }
    public class RespResCancel_Body
    {
        public Result Result { get; set; }
    }
    public class Result
    {
        public ResultObject ResultObject { get; set; }
    }
    public class ResultObject
    {
        public string Code { get; set; }
        public string Descript { get; set; }
    }
}
