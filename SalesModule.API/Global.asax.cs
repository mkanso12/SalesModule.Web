using SalesModule.BusinessLogic;
using SalesModule.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity.AspNet.WebApi;
using Unity.Injection;
using Unity.Lifetime;
using Unity;
using AutoMapper;
using SalesModule.API.MappingProfiles;

namespace SalesModule.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SalesModuleProfile>();
            });
            IMapper mapper = mapperConfig.CreateMapper();

            var container = new UnityContainer();

            string connString = ConfigurationManager.ConnectionStrings["SalesModuleDB"].ConnectionString;

            container.RegisterType<IInvoiceDataAccess, InvoiceDataAccess>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(connString)
            );

            container.RegisterType<IPaymentDataAccess, PaymentDataAccess>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(connString)
            );

            container.RegisterType<IInvoiceService, InvoiceService>(
                new ContainerControlledLifetimeManager()
            );

            container.RegisterType<IPaymentService, PaymentService>(
                new ContainerControlledLifetimeManager()
            );
            container.RegisterType<ISalesOrderDataAccess, SalesOrderDataAccess>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(connString)
            );

            container.RegisterType<ISalesOrderService, SalesOrderService>(
                new ContainerControlledLifetimeManager()
            );
            container.RegisterType<IGLTransactionDataAccess, GLTransactionDataAccess>(
             new ContainerControlledLifetimeManager(),
             new InjectionConstructor(connString)
            );
            container.RegisterInstance<IMapper>(mapper);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}