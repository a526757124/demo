using ETVS.DomainModels;
using ETVS.Framework;
using ETVS.Framework.Service.Core;
using ETVS.Utility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETVS.DomainService
{
    public class SubjectTypeService : ServiceBase
    {
        private readonly IRepository<SubjectType, int> _subjectTypeRepository;
        /// <summary>
        /// 初始化一个<see cref="IdentityService"/>类型的新实例
        /// </summary>
        public SubjectTypeService(IRepository<SubjectType, int> subjectTypeRepository)
            : base(subjectTypeRepository.UnitOfWork)
        {
            _subjectTypeRepository = subjectTypeRepository;
        }
        public OperationResult Insert(SubjectType subjectType)
        {
            base.UnitOfWork.TransactionEnabled = true;
            var re= _subjectTypeRepository.Insert(subjectType);
            re= UnitOfWork.SaveChanges();
            return new OperationResult(OperationResultType.Success);
        }

    }
}
