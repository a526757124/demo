using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Web;

namespace CTS.Common
{
    public class PrintInvoice
    {

        #region 01.获得打印机1获得吧台打印机2获得后厨打印机

        /// <summary>        

        /// 获得打印机1获得吧台打印机2获得后厨打印机     

        /// </summary>      

        public TcpClient GetPrint()
        {
            var client = new System.Net.Sockets.TcpClient();


            var port = 0;

            var ipPrint = "192.168.2.108";

            client.Connect(ipPrint, port);

            return client;

        }

        #endregion

        #region 02.初始化一个网路访问数据流

        /// <summary>    

        /// 初始化一个网路访问数据流    

        /// </summary>   

        /// <returns></returns>     

        public NetworkStream BuildStream()
        {

            System.Net.Sockets.NetworkStream stream = null;

            return stream;

        }

        #endregion

        #region 03.套接字和打印机通讯放回通讯流

        /// <summary>     

        /// 套接字和打印机通讯放回通讯流      

        /// </summary>   

        /// <returns></returns>   

        public NetworkStream GetStream(TcpClient client, NetworkStream stream)
        {

            byte[] chushihua = new byte[] { 27, 64 };//初始化打印机        

            byte[] ziti = new byte[] { 27, 77, 0 };//选择字体n =0,1,48,49     

            byte[] zitidaxiao = new byte[] { 29, 33, 0 };//选择字体大小      

            byte[] duiqifangshi = new byte[] { 27, 97, 1 };//选择对齐方式0,48左对齐1,49中间对齐2,50右对齐     

            stream = client.GetStream();             //是否支持写入         

            if (!stream.CanWrite) { stream = null; }

            stream.Write(chushihua, 0, chushihua.Length);//初始化     

            stream.Write(ziti, 0, ziti.Length);//设置字体      

            stream.Write(zitidaxiao, 0, zitidaxiao.Length);//设置字体大小--关键     

            stream.Write(duiqifangshi, 0, duiqifangshi.Length);//居中      

            return stream;

        }

        #endregion

        #region 04.把要打印的文字写入打印流

        /// <summary>      

        /// 把要打印的文字写入打印流     

        /// </summary>     

        /// <param name="stream"></param>   

        /// <param name="output"></param>     

        public void PrintText(NetworkStream stream, string output)
        {

            Byte[] data = System.Text.Encoding.Default.GetBytes(output);

            stream.Write(data, 0, data.Length);//输出文字  

        }

        #endregion

        #region 05.设置对齐方式0,48左对齐1,49中间对齐2,50右对齐

        /// <summary>    

        /// 设置对齐方式0,48左对齐1,49中间对齐2,50右对齐     

        /// </summary>      

        /// <param name="stream"></param>   

        /// <param name="n"></param>    

        public void SetDuiQi(NetworkStream stream, byte n)
        {

            byte[] duiqifangshi = new byte[] { 27, 97, 1 };//选择对齐方式0,48左对齐1,49中间对齐2,50右对齐    

            stream.Write(duiqifangshi, 0, duiqifangshi.Length);

        }

        #endregion

        #region 06.设置字体n =0,1,48,49

        /// <summary>     

        /// 设置字体n =0,1,48,49      

        /// </summary>     

        /// <param name="stream"></param>     

        /// <param name="n"></param>    

        public void SetFont(NetworkStream stream, byte n)
        {

            byte[] ziti = new byte[] { 27, 77, 0 };//选择字体n =0,1,48,49    

            stream.Write(ziti, 0, ziti.Length);

        }

        #endregion

        #region 07.设置加粗1加粗0还原

        /// <summary>        

        /// 设置加粗1加粗0还原      

        /// </summary>    

        /// <param name="stream"></param>     

        /// <param name="n"></param>     

        public void SetBold(NetworkStream stream, byte n)
        {

            byte[] jiacu = new byte[] { 27, 69, n };//选择加粗模式    

            stream.Write(jiacu, 0, jiacu.Length);

        }

        #endregion

        #region 08.设置字体大小0最小1,2,3

        /// <summary>

        /// 设置字体大小0最小1,2,3     

        /// </summary>     

        /// <param name="stream"></param>      

        /// <param name="n"></param>    

        public void SetFontSize(NetworkStream stream, byte n)
        {

            byte[] zitidaxiao = new byte[] { 29, 33, n };//选择字体大小   

            stream.Write(zitidaxiao, 0, zitidaxiao.Length);

        }

        #endregion

        #region 09.切纸

        /// <summary>    

        /// 切纸    

        /// </summary>   

        /// <param name="stream"></param>  

        /// <param name="n"></param>   

        public void QieZhi(NetworkStream stream)
        {

            byte[] qiezhi = new byte[] { 29, 86, 1, 49 };//切纸   

            stream.Write(qiezhi, 0, qiezhi.Length);

        }

        #endregion

        #region 10.释放资源

        /// <summary>    

        /// 释放资源    

        /// </summary>      

        /// <param name="client"></param>    

        /// <param name="stream"></param>    

        public void DiposeStreamClient(TcpClient client, NetworkStream stream)
        {

            if (stream != null)
            {

                stream.Close();

                stream.Dispose();

            }

            if (client != null) client.Close();

        }

        #endregion

        #region 11.样例展示

        public void Printeg()
        {

            var p = new PrintInvoice();

            var batai = p.GetPrint();//获得吧台打印机    

            var liunull = p.BuildStream();//初始化一个网络访问数据流  

            try
            {

                var liu = p.GetStream(batai, liunull);//获得通讯打印流  

                p.SetFontSize(liu, 2);//字体变大         

                p.SetBold(liu, 1);//加粗       

                p.PrintText(liu, "\n结账单\n\n");//打印文字   

                p.SetBold(liu, 0);//取消加粗             

                p.SetFontSize(liu, 0);//字体还原

                p.PrintText(liu, "北京西城                  ");//打印文字        

                p.PrintText(liu, "\n----------------------------------------------\n");//打印文字     

                p.PrintText(liu, "账单号：1405220015       消费方式：普通消费\n");//打印文字        

                p.PrintText(liu, "时间：14-06-06 08:58   人数：4     桌号：11");//打印文字      

                p.PrintText(liu, "\n----------------------------------------------\n");//打印文字   

                p.SetFontSize(liu, 1);//字体变大        

                p.SetBold(liu, 1);//加粗        

                p.PrintText(liu, "品项名称      数量     单价      金额");//打印文字         

                p.SetBold(liu, 0);//取消加粗                 p.SetFontSize(liu, 0);//字体还原       

                p.PrintText(liu, "\n----------------------------------------------\n");//打印文字

                p.PrintText(liu, "木须肉         2        16        32\n");//打印文字       

                p.PrintText(liu, "宫保鸡丁       1        20        20\n");//打印文字

                p.PrintText(liu, "----------------------------------------------\n");//打印文字   

                p.SetFontSize(liu, 1);//字体变大                 p.SetBold(liu, 1);//加粗      

                p.PrintText(liu, "                              小计：52.00");//打印文字     

                p.SetBold(liu, 0);//取消加粗       

                p.SetFontSize(liu, 0);//字体还原        

                p.PrintText(liu, "\n----------------------------------------------\n");//打印文字

                p.PrintText(liu, "\n折扣：4         现金：100         找零：44\n\n");//打印文字

                p.SetFontSize(liu, 1);//字体变大                 p.SetBold(liu, 1);//加粗  

                p.PrintText(liu, "                              实际结算：￥48");//打印文字   

                p.SetBold(liu, 0);//取消加粗                 p.SetFontSize(liu, 0);//字体还原    

                p.PrintText(liu, "\n----------------------------------------------\n");//打印文字

                p.PrintText(liu, "收银员：张三                                 \n");//打印文字      

                p.PrintText(liu, "服务员：李四                                 \n");//打印文字     

                p.PrintText(liu, "\n客人签字：________________                   \n");//打印文字

                p.PrintText(liu, "\n==============================================\n");//打印文字      

                p.PrintText(liu, "xx餐厅祝您用餐愉快！");//打印文字                 p.PrintText(liu, "\n餐厅电话：4000000 ");//打印文字     

                p.PrintText(liu, "\n==============================================\n");//打印文字  

                p.PrintText(liu, "\n\n\n\n\n\n\n\n");//打印文字    

                p.QieZhi(liu);//切纸        

            }
            catch
            {

                //打印机缺纸或者纸匣打开时,不会出现异常,不用特殊代码判断,数据不会丢失.     

            }

            finally
            {

                p.DiposeStreamClient(batai, liunull);//释放资源       

            }

        }

        #endregion

        
    }
    
}