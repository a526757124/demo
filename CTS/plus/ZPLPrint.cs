using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Management;

namespace TRCService
{
    public class ZPLPrint
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct OVERLAPPED
        {
            int Internal;
            int InternalHigh;
            int Offset;
            int OffSetHigh;
            int hEvent;
        }
        /// <summary>
        /// 打开端口
        /// </summary>
        /// <param name="lpFileName"></param>
        /// <param name="dwDesiredAccess"></param>
        /// <param name="dwShareMode"></param>
        /// <param name="lpSecurityAttributes"></param>
        /// <param name="dwCreationDisposition"></param>
        /// <param name="dwFlagsAndAttributes"></param>
        /// <param name="hTemplateFile"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern int CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            int dwShareMode,
            int lpSecurityAttributes,
            int dwCreationDisposition,
            int dwFlagsAndAttributes,
            int hTemplateFile);

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="hFile"></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nNumberOfBytesToWriter"></param>
        /// <param name="lpNumberOfBytesWriten"></param>
        /// <param name="lpOverLapped"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern bool WriteFile(
            int hFile,
            byte[] lpBuffer,
            int nNumberOfBytesToWriter,
            out int lpNumberOfBytesWriten,
            out OVERLAPPED lpOverLapped);

        /// <summary>
        /// 关闭端口
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(int hObject);

        /// <summary>
        /// 中文处理
        /// </summary>
        /// <param name="ChineseText">待转变中文内容</param>
        /// <param name="FontName">字体名称</param>
        /// <param name="Orient">旋转角度0,90,180,270</param>
        /// <param name="Height">字体高度</param>
        /// <param name="Width">字体宽度，通常是0</param>
        /// <param name="IsBold">1 变粗,0 正常</param>
        /// <param name="IsItalic">1 斜体,0 正常</param>
        /// <param name="ReturnPicData">返回的图片字符</param>
        /// <returns></returns>
        [DllImport("fnthex32.dll")]
        public static extern int GETFONTHEX(
            string ChineseText,
            string FontName,
            int Orient,
            int Height,
            int Width,
            int IsBold,
            int IsItalic,
            StringBuilder ReturnPicData);

        /// <summary>
        /// 中文处理
        /// </summary>
        /// <param name="ChineseText">待转变中文内容</param>
        /// <param name="FontName">字体名称</param>
        /// <param name="FileName">返回的图片字符重命</param>
        /// <param name="Orient">旋转角度0,90,180,270</param>
        /// <param name="Height">字体高度</param>
        /// <param name="Width">字体宽度，通常是0</param>
        /// <param name="IsBold">1 变粗,0 正常</param>
        /// <param name="IsItalic">1 斜体,0 正常</param>
        /// <param name="ReturnPicData">返回的图片字符</param>
        /// <returns></returns>
        [DllImport("fnthex32.dll")]
        public static extern int GETFONTHEX(
                              string ChineseText,
                              string FontName,
                              string FileName,
                              int Orient,
                              int Height,
                              int Width,
                              int IsBold,
                              int IsItalic,
                              StringBuilder ReturnPicData);

        private int iHandle;
        //打开LPT 端口
        public bool Open(string PNPDeviceID)
        {
            iHandle = CreateFile("\\\\.\\" + PNPDeviceID.Replace('\\', '#') + "#{A5DCBF10-6530-11D2-901F-00C04FB951ED}"
                    , (uint)FileAccess.ReadWrite, 0, 0, (int)FileMode.Open, 0, 0);
            if (iHandle != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //打印函数，参数为打印机的命令或者其他文本！
        public bool Write(string MyString)
        {
            if (iHandle != 1)
            {
                int i;
                OVERLAPPED x;
                byte[] mybyte = System.Text.Encoding.Default.GetBytes(MyString);
                return WriteFile(iHandle, mybyte, mybyte.Length, out i, out x);
            }
            else
            {
                throw new Exception("端口未打开~！");
            }
        }
        //关闭打印端口
        public bool Close()
        {
            return CloseHandle(iHandle);
        }

        /// <summary>
        /// 打印凭条设置
        /// </summary>
        /// <param name="width">凭条宽度</param>
        /// <param name="height">凭条高度</param>
        /// <returns>返回ZPL命令</returns>
        public string ZPL_SetLabel(int width, int height)
        {
            //ZPL条码设置命令：^PW640^LL480
            string sReturn = "^PW{0}^LL{1}";
            return string.Format(sReturn, width, height);
        }

        /// <summary>
        ///  打印矩形
        /// </summary>
        /// <param name="px">起点X坐标</param>
        /// <param name="py">起点Y坐标</param>
        /// <param name="thickness">边框宽度</param>
        /// <param name="width">矩形宽度，0表示打印一条竖线</param>
        /// <param name="height">矩形高度，0表示打印一条横线</param>
        /// <returns>返回ZPL命令</returns>
        public string ZPL_DrawRectangle(int px, int py, int thickness, int width, int height)
        {
            //ZPL矩形命令：^FO50,50^GB300,200,2^FS
            string sReturn = "^FO{0},{1}^GB{3},{4},{2}^FS";
            return string.Format(sReturn, px, py, thickness, width, height);
        }

        /// <summary>
        /// 打印英文
        /// </summary>
        /// <param name="EnText">待打印文本</param>
        /// <param name="px">起点X坐标</param>
        /// <param name="py">起点Y坐标</param>
        /// <param name="Orient">旋转角度N = normal，R = rotated 90 degrees (clockwise)，I = inverted 180 degrees，B = read from bottom up, 270 degrees</param>
        /// <param name="Height">字体高度</param>
        /// <param name="Width">字体宽度</param>
        /// <returns>返回ZPL命令</returns>
        public string ZPL_DrawENText(string EnText, int px, int py,string Orient,int Height, int Width)
        {
            //ZPL打印英文命令：^FO50,50^A0N,32,25^FDZEBRA^FS
            string sReturn = "^FO{1},{2}^A0{3},{4},{5}^FD{0}^FS";
            return string.Format(sReturn, EnText, px, py, Orient, Height, Width);
        }
        
        /// <summary>
        /// 打印中文
        /// </summary>
        /// <param name="px">起点X坐标</param>
        /// <param name="py">起点Y坐标</param>
        /// <param name="FileName">文本字符流</param>
        /// <returns>返回ZPL命令</returns>
        public string ZPL_DrawCHText(int px, int py,string FileName)
        {
            //ZPL打印英文命令：^FO60,90^XGtemp1,1,1^FS
            string sReturn = "^FO{0},{1}^XG{2},1,1^FS";
            return string.Format(sReturn, px, py, FileName);
        }

        /// <summary>
        /// 打印条形码（128码）
        /// </summary>
        /// <param name="px">起点X坐标</param>
        /// <param name="py">起点Y坐标</param>
        /// <param name="width">基本单元宽度</param>
        /// <param name="ratio">宽窄比</param>
        /// <param name="barheight">条码高度</param>
        /// <param name="barcode">条码内容</param>
        /// <returns>返回ZPL命令</returns>
        public string ZPL_DrawBarcode(int px, int py,int width,int ratio,int barheight,string barcode)
        {
            //ZPL打印英文命令：^FO50,260^BY1,2^BCN,100,Y,N^FDSMJH2000544610^FS
            string sReturn = "^FO{0},{1}^BY{2},{3}^BCN,{4},N,N^FD{5}^FS";
            return string.Format(sReturn, px, py, width, ratio, barheight, barcode);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="strCommand"></param>
        public void ZPL_Print(string strCommand)
        {
            //USB打印支持属于Win32_USBHub类
            SelectQuery selectQuery = new SelectQuery("Win32_USBHub");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(selectQuery);
            foreach (ManagementObject disk in searcher.Get())
            {
                string PNPDeviceID = disk["PNPDeviceID"] as String;

                Open(PNPDeviceID);
                Write(strCommand);
                Close();
            }
        }
    }
}
