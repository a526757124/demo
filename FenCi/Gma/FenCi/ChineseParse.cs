namespace Gma.FenCi
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections;
    using System.Configuration;
    using System.IO;
    using System.Text.RegularExpressions;

    public class ChineseParse
    {
        private static Gma.FenCi.ChineseWordsHashCountSet _countTable = new Gma.FenCi.ChineseWordsHashCountSet();
        private static bool _IsReplaceOneWord = false;
        private int _Time;

        public ChineseParse()
        {
            string path = ConfigurationSettings.AppSettings["ChineseWords"];
            if (!File.Exists(path))
            {
                throw new Exception("文件不存在");
            }
            InitFromFile(path);
        }

        public string[] getParse(string s)
        {
            string[] strArray2 = new string[0];
            DateTime time2 = DateAndTime.Now;
            strArray2 = ParseChinese(s);
            TimeSpan span = DateAndTime.Now.Subtract(time2);
            this._Time = (int) Math.Round(span.TotalMilliseconds);
            return strArray2;
        }

        private static void InitFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                StreamReader reader = File.OpenText(fileName);
                while (reader.Peek() != -1)
                {
                    Gma.FenCi.ChineseWordUnit unit = InitUnit(reader.ReadLine());
                    _countTable.InsertWord(unit.Word);
                }
                reader.Close();
            }
        }

        private static Gma.FenCi.ChineseWordUnit InitUnit(string s)
        {
            return new Gma.FenCi.ChineseWordUnit(s, 1);
        }

        private static string[] ParseChinese(string s)
        {
            int length = s.Length;
            string input = string.Empty;
            ArrayList list = new ArrayList();
            int startIndex = 0;
            while (startIndex < s.Length)
            {
                input = s.Substring(startIndex, 1);
                if (Regex.IsMatch(input, "[0-9A-Za-z]{1,}"))
                {
                    Match match = Regex.Match(s.Substring(startIndex), "[0-9A-Za-z]{1,}");
                    while (match.Success)
                    {
                        input = match.Value;
                        break;
                    }
                    startIndex += match.Value.Length;
                }
                else
                {
                    if (_countTable.GetCount(input) > 1)
                    {
                        int num3 = 2;
                        while (((startIndex + num3) < (s.Length + 1)) && (_countTable.GetCount(s.Substring(startIndex, num3)) > 0))
                        {
                            num3++;
                        }
                        input = s.Substring(startIndex, num3 - 1);
                        startIndex = (startIndex + num3) - 2;
                    }
                    startIndex++;
                }
                if (!IsReplaceOneWord)
                {
                    list.Add(input);
                }
                else if (input.Length > 1)
                {
                    list.Add(input);
                }
            }
            string[] array = new string[(list.Count - 1) + 1];
            list.CopyTo(array);
            return array;
        }

        public static bool IsReplaceOneWord
        {
            get
            {
                return _IsReplaceOneWord;
            }
            set
            {
                _IsReplaceOneWord = value;
            }
        }

        public int Time
        {
            get
            {
                return this._Time;
            }
        }
    }
}

