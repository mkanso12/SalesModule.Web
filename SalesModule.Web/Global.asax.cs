using SalesModule.BusinessLogic;
using SalesModule.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace SalesModule.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new UnityContainer();

            string connString = ConfigurationManager.ConnectionStrings["SalesModuleDB"].ConnectionString;

            // Register IInvoiceDataAccess with its implementation
            container.RegisterType<IInvoiceDataAccess, InvoiceDataAccess>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(connString)
            );

            // Register IInvoiceService
            container.RegisterType<IInvoiceService, InvoiceService>(
                new ContainerControlledLifetimeManager()
            );

            Application["UnityContainer"] = container;
        }
    }
}