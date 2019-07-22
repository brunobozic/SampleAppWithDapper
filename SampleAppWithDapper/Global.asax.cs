using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SampleAppWithDapper.Container;
using SampleAppWithDapper.LoggingHelper;

namespace SampleAppWithDapper
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IocContainer.Setup();

            //var logger = (ILoggingHelper) DependencyResolver.Current.GetService(typeof(ILoggingHelper));
            //logger.Information(null, "Application start @" + DateTime.Now);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            string culture = "en-US";
            if (Request.UserLanguages != null)
            {
                culture = Request.UserLanguages[0];
            }
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
        }
    }
}
