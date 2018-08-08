using System;
using System.Collections.Generic;
using System.Linq;
using UserNHibernateToMySQL.Model;

namespace UserNHibernateToMySQL.Manager
{
    //User管理接口
    interface IUserManager
    {
        void Add(User user); //添加用户
        void Update(User user); //更新用户
        void Remove(User user); //删除用户
        User GetByID(int id); //根据ID得到用户

        User GetByUsername(string username); //根据用户名得到用户
        ICollection<User> GetAllUsers(); //得到所有用户，返回一个集合

        bool VerifyUser(string username,string password);
    }
}
