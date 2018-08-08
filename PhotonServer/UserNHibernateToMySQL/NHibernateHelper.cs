using NHibernate;
using NHibernate.Cfg;

namespace UserNHibernateToMySQL
{
    class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();//根据默认路径下解析hibernate.cfg.xml
                    //解析配置文件
                    configuration.Configure(); 
                    //解析映射文件
                    configuration.AddAssembly("UserNHibernateToMySQL");//解析User.hbm.xml;
                    _sessionFactory = configuration.BuildSessionFactory(); //创建seeion工厂
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            //打开与数据库的连接
            return SessionFactory.OpenSession();//SessionFactory会调用上述get方法
        }
    }
}
