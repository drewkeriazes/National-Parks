using Capstone.Web.DAL;
using Ninject;
using Ninject.Web.Common.WebHost;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Capstone.Web
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            kernel.Bind<IParkDAL>().To<ParkSqlDAL>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<ISurveyDAL>().To<SurveySqlDAL>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<IWeatherDAL>().To<WeatherSqlDAL>().WithConstructorArgument("connectionString", connectionString);

            return kernel;
        }
    }
}
