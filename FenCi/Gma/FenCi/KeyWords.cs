namespace Gma.FenCi
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections;
    using System.Configuration;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    public class KeyWords
    {
        private static Hashtable _countTable = new Hashtable();
        private static Hashtable _Tag = new Hashtable();

        public KeyWords()
        {
            string path = ConfigurationSettings.AppSettings["KeyWordsTxt"];
            if (!File.Exists(path))
            {
                throw new Exception("文件不存在");
            }
            InitFromFile(path);
        }

        public string getKeyWords(string s, int maxLink)
        {
            int num = 0;
            string str2 = "";
            IDictionaryEnumerator enumerator = _countTable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                DictionaryEntry current = (DictionaryEntry) enumerator.Current;
                if (num > (maxLink - 1))
                {
                    return s;
                }
                Match match = new Regex("(?<iHead>" + current.Key.ToString() + ")", RegexOptions.IgnoreCase).Match(s);
                while (match.Success && (str2.IndexOf(current.Value.ToString()) == -1))
                {
                    str2 = str2 + current.Value.ToString() + ",";
                    string[] strArray = new string[7];
                    strArray[0] = s.Substring(0, s.ToUpper().IndexOf(current.Key.ToString().ToUpper()));
                    strArray[1] = "<a href=\"";
                    strArray[2] = current.Value.ToString();
                    strArray[3] = "\">";
                    strArray[4] = current.Key.ToString();
                    strArray[5] = "</a>";
                    int index = s.ToUpper().IndexOf(current.Key.ToString().ToUpper());
                    strArray[6] = s.Substring(index + current.Key.ToString().Length);
                    s = string.Concat(strArray);
                    num++;
                    break;
                }
            }
            return s;
        }

        private static void InitFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                StreamReader reader = new StreamReader(fileName, Encoding.GetEncoding("GB2312"));
                while (reader.Peek() != -1)
                {
                    string[] strArray = reader.ReadLine().Split(new char[] { ',' });
                    if (Information.UBound(strArray, 1) > 1)
                    {
                        int index = 0;
                        int num2 = Information.UBound(strArray, 1) - 1;
                        for (index = 1; index <= num2; index++)
                        {
                            if (!_countTable.Contains(strArray[index]))
                            {
                                _countTable.Add(strArray[index], strArray[Information.UBound(strArray, 1)]);
                            }
                        }
                    }
                }
                reader.Close();
            }
        }
    }
}

