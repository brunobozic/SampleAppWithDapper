using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleAppWithDapper.ToastrAlertHelpers;

namespace SampleAppWithDapper.ControllerExtensions
{
    public static class ControllerExtensions
    {
        public static ToastMessage AddToastMessage(
            this Controller controller
            , string title
            , string message
            , ToastType toastType = ToastType.Info
            , bool closeButoon = true
            , bool progressBar = true
            , bool promptToCloseForm = false
        )
        {
            Toastr toastr = controller.TempData["Toastr"] as Toastr;
            toastr = toastr ?? new Toastr();

            var toastMessage = toastr.AddToastMessage(title, message, toastType, closeButoon, progressBar, promptToCloseForm);
            controller.TempData["Toastr"] = toastr;

            return toastMessage;
        }
    }
}