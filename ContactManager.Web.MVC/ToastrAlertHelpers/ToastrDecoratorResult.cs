using Microsoft.AspNetCore.Mvc;

namespace SampleAppWithDapper.ToastrAlertHelpers
{
    /// <summary>
    /// Adds the alerts to an existing ActionResult
    /// </summary>
    public class ToastrDecoratorResult : ActionResult
    {
        public ActionResult InnerResult { get; set; }
        public ToastType Style { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public bool CloseButton { get; set; }
        public bool ProgressBar { get; set; }
        public bool CloseWindowButtonShown { get; set; }


        public ToastrDecoratorResult(ActionResult innerResult, ToastType style, string title, string message, bool closeButton = true, bool progressBar = true, bool closeWindowButtonShown = false)
        {
            InnerResult = innerResult;
            Style = style;
            Message = message;
            Title = title;
            CloseButton = closeButton;
            ProgressBar = progressBar;
            CloseWindowButtonShown = closeWindowButtonShown;
        }


    }
}