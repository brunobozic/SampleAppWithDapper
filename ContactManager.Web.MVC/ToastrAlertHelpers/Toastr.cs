

using System;
using System.Collections.Generic;

namespace SampleAppWithDapper.ToastrAlertHelpers
{
    [Serializable]
    public class Toastr
    {
        public bool ShowNewestOnTop { get; set; }
        public bool ShowCloseButton { get; set; }
        public bool ShowProgressBar { get; set; }
        public bool ShowCloseWindowButton { get; set; }

        public List<ToastMessage> ToastMessages { get; set; }
        public string Position { get; set; }

        public ToastMessage AddToastMessage(
            string title
            , string message
            , ToastType toastType
            , bool closeButton = true
            , bool progressBar = true
            , bool promptToCloseWindow = false
        )
        {
            var toast = new ToastMessage
            {
                Title = title,
                Message = message,
                ToastType = toastType,
                CloseWindowButtonShown = promptToCloseWindow,
                IsSticky = false,
                Position = ToastrPositionEnum.TopRight,
                ProgressBar = progressBar
            };

            ShowCloseButton = closeButton;
            ShowProgressBar = progressBar;
            ShowCloseWindowButton = promptToCloseWindow;

            ToastMessages.Add(toast);

            return toast;
        }

        public Toastr()
        {
            ToastMessages = new List<ToastMessage>();
            ShowNewestOnTop = true;
            ShowCloseButton = true;
            ShowProgressBar = true;
            ShowCloseWindowButton = false;
        }
    }
}