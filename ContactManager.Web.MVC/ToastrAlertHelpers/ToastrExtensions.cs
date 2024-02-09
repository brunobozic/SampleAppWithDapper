using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace SampleAppWithDapper.ToastrAlertHelpers
{
    /// <summary>
    /// A strongly-type extension method for accessing TempData which
    /// is used to store our Alerts.
    /// </summary>
    public static class ToastrExtensions
    {
        private const string Toastr = "Toastr";

        public static Toastr GetToastr(this TempDataDictionary tempData)
        {
            if (!tempData.ContainsKey(Toastr))
            {
                tempData[Toastr] = new Toastr();
            }

            return (Toastr)tempData[Toastr];
        }

        // helper methods to simplify the creation of the AlertDecoratorResult types
        public static ActionResult WithSuccess(this ActionResult result, ToastType toastType, string title, string message, bool closeButton = true, bool progressBar = true, bool closeWindowButtonShown = false)
        {
            return new ToastrDecoratorResult(result, toastType, title, message, closeButton, progressBar, closeWindowButtonShown);
        }

        public static ActionResult WithInfo(this ActionResult result, ToastType toastType, string title, string message, bool closeButton = true, bool progressBar = true, bool closeWindowButtonShown = false)
        {
            return new ToastrDecoratorResult(result, toastType, title, message, closeButton, progressBar, closeWindowButtonShown);
        }

        public static ActionResult WithWarning(this ActionResult result, ToastType toastType, string title, string message, bool closeButton = true, bool progressBar = true, bool closeWindowButtonShown = false)
        {
            return new ToastrDecoratorResult(result, toastType, title, message, closeButton, progressBar, closeWindowButtonShown);
        }

        public static ActionResult WithError(this ActionResult result, ToastType toastType, string title, string message, bool closeButton = true, bool progressBar = true, bool closeWindowButtonShown = false)
        {
            return new ToastrDecoratorResult(result, toastType, title, message, closeButton, progressBar, closeWindowButtonShown);
        }
    }
}