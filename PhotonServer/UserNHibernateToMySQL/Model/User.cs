using System;

namespace UserNHibernateToMySQL.Model
{
    class User
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set;}
        public virtual string Password { get; set; }
        public virtual DateTime Registerdate { get; set; }
    }
}
