using ATMOS_SROM.DataAccess;
using ATMOS_SROM.Services;
using Autofac;
using log4net;
using log4net.Config;
using System;

namespace ATMOS_SROM
{
    public class Global : System.Web.HttpApplication
    {
        private static IContainer Container { get; set; }

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/web.config")));
            var builder = new ContainerBuilder();

            builder.Register(c => LogManager.GetLogger(typeof(object))).As<ILog>().InstancePerDependency();
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ArticleService>().As<IArticleService>();
            builder.RegisterType<ArticleDbTransactionService>().As<IArticleDbTransactionService>();

            Container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
