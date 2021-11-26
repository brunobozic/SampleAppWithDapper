using System;

namespace SampleAppWithDapper.Controllers
{
    internal class HandleErrorInfo
    {
        private Exception exception;
        private string v1;
        private string v2;

        public HandleErrorInfo(Exception exception, string v1, string v2)
        {
            this.exception = exception;
            this.v1 = v1;
            this.v2 = v2;
        }
    }
}