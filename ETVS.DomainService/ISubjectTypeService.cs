using ETVS.DomainModels;
using ETVS.Framework;
using ETVS.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETVS.DomainService
{
    public interface ISubjectTypeService : IDependency
    {
        OperationResult Insert(SubjectType subjectType);
    }
}
