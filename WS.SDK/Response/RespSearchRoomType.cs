using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ks_Model
{
    public class RespSearchRoomType
    {
        public RespSearchRoomType_Head Head { get; set; }

        public RespSearchRoomType_Body Body { get; set; }
    }
    public class RespSearchRoomType_Head
    {
        public string transcode { get; set; }
        public string systype { get; set; }
        public string retcode { get; set; }
        public string retmsg { get; set; }
    }
    public class RespSearchRoomType_Body
    {
        public ResponseBodyRmtype ResponseBodyRmtype { get; set; }
    }
    public class ResponseBodyRmtype
    {
       [XmlElement(ElementName = "Rmtype")]
        public List< Rmtype> Rmtype { get; set; }
        public string responsecode { get; set; }
        public string responsemsg { get; set; }
    }
    public class Rmtype
    {
        public string rmtype { get; set; }
        public string descript { get; set; }
        public string descript1 { get; set; }
    }
}
