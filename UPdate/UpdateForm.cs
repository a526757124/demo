using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace UPdate
{
    public partial class UpdateForm : Form
    {
        public UpdateForm()
        {
            InitializeComponent();
            CheckProcess();
            DownLoadFile();
        }

        private void CheckProcess()
        {
            System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process p in ps)
            {
                //MessageBox.Show(p.ProcessName);
                if (p.ProcessName.ToLower() == "snzyzcardcontroller")
                {
                    p.Kill();
                    System.Threading.Thread.Sleep(1000);
                    break;
                }
            }
        }

        private void DownLoadFile()
        {
            string str = Application.StartupPath + @"\update.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(str);
            XmlElement root = doc.DocumentElement;
            //看看有几个文件需要更新
            XmlNode updateNode = root.SelectSingleNode("filelist");
            string path = updateNode.Attributes["sourcepath"].Value;
            int count = int.Parse(updateNode.Attributes["count"].Value);
            List<UpdateDate> listDate = new List<UpdateDate>();
            for (int i = 0; i < count; i++)
            {
                XmlNode itemNode = updateNode.ChildNodes[i];
                string urlPath = path + itemNode.Attributes["name"].Value;
                string fileName = Application.StartupPath + "\\" + itemNode.Attributes["name"].Value;

                using (WebClient wc = new WebClient())
                {
                    try
                    {
                        wc.DownloadFile(urlPath, fileName);
                        UpdateDate ud = new UpdateDate();
                        ud.FileName = itemNode.Attributes["name"].Value;
                        ud.FileSize = itemNode.Attributes["size"].Value;
                        listDate.Add(ud);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("下载失败，请清查网络");
                    }
                }
                this.dgvFile.DataSource = listDate;

            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + @"\SnzyzCardController.exe");
            Application.Exit();
        }
    }


    public class UpdateDate
    {
        public string FileName { get; set; }
        public string FileSize { get; set; }
    }
}
