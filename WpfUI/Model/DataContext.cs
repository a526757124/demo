using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUI.Model
{
    public class DataContext
    {
        public List<SubjectType> SubjectType = new List<SubjectType>();
        public List<Subject> Subject = new List<Subject>();
        public DataContext()
        {

        }
    }
}
