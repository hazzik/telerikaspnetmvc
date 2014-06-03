namespace AltNorthwind
{
    using System;
    using System.Web;

    using NHibernate;
    using NHibernate.Context;

    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    public class NHibernateSessionPerRequest : IHttpModule
    {
        private static readonly ISessionFactory sessionFactory = CreateSessionFactory();

        public static ISession CurrentSession
        {
            get
            {
                return sessionFactory.GetCurrentSession();
            }
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
            context.EndRequest += OnEndRequest;
        }

        public void Dispose()
        {
        }

        private static void OnBeginRequest(object sender, EventArgs e)
        {
            var session = sessionFactory.OpenSession();

            session.BeginTransaction();

            CurrentSessionContext.Bind(session);
        }

        private static void OnEndRequest(object sender, EventArgs e)
        {
            var session = CurrentSessionContext.Unbind(sessionFactory);

            if (session != null)
            {
                try
                {
                    session.Transaction.Commit();
                }
                catch
                {
                    session.Transaction.Rollback();
                }
                finally
                {
                    session.Close();
                    session.Dispose();
                }
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            const string ConnectionStringName = "NorthwindConnectionString";

            var configuration = Fluently.Configure()
                                        .Database(MsSqlConfiguration.MsSql2005.ConnectionString(x => x.FromConnectionStringWithKey(ConnectionStringName)))
                                        .ExposeConfiguration(c => c.SetProperty("current_session_context_class", "web"))
                                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateSessionPerRequest>());

            return configuration.BuildSessionFactory();
        }
    }
}