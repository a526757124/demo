using CTS.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Text;
using System.Drawing.Printing;

namespace CTS.Common
{
    public class Printer
    {
        private Font printFont;
        private Font titleFont;
        private StringReader streamToPrint;
        private int leftMargin = 0;

        public void Print(List<Receipt> list)
        {
            var receipt = list.FirstOrDefault();
            if (receipt == null) return;

            StringBuilder sb = new StringBuilder();
            sb.Append("     快递领取信息\n");
            sb.Append("**********************************************\n");
            sb.AppendFormat("客户名称: {0}\n", receipt.CustomerName);
            sb.AppendFormat("客户电话: {0}\n", receipt.CustomerPhone);
            sb.Append("**********************************************\n");
            sb.Append("快递公司        数量\n");
            sb.Append("\n");
            foreach (var item in list.GroupBy(g => g.BelongCompany))
            {
                sb.AppendFormat("{0}         {1}件\n", item.Key.CourierName, item.Count());
            }
            sb.Append("**********************************************\n");
            sb.Append("\n客户签字: \n");
            sb.Append("\n**********************************************\n");
            sb.AppendFormat("\n      {0}\n", DateTime.Now.ToString("yyyy-MM-dd hh:mm"));
            sb.Append("     欢迎光临快递收发点 \n");
            Print(sb.ToString());
        }
        /// <summary>
        /// 设置PrintDocument 的相关属性
        /// </summary>
        /// <param name="str">要打印的字符串</param>
        public void Print(string str)
        {
            try
            {
                streamToPrint = new StringReader(str);
                printFont = new Font("宋体", 10);
                titleFont = new Font("宋体", 12, FontStyle.Bold);
                System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
                //pd.PrinterSettings.PrinterName = "SmartPrinter";
                pd.DocumentName = pd.PrinterSettings.MaximumCopies.ToString();
                pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd_PrintPage);
                pd.PrintController = new System.Drawing.Printing.StandardPrintController();
                PageSettings pageSetting = new PageSettings();
                pageSetting.Margins = new Margins(0, 1, 2, 2);
                pd.DefaultPageSettings = pageSetting;
                pd.Print();
            }
            catch (Exception ex)
            {
            }
        }


        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
        {

            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = this.leftMargin;
            float topMargin = 0;
            String line = null;
            linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);
            while (count < linesPerPage &&
            ((line = streamToPrint.ReadLine()) != null))
            {
                if (count == 0)
                {
                    yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(line, titleFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                }
                else if (count == 5)
                {
                    yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(line, new Font("宋体", 10, FontStyle.Bold), Brushes.Black, leftMargin, yPos, new StringFormat());
                }
                else
                {
                    yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                }
                count++;
            }
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;

        }
    }
}