using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace UnitTest.Services
{
    public class FileOperation
    {
        public void Write()
        {
            System.IO.FileStream fs = new FileStream(@"D:\Test.txt", FileMode.Append,FileAccess.Write);
            string str ="【"+ DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")+"】 Hello,this is my cc\n\t";
            byte[] array = new byte[str.Length];
            System.Text.Encoding e = System.Text.UTF8Encoding.UTF8;
            array = e.GetBytes(str);
            fs.Write(array, 0, array.Length);
            fs.Close();
        }
    }
}
