using System;

namespace SampleAppWithDapper.LoggingHelper
{
    public interface ILoggingHelper
    {
        void Error(Exception ex, string message);
        void Warning(Exception ex, string message);
        void Debug(Exception ex, string message);
        void Verbose(Exception ex, string message);
        void Fatal(Exception ex, string message);
        void Information(Exception ex, string message);

    }
}