using SalesModule.BusinessLogic;
using SalesModule.DataAccess;
using System;
using System.Configuration;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace SalesModule.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new UnityContainer();

            string connString = ConfigurationManager.ConnectionStrings["SalesModuleDB"].ConnectionString;

            // DataAccess registrations
            container.RegisterType<ICustomerDataAccess, CustomerDataAccess>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(connString)
            );
            container.RegisterType<IInvoiceDataAccess, InvoiceDataAccess>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(connString)
            );
            container.RegisterType<IPaymentDataAccess, PaymentDataAccess>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(connString)
            );
            container.RegisterType<IItemDataAccess, ItemDataAccess>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(connString)
            );
            container.RegisterType<ISalesOrderDataAccess, SalesOrderDataAccess>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(connString)
            );

            // BusinessLogic registrations
            container.RegisterType<ICustomerService, CustomerService>(
                new ContainerControlledLifetimeManager()
            );
            container.RegisterType<IInvoiceService, InvoiceService>(
                new ContainerControlledLifetimeManager()
            );
            container.RegisterType<IPaymentService, PaymentService>(
                new ContainerControlledLifetimeManager()
            );
            container.RegisterType<IItemService, ItemService>(
                new ContainerControlledLifetimeManager()
            );
            container.RegisterType<ISalesOrderService, SalesOrderService>(
                new ContainerControlledLifetimeManager()
            );
            container.RegisterType<IGLTransactionDataAccess, GLTransactionDataAccess>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(connString)
            );

            Application["UnityContainer"] = container;
        }
    }
}