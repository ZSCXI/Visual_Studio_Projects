using System.Collections.Generic;
using UserNHibernateToMySQL.Model;
using NHibernate;
using NHibernate.Criterion;

namespace UserNHibernateToMySQL.Manager
{
    //
    class UserManager : IUserManager
    {
        public void Add(User user)
        {
            //ISession session = NHibernateHelper.OpenSession();
            //session.Save(user);
            //session.Close();

            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(user);
                    transaction.Commit();
                }
            }
        }

        public ICollection<User> GetAllUsers()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                //创建一个查询条件对象
                ICriteria criteria = session.CreateCriteria(typeof(User));
                //查询操作
                IList<User> users = criteria.List<User>();

                return users;
            }
        }

        //根据主键查询
        public User GetByID(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    User user = session.Get<User>(id);
                    transaction.Commit();
                    return user;
                }
            }
        }

        public User GetByUsername(string username)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                //创建一个查询条件对象
                ICriteria criteria = session.CreateCriteria(typeof(User));
                //在查询条件对象中添加查询条件
                criteria.Add(Restrictions.Eq("Username",username));//Username为User类的字段名
                //查询操作
                User user = criteria.UniqueResult<User>();//取得查询结果一条记录

                return user;
            }
        }

        //删除是根据主键实现的，因此User对象的主键必须存在
        public void Remove(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(user);
                    transaction.Commit();
                }
            }
        }

        //更新是根据主键实现的，因此User对象的主键必须存在
        public void Update(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(user);
                    transaction.Commit();
                }
            }
        }

        public bool VerifyUser(string username, string password)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                //创建一个查询条件对象
                ICriteria criteria = session.CreateCriteria(typeof(User));
                //在查询条件对象中添加查询条件
                criteria.Add(Restrictions.Eq("Username", username));//Username为User类的字段名
                criteria.Add(Restrictions.Eq("Password", password));//Username为User类的字段名
                //查询操作
                User user = criteria.UniqueResult<User>();//取得查询结果一条记录

                if (user == null) return false;
                else return true;
            }
        }
    }
}
