using Core.WebHelper;
using DomainModels;
using Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainService
{
    public partial class BaseService<T>
    {
        public User CurrentUser
        {
            set { }
            get {

                return SessionHelper.GetSession<User>(SessionEnum.LoginUser.ToString());
            }
        }
        public Company CurrentCompany
        {
            set { }
            get
            {

                return SessionHelper.GetSession<Company>(SessionEnum.OperateCompany.ToString());
            }
        }
    }
}
