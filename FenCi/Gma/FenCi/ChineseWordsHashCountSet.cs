namespace Gma.FenCi
{
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.Collections;

    public class ChineseWordsHashCountSet
    {
        private Hashtable _rootTable = new Hashtable();

        public int GetCount(string s)
        {
            if (!this._rootTable.ContainsKey(s.Length))
            {
                return -1;
            }
            Hashtable hashtable = (Hashtable) this._rootTable[s.Length];
            if (!hashtable.ContainsKey(s))
            {
                return -1;
            }
            return IntegerType.FromObject(hashtable[s]);
        }

        private void InsertSubString(string s)
        {
            if (!this._rootTable.ContainsKey(s.Length) && (s.Length > 0))
            {
                Hashtable hashtable2 = new Hashtable();
                this._rootTable.Add(s.Length, hashtable2);
            }
            Hashtable hashtable = (Hashtable) this._rootTable[s.Length];
            if (!hashtable.ContainsKey(s))
            {
                hashtable.Add(s, 1);
            }
            else
            {
                hashtable[s] = IntegerType.FromObject(hashtable[s]) + 1;
            }
        }

        public void InsertWord(string s)
        {
            int num2 = s.Length - 1;
            for (int i = 0; i <= num2; i++)
            {
                string str = s.Substring(0, i + 1);
                this.InsertSubString(str);
            }
        }
    }
}

