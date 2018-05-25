using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Ks_Model
{
   public class Common
    {
       //xml转实体
       public static object Deserialize(Type type, string xml)
       {
           try
           {

               xml = xml.Replace("<Response x", "<"+type.Name+" x");
               xml = xml.Replace("</Response>", "</"+type.Name+">");
               using (StringReader sr = new StringReader(xml))
               {
                   XmlSerializer xmldes = new XmlSerializer(type);
                   return xmldes.Deserialize(sr);
               }
           }
           catch (Exception e)
           {

               return null;
           }
       }


       //实体转xml

       public static string ObjectToXmlSerializer(Object Obj)
       {
           string XmlString = "";
           XmlWriterSettings settings = new XmlWriterSettings();
           //去除xml声明
           //settings.OmitXmlDeclaration = true;
           settings.Indent = true;
           settings.Encoding = Encoding.UTF8;
           
           using (System.IO.MemoryStream mem = new MemoryStream())
           {
               using (XmlWriter writer = XmlWriter.Create(mem, settings))
               {
                   //去除默认命名空间xmlns:xsd和xmlns:xsi
                  // XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                   //ns.Add("", "");
                   XmlSerializer formatter = new XmlSerializer(Obj.GetType());
                   formatter.Serialize(writer, Obj);
               }
               XmlString = Encoding.UTF8.GetString(mem.ToArray());
           }
           var name = Obj.GetType().Name;
           return XmlString.Replace(name, "Request");
       }

       public static string SerializeToXml(object srcObject, Type type)
       {
           type = type != null ? type : srcObject.GetType();
           XmlSerializer xs = new XmlSerializer(type);
           StringBuilder stb = new StringBuilder();
           XmlWriter xw = XmlWriter.Create(stb, new XmlWriterSettings() { Encoding = UTF8Encoding.UTF8 });
           xs.Serialize(xw, srcObject);
           return stb.ToString().Replace("utf-16","utf-8");
       }

       public static string HttpPost(string Url, string MethodName, string postXml)
       {
           string MessInfo = string.Empty;
           try
           {
               //请求
               WebRequest request = WebRequest.Create(Url);
               //请求方式
               request.Method = "POST";
               //标头的类型
               request.ContentType = "text/xml";
               //超时时间(毫秒)
               request.Timeout = 20000;
               //SOAP请求方法
               request.Headers.Add("SOAPAction", MethodName);
               //转化发送的数据
               byte[] byteArray = Encoding.UTF8.GetBytes(postXml);
               //标头的长度
               request.ContentLength = byteArray.Length;
               Stream newStream = request.GetRequestStream();
               newStream.Write(byteArray, 0, byteArray.Length);//写入参数
               newStream.Close();
               HttpWebResponse responseSorce = (HttpWebResponse)request.GetResponse();
               StreamReader Reader = new StreamReader(responseSorce.GetResponseStream(), Encoding.UTF8);
               MessInfo = Reader.ReadToEnd();
               Reader.Close();
               responseSorce.Close();
               newStream.Close();
           }
           catch (Exception ex)
           {
               MessInfo = ex.Message;
           }
           return MessInfo;
       }
 
    }
}
