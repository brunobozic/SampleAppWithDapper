using System;

namespace SampleAppWithDapper.ToastrAlertHelpers
{
    [Serializable]
    public class ToastMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public ToastType ToastType { get; set; }
        public bool IsSticky { get; set; }
        public bool CloseWindowButtonShown { get; set; }
        public ToastrPositionEnum Position { get; set; }
        public bool ProgressBar { get; set; }
    }
    public enum ToastrPositionEnum
    {
        TopRight,
        BottomRight
    }
    public enum ToastType
    {
        Error,
        Info,
        Success,
        Warning
    }
}