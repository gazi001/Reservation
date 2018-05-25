using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ks_Model
{
     [XmlRoot(ElementName = "Request")]
    public class ReqSearchRoomType
    {
        public Head Head { get; set; }
        public ReqSearchRoomType_Body Body { get; set; }
    }
    public class ReqSearchRoomType_Body { }
}
