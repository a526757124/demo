using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
namespace PosConsole
{
    /// <summary>
    /// 帮助类
    /// </summary>
    public static class Tool
    {
        private static ILog _iLog = LogManager.GetLogger("HttpWebLogger");
        private static string _url = ConfigurationManager.AppSettings["ICBCPosNotifyAddress"];
        /// <summary>
        /// 加密密钥
        /// </summary>
        private static readonly string keyString = "";
        #region 钱袋子处理

        /// <summary>
        /// 调用钱袋子接口数据处理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string ToWebPost(DataEntity<RequestData> data)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("POSNumber={0}&", data.Body.databuf.TERMDINO);
            sb.AppendFormat("CreateDate={0}&", data.Body.databuf.TRANDATE + data.Body.databuf.TRANTIME);
            sb.AppendFormat("BankCardNumber={0}&", data.Body.databuf.MDCARDNO);
            sb.AppendFormat("TradeAmount={0}&", DealPrice(data.Body.databuf.AMOUNT));
            sb.AppendFormat("OutOrderNo={0}", data.Body.databuf.REFNO);
            return sb.ToString();
        }
        /// <summary>
        /// 数据处理
        /// 金额为分转换成元
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        private static decimal DealPrice(string price)
        {
            try
            {
                var p = decimal.Parse(price);
                p = p / 100;
                return p;
            }
            catch (Exception ex)
            {
                _iLog.Error("金额转换出错", ex);
                return 0;
            }
        }
        /// <summary>
        /// 调用钱袋子接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TradeResultData Notify(DataEntity<RequestData> data)
        {
            string postData = ToWebPost(data);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            TradeResultData resultTrade = new TradeResultData();

            if (!string.IsNullOrWhiteSpace(postData))
            {
                using (var stream = request.GetRequestStream())
                using (StreamWriter streamWriter = new StreamWriter(stream))
                {
                    _iLog.InfoFormat("提交api数据：{0}", postData);
                    streamWriter.Write(postData);
                }
            }
            else
            {
                request.ContentLength = 0;
            }
            try
            {
                string result;
                using (var response = request.GetResponse())
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                    _iLog.InfoFormat("接收到api数据：{0}", result);
                }
                var v = JsonConvert.DeserializeObject<dynamic>(result);
                if (v.status == false)
                {
                    resultTrade = new TradeResultData()
                    {
                        Code = "1",
                        ErrorMsg = v.message
                    };
                }
                else
                {
                    resultTrade = new TradeResultData()
                    {
                        OrderNo = v.result.TradeNo,
                        Code = "0",
                        ErrorMsg = ""
                    };
                }

            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    using (var streamReader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        resultTrade = new TradeResultData()
                        {
                            Code = "1",
                            ErrorMsg = result
                        };
                    }
                }
                _iLog.InfoFormat("连接api异常：{0}", ex.Message);
            }
            return resultTrade;
        }
        #endregion


        #region byte处理

        /// <summary>
        /// 处理接收到的byte数据
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static StateObject DealData(StateObject o)
        {
            o.byteLen = SubByte(o.bufferAll.ToArray(), 0, 2);
            o.byteBCD = SubByte(o.bufferAll.ToArray(), 2, 5);

            o.byteBody = RemoveLeftByte(o.bufferAll.ToArray(), 7);
            byte[] outDes = new byte[o.byteBody.Count];
            byte b = 0;
            Tool.HKDes(ref o.byteBody.ToArray()[0], ref outDes[0], o.byteBody.Count, b);
            o.byteBody.Clear();
            o.byteBody.AddRange(outDes);
            return o;
        }
        /// <summary>
        /// 得到从左移出指定位数的byte
        /// </summary>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static List<byte> RemoveLeftByte(byte[] b, int length)
        {
            List<byte> list = new List<byte>();
            for (int i = length; i < b.Length; i++)
            {
                list.Add(b[i]);
            }
            return list;
        }
        /// <summary>
        /// 截取
        /// </summary>
        /// <param name="b"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static byte[] SubByte(byte[] b, int startIndex, int length)
        {
            List<byte> list = new List<byte>();
            for (int i = startIndex; i < (startIndex + length); i++)
            {
                list.Add(b[i]);
            }
            return list.ToArray();
        }
        #endregion

        #region 补全
        //public static string PadLeft(int totalWidth)
        //{
        //    return 
        //}
        //public static string PadLeft(int totalWidth, char paddingChar)
        //{

        //}
        #endregion

        #region DES加密

        /// <summary>
        /// 对字符串进行DES加密
        /// </summary>
        /// <param name="sInputString"></param>
        /// <returns></returns>
        public static string EncryptString(string sInputString)
        {
            return EncryptString(sInputString, keyString);
        }
        /// <summary>
        /// 对字符串进行DES加密
        /// </summary>
        /// <param >将要加密的字符串</param>
        /// <param >密钥值（需为8位字符串）</param>
        /// <returns></returns>
        public static string EncryptString(string sInputString, string sKey)
        {
            byte[] data = Encoding.ASCII.GetBytes(sInputString);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();  //创建其支持存储区为内存的流。
            CryptoStream cs = new CryptoStream(ms, DES.CreateEncryptor(), CryptoStreamMode.Write);//将数据流连接到加密转换流
            cs.Write(data, 0, data.Length);
            cs.FlushFinalBlock();  //用缓冲区的当前状态更新基础数据源或储存库，随后清除缓
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        /// <summary>
        ///  DES解密字符串
        /// </summary>
        /// <param name="sInputString"></param>
        /// <returns></returns>
        public static string DecryptString(string sInputString)
        {
            return DecryptString(sInputString, keyString);
        }
        // DES解密字符串
        public static string DecryptString(string sInputString, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //Put  the  input  string  into  the  byte  array  
            byte[] inputByteArray = new byte[sInputString.Length / 2];
            for (int x = 0; x < sInputString.Length; x += 2)
            {
                int i = Convert.ToInt32(sInputString.Substring(x, 2), 16);
                inputByteArray[x / 2] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            //Flush  the  data  through  the  crypto  stream  into  the  memory  stream  
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            //Get  the  decrypted  data  back  from  the  memory  stream  
            //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象  
            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.UTF8.GetString(ms.ToArray());

        }
        #endregion

        #region 调用C动态库加密
        [DllImport("DES.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int HKDes(ref byte source, ref byte dest, int len, byte flag);

        #endregion

        /// <summary>
        /// 得到当前时间 
        /// </summary>
        /// <returns></returns>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
        }
        /// <summary>
        /// 编码格式
        /// </summary>
        /// <returns></returns>
        public static Encoding GetEncoding()
        {
            //return Encoding.ASCII;
            return GetGBKEncoding();
        }
        public static Encoding GetGBKEncoding()
        {
            return Encoding.GetEncoding("GBK");
        }
    }
}
