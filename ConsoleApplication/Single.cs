using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public sealed class Singleton
    {
        public static readonly Singleton Instance = new Singleton();
        private Singleton() { }
    }
}
