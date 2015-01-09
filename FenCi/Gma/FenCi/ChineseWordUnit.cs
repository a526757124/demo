namespace Gma.FenCi
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ChineseWordUnit
    {
        private string _word;
        private int _power;
        public string Word
        {
            get
            {
                return this._word;
            }
        }
        public int Power
        {
            get
            {
                return this._power;
            }
        }
        public ChineseWordUnit(string word, int power)
        {
            this = new Gma.FenCi.ChineseWordUnit();
            this._word = word;
            this._power = power;
        }
    }
}

