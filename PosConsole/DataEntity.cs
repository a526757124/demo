using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PosConsole
{
    #region 数据实体

    /// <summary>
    /// 报文数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataEntity<T> where T : class
    {
        /// <summary>
        /// 包头
        /// </summary>
        public HeaderData Header { get; set; }
        /// <summary>
        /// 包体
        /// </summary>
        public RequestBody<T> Body { get; set; }
        /// <summary>
        /// 注 只返回body内容
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Body.ToString();
        }
    }
    /// <summary>
    /// 包头
    /// </summary>
    public class HeaderData
    {
        /// <summary>
        /// 未交换的
        /// </summary>
        public byte[] TPDU { get; set; }
        /// <summary>
        /// 2、3位与4、5位交换
        /// </summary>
        /// <returns></returns>
        public byte[] ToByte()
        {
            List<byte> list = new List<byte>();
            if (TPDU.Length != 5)
                new Exception("包头TPDU长度(5位)不完整");
            list.Add(TPDU[0]);
            list.Add(TPDU[3]);
            list.Add(TPDU[4]);
            list.Add(TPDU[1]);
            list.Add(TPDU[2]);
            ////交换 2 4位
            //t= TPDU[3];
            //TPDU[3] = TPDU[1];
            //TPDU[1] = t;

            ////交换 3 5 位
            //t = TPDU[4];
            //TPDU[4] = TPDU[2];
            //TPDU[4] = t;

            return list.ToArray();
        }
    }
    /// <summary>
    /// 请求包体
    /// </summary>
    public class RequestBody<T> where T : class
    {
        [DataLength(1)]
        public string trxcode { get; set; }
        [DataLength(4)]
        public string reqlength { get; set; }
        public T databuf { get; set; }
        /// <summary>
        /// 返回包体与报文的字符
        /// 注：若是应答报文，则返回的TPDU为五个0
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.reqlength);
            sb.Append(this.trxcode);
            sb.Append(databuf.ToString());
            return sb.ToString();
        }
        /// <summary>
        /// 注 此方法只把本类属性转换为byte
        /// </summary>
        /// <returns></returns>
        public byte[] ToByte()
        {
            List<byte> list = new List<byte>();
            list.AddRange(Tool.GetEncoding().GetBytes(this.reqlength));
            list.AddRange(Tool.GetEncoding().GetBytes(this.trxcode));
            return list.ToArray();
        }
    }
    /// <summary>
    /// 请求报文 68
    /// </summary>
    public class RequestData
    {
        /// <summary>
        /// 卡号 19
        /// </summary>
        [DataLength(19)]
        public string MDCARDNO { get; set; }
        /// <summary>
        /// 金额 12
        /// </summary>
        [DataLength(12)]
        public string AMOUNT { get; set; }
        /// <summary>
        /// 终端号 15
        /// </summary>
        [DataLength(15)]
        public string TERMDINO { get; set; }
        /// <summary>
        /// 检索参考号 8
        /// </summary>
        [DataLength(23)]
        public string REFNO { get; set; }
        /// <summary>
        /// 交易日期 8
        /// </summary>
        [DataLength(8)]
        public string TRANDATE { get; set; }
        /// <summary>
        /// 交易时间 6
        /// </summary>
        [DataLength(6)]
        public string TRANTIME { get; set; }
        /// <summary>
        /// 卡名称
        /// </summary>
        [DataLength(40)]
        public string CARDNAME { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
    /// <summary>
    /// 应答报文 141
    /// </summary>
    public class ResponseData
    {
        /// <summary>
        /// TPDU 5 
        /// 存 已交换后的数据
        /// </summary>
        [DataLength(5)]
        public byte[] TPDU { get; set; }
        /// <summary>
        /// 数据长度 4
        /// </summary>
        [DataLength(4)]
        public string RSPLENGTH { get; set; }
        /// <summary>
        /// 交易码 1
        /// </summary>
        [DataLength(1)]
        public string TRXCODE { get; set; }
        /// <summary>
        /// 响应码     1
        /// </summary>
        [DataLength(1)]
        public string REQCODE { get; set; }
        /// <summary>
        /// 响应信息    43
        /// </summary>
        [DataLength(43)]
        public string ANSMSG { get; set; }
        /// <summary>
        /// 交易序列号   20
        /// </summary>
        [DataLength(20)]
        public string JYXLH { get; set; }
        /// <summary>
        /// 卡号  19
        /// </summary>
        [DataLength(19)]
        public string MDCARDNO { get; set; }
        /// <summary>
        /// 金额  12
        /// </summary>
        [DataLength(12)]
        public string AMOUNT { get; set; }
        /// <summary>
        /// 终端号     15
        /// </summary>
        [DataLength(15)]
        public string TERMDINO { get; set; }
        /// <summary>
        /// 检索参考号   8
        /// </summary>
        [DataLength(23)]
        public string REFNO { get; set; }
        /// <summary>
        /// 交易日期    8
        /// </summary>
        [DataLength(8)]
        public string TRANDATE { get; set; }
        /// <summary>
        /// 交易时间    6
        /// </summary>
        [DataLength(6)]
        public string TRANTIME { get; set; }
        /// <summary>
        /// 卡名称
        /// </summary>
        [DataLength(40)]
        public string CARDNAME { get; set; }
        /// <summary>
        /// 返回除TPDU外的字符
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("0".PadLeft(5, '0'));
            sb.Append(this.RSPLENGTH.PadLeft(DataEntityAttributeHelper.GetDataLength<ResponseData>(p => p.RSPLENGTH), '0'));
            sb.Append(this.TRXCODE);
            sb.Append(this.REQCODE);
            sb.Append(this.ANSMSG.PadLeft(DataEntityAttributeHelper.GetDataLength<ResponseData>(p => p.ANSMSG)));
            sb.Append(this.JYXLH.PadLeft(DataEntityAttributeHelper.GetDataLength<ResponseData>(p => p.JYXLH), '0'));
            sb.Append(this.MDCARDNO.PadLeft(DataEntityAttributeHelper.GetDataLength<ResponseData>(p => p.MDCARDNO), '0'));
            sb.Append(this.AMOUNT.PadLeft(DataEntityAttributeHelper.GetDataLength<ResponseData>(p => p.AMOUNT), '0'));
            sb.Append(this.TERMDINO.PadLeft(DataEntityAttributeHelper.GetDataLength<ResponseData>(p => p.TERMDINO), '0'));
            sb.Append(this.REFNO.PadLeft(DataEntityAttributeHelper.GetDataLength<ResponseData>(p => p.REFNO), '0'));
            sb.Append(this.TRANDATE);
            sb.Append(this.TRANTIME);
            return sb.ToString();
        }
        /// <summary>
        /// 返回所有的字段转换后的Byte
        /// </summary>
        /// <returns></returns>
        public Byte[] ToByte()
        {

            List<byte> list = new List<byte>();
            list.AddRange(new byte[] { 0x00, 0xC5 });
            list.AddRange(this.TPDU);

            //加密前数据
            List<byte> listDes = new List<byte>();

            listDes.AddRange(new byte[] { 0x30, 0x31, 0x38, 0x38 });//188
            listDes.AddRange(DataToBytes(this.TRXCODE));
            listDes.AddRange(DataToBytes(this.REQCODE));

            byte[] ansmsg = new byte[DataEntityAttributeHelper.GetDataLength<ResponseData>(p => p.ANSMSG)];
            var temp = DataToBytes(this.ANSMSG, 1);
            for (int i = 0; i < temp.Length; i++)
            {
                ansmsg[i] = temp[i];
            }
            listDes.AddRange(ansmsg);

            listDes.AddRange(DataToBytes(this.JYXLH.PadLeft(DataEntityAttributeHelper.GetDataLength<ResponseData>(p => p.JYXLH), '0')));
            listDes.AddRange(DataToBytes(this.MDCARDNO.PadLeft(DataEntityAttributeHelper.GetDataLength<ResponseData>(p => p.MDCARDNO), '0')));
            listDes.AddRange(DataToBytes(this.AMOUNT.PadLeft(DataEntityAttributeHelper.GetDataLength<ResponseData>(p => p.AMOUNT), '0')));
            listDes.AddRange(DataToBytes(this.TERMDINO.PadLeft(DataEntityAttributeHelper.GetDataLength<ResponseData>(p => p.TERMDINO), '0')));
            listDes.AddRange(DataToBytes(this.REFNO.PadLeft(DataEntityAttributeHelper.GetDataLength<ResponseData>(p => p.REFNO), '0')));
            listDes.AddRange(DataToBytes(this.TRANDATE));
            listDes.AddRange(DataToBytes(this.TRANTIME));

            byte[] cardname = new byte[DataEntityAttributeHelper.GetDataLength<ResponseData>(p => p.CARDNAME)];
            cardname = DataToBytes(this.CARDNAME, 1);
            listDes.AddRange(cardname);
            byte[] outDes=new byte[listDes.Count];
            byte b = 1;
            Tool.HKDes(ref listDes.ToArray()[0], ref outDes[0], listDes.Count, b);
            list.AddRange(outDes);
            return list.ToArray();
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private byte[] DataToBytes(string data,int type=0)
        {
            switch (type)
            {
                case 1:
                    return Tool.GetGBKEncoding().GetBytes(data);
                default:
                    return Tool.GetEncoding().GetBytes(data);
            }
            
        }
        public byte[] ConvertHexToBytes(object value)
        {
            var temp = String.Format("{0:X2}", value);
            int len = temp.Length / 2;
            byte[] ret = new byte[len];
            for (int i = 0; i < len; i++)
                ret[i] = (byte)(System.Convert.ToInt32(temp.Substring(i * 2, 2), 16));
            return ret;
        }
    }
    /// <summary>
    /// 交易结果数据
    /// </summary>
    public class TradeResultData
    {
        private string _orderNo = string.Empty;
        /// <summary>
        /// 交易号
        /// </summary>
        public string OrderNo
        {
            get
            {
                return this._orderNo;
            }
            set
            {
                if (value.Length <= 20) { this._orderNo = value; }
                else
                {
                    throw new Exception("交易号不有大于20位");
                }
            }
        }
        /// <summary>
        /// 交易状态编码 0 表示成功 非0表示失败，并传送错误信息
        /// </summary>
        public string Code { get; set; }
        private string _errorMsg = string.Empty;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg
        {
            get
            {
                if (this.Code.Equals("0"))
                    return string.Empty;
                else
                {
                    if (string.IsNullOrEmpty(this._errorMsg)) throw new Exception("交易失败后必须返回错误信息");
                    return this._errorMsg;
                }
            }
            set
            {
                this._errorMsg = value;
            }
        }
        public override string ToString()
        {
            return string.Format("OrderNo:{0},Code:{1},ErrorMsg:{2}", this.OrderNo, this.Code, this.ErrorMsg);
        }
    }

    #endregion

    #region 数据实体处理

    public static class DataEntityHelper
    {
        public static DataEntity<RequestData> ToRequestData(byte[] tpdu, string strData)
        {
            //if (strData.Trim().Length != 77)
            //    throw new Exception("接收的数据不完整！");
            DataEntity<RequestData> data = new DataEntity<RequestData>();
            data.Header = new HeaderData()
            {
                TPDU = tpdu
            };
            data.Body = new RequestBody<RequestData>()
            {
                trxcode = GetStrDataSubString(ref strData, DataEntityAttributeHelper.GetDataLength<RequestBody<RequestData>>(p => p.trxcode)),
                reqlength = GetStrDataSubString(ref strData, DataEntityAttributeHelper.GetDataLength<RequestBody<RequestData>>(p => p.reqlength)),
                databuf = new RequestData()
                {
                    MDCARDNO = GetStrDataSubString(ref strData, DataEntityAttributeHelper.GetDataLength<RequestData>(p => p.MDCARDNO)),
                    AMOUNT = GetStrDataSubString(ref strData, DataEntityAttributeHelper.GetDataLength<RequestData>(p => p.AMOUNT)),
                    TERMDINO = GetStrDataSubString(ref strData, DataEntityAttributeHelper.GetDataLength<RequestData>(p => p.TERMDINO)),
                    REFNO = GetStrDataSubString(ref strData, DataEntityAttributeHelper.GetDataLength<RequestData>(p => p.REFNO)),
                    TRANDATE = GetStrDataSubString(ref strData, DataEntityAttributeHelper.GetDataLength<RequestData>(p => p.TRANDATE)),
                    TRANTIME = GetStrDataSubString(ref strData, DataEntityAttributeHelper.GetDataLength<RequestData>(p => p.TRANTIME)),
                    CARDNAME = GetStrDataSubString(ref strData, DataEntityAttributeHelper.GetDataLength<RequestData>(p => p.CARDNAME))
                }
            };
            return data;
        }
        public static DataEntity<ResponseData> ToResponseData(DataEntity<RequestData> request, TradeResultData tradeResultData)
        {
            DataEntity<ResponseData> data = new DataEntity<ResponseData>();
            data.Header = new HeaderData()
            {
                TPDU = request.Header.TPDU
            };
            data.Body = new RequestBody<ResponseData>()
            {
                reqlength = "197",
                trxcode = "1",
                databuf = new ResponseData()
                {
                    TPDU = request.Header.ToByte(),
                    RSPLENGTH = "188",
                    TRXCODE = "1",
                    REQCODE = tradeResultData.Code,
                    ANSMSG = tradeResultData.ErrorMsg,
                    JYXLH = tradeResultData.OrderNo,
                    MDCARDNO = request.Body.databuf.MDCARDNO,
                    AMOUNT = request.Body.databuf.AMOUNT,
                    TERMDINO = request.Body.databuf.TERMDINO,
                    REFNO = request.Body.databuf.REFNO,
                    TRANDATE = request.Body.databuf.TRANDATE,
                    TRANTIME = request.Body.databuf.TRANTIME,
                    CARDNAME=request.Body.databuf.CARDNAME
                }
            };
            return data;
        }

        private static string GetStrDataSubString(ref string str, int len)
        {
            if (str.Length > len)
            {
                var result = str.Substring(0, len);
                str = str.Remove(0, len);
                return result;
            }
            return str;
        }
    }
    #endregion

    #region 数据实体特性

    /// <summary>
    /// 数据长度特性
    /// </summary>
    public class DataLengthAttribute : Attribute
    {
        public int Length { get; set; }
        public DataLengthAttribute()
        {

        }
        public DataLengthAttribute(int length)
        {
            Length = length;
        }
    }

    public static class DataEntityAttributeHelper
    {
        public static int GetDataLength<T>(Expression<Func<T, object>> predicate) where T : class
        {
            int result = 0;
            var propName = predicate.Body.ToString().Replace("p.", "");
            var t = typeof(T);
            foreach (var prop in t.GetProperties())
            {
                if (prop.Name.Equals(propName))
                {
                    var attr = prop.GetCustomAttributes(typeof(DataLengthAttribute), false);
                    if (attr.Any())
                    {
                        var v = attr[0] as DataLengthAttribute;
                        result = v.Length;
                        break;
                    }
                }
            }
            return result;
        }
    }
    #endregion

}
