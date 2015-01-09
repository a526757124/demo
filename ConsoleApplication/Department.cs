using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Des { get; set; }
        public virtual Department Parent { get; set; }
    }
}
