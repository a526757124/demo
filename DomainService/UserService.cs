using DomainModels;
using Framework.Service;
using PagedList;
using QuerytDtos;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.DEncrypt;
using Utility.Paged;

namespace DomainService
{
    public class UserService : BaseService<User>
    {
        #region 增删改查

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
                return express.OrderByDescending(p=>p.CreatedTime).ToPagedList(queryCond.PageNo, queryCond.PageSize);
            }
        }
        #endregion
    }
}
