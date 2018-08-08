using System;
using NHibernate;
using NHibernate.Cfg;
using UserNHibernateToMySQL.Model;
using UserNHibernateToMySQL.Manager;
using System.Collections.Generic;

namespace UserNHibernateToMySQL
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User { Id = 20 ,Username = "yyyy2", Password = "7899" };
            User user1 = new User { Id = 20};
            IUserManager userManager = new UserManager();

            //userManager.Add(user);

            //userManager.Update(user);

            //userManager.Remove(user1);

            //User user3 = userManager.GetByID(4);

            //User user3 = userManager.GetByUsername("qt");
            //Console.WriteLine(user3.Username + " " + user3.Password + " " + user3.Registerdate);

            //ICollection<User> users = userManager.GetAllUsers();
            //foreach (User user3 in users)
            //{
            //    Console.WriteLine(user3.Username + " " + user3.Password + " " + user3.Registerdate);
            //}

            Console.WriteLine(userManager.VerifyUser("qt","456")); //True
            Console.WriteLine(userManager.VerifyUser("qt", "4567")); // False

            Console.ReadKey();
        }
    }
}
