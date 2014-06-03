namespace Northwind
{
    using System;
    using System.Web;

    public class EntityFrameworkObjectContextPerRequest : IHttpModule
    {
        private static readonly string key = typeof(Database).FullName;

        public static Database CurrentDatabase
        {
            get
            {
                return HttpContext.Current.Items[key] as Database;
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
            var database = HttpContext.Current.Items[key] as Database;

            if (database == null)
            {
                database = new Database();
                HttpContext.Current.Items[key] = database;
            }
        }

        private static void OnEndRequest(object sender, EventArgs e)
        {
            var database = HttpContext.Current.Items[key] as Database;

            if (database != null)
            {
                database.SaveChanges();
                database.Dispose();
            }
        }
    }
}