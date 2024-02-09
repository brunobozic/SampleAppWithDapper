using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace SampleAppWithDapper.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        public ViewResult HandleException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            var exception = filterContext.Exception;

            Log.Fatal(exception, exception.Message);

            var viewResult = View("Error", new HandleErrorInfo(exception,
                filterContext.RouteData.Values["controller"].ToString(),
                filterContext.RouteData.Values["action"].ToString()));

            return viewResult;
        }
    }
}