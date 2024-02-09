using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SampleAppWithDapper.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public INotyfService _toasts { get; }

        public HomeController(INotyfService toasts)
        {
            this._toasts = toasts;
        }

        public ActionResult Index()
        {
            //_toasts.Custom("Custom Notification - closes in 5 seconds.", 5, "whitesmoke", "fa fa-gear");
            //_toasts.Custom("Custom Notification - closes in 10 seconds.", 10, "#B600FF", "fa fa-home");
            return this.View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}