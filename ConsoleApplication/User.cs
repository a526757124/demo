using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public char Sex { get; set; }
        public virtual Department Dept { get; set; }
    }
}
