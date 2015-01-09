using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Services
{
    public class Operation
    {
        public int Add(int a, int b)
        {
            if (a > 99999999 || b == 99999999)
                throw new Exception("传入数据不能大于99999999");
            return a + b;
        }
    }
}
