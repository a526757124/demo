using Core.WebHelper;
using DomainModels;
using Framework.Enums;
using Framework.Service;
using PagedList;
using QuerytDtos;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.DEncrypt;
using Utility.Exceptions;
using Utility.Paged;

namespace DomainService
{
    public class UserService : BaseService<User>
    {
        #region 增删改查
        public void Login(string LoginName, string PassWord, int companyId)
        {
            using (ETVSContext context = new ETVSContext())
            {
                PassWord = Encrypt.Md5(PassWord);
                var user = context.Users.FirstOrDefault(p => p.LoginName.Equals(LoginName) && p.Password.Equals(PassWord));
                if (user != null && user.Id > 0)
                {
                    SessionHelper.SetSession(SessionEnum.LoginUser.ToString(), user);
                    SessionHelper.SetSession(SessionEnum.OperateCompany.ToString(), new CompanyService().GetById(companyId));
                }
                else
                {
                    throw new BusinessException("登录失败，用户名或者密码错误！");
                }
            }
        }
        public void Add(User model)
        {
            using (ETVSContext context = new ETVSContext())
            {
                model.Password = Encrypt.Md5("123456");
                context.Users.Add(model);
                context.SaveChanges();
            }
        }
        public void Update(User model)
        {
            using (ETVSContext context = new ETVSContext())
            {
                var user = context.Users.FirstOrDefault(p => p.Id == model.Id);
                user.LoginName = model.LoginName;
                user.UserName = model.UserName;
                context.SaveChanges();
            }
        }
        public void Delete(int Id)
        {
            using (ETVSContext context = new ETVSContext())
            {
                context.SaveChanges();
            }
        }
        public User GetById(int Id)
        {
            using (ETVSContext context = new ETVSContext())
            {
                return context.Users.FirstOrDefault(p => p.Id == Id);
            }
        }
        public IPagedList<User> List(PagedParam<UserQuery> queryCond)
        {
            using (ETVSContext context = new ETVSContext())
            {
                var express = context.Users
                        .Where(p => !p.IsDeleted);
                return express.OrderByDescending(p => p.CreatedTime).ToPagedList(queryCond.PageNo, queryCond.PageSize);
            }
        }
        #endregion
    }
}
