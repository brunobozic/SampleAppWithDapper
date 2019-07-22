using System;
using Serilog;
using Serilog.Events;

namespace SampleAppWithDapper.LoggingHelper
{
    public class LoggingHelper : ILoggingHelper
    {
        private static readonly ILogger Errorlog;
        private static readonly ILogger Warninglog;
        private static readonly ILogger Debuglog;
        private static readonly ILogger Verboselog;
        private static readonly ILogger Fatallog;

        static LoggingHelper()
        {

            // 5 MB = 5242880 bytes

            Errorlog = new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.RollingFile(System.Web.Hosting.HostingEnvironment.MapPath("~/ErrorLog/Error/log.txt"), fileSizeLimitBytes: 5242880)
                .CreateLogger();

            Warninglog = new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.RollingFile(System.Web.Hosting.HostingEnvironment.MapPath("~/ErrorLog/Warning/log.txt"), fileSizeLimitBytes: 5242880)
                .CreateLogger();

            Debuglog = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile(System.Web.Hosting.HostingEnvironment.MapPath("~/ErrorLog/Debug/log.txt"), fileSizeLimitBytes: 5242880)
                .CreateLogger();

            Verboselog = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.RollingFile(System.Web.Hosting.HostingEnvironment.MapPath("~/ErrorLog/Verbose/log.txt"), fileSizeLimitBytes: 5242880)
                .CreateLogger();

            Fatallog = new LoggerConfiguration()
              .MinimumLevel.Fatal()
                .WriteTo.RollingFile(System.Web.Hosting.HostingEnvironment.MapPath("~/ErrorLog/Fatal/log.txt"), fileSizeLimitBytes: 5242880)
                .CreateLogger();

        }

        public void Error(Exception ex, string message)
        {
            Errorlog.Write(LogEventLevel.Error, ex, message);
        }

        public void Warning(Exception ex, string message)
        {
            Warninglog.Write(LogEventLevel.Warning, ex, message);
        }

        public void Debug(Exception ex, string message)
        {
            Debuglog.Write(LogEventLevel.Debug, ex, message);
        }

        public void Verbose(Exception ex, string message)
        {
            Verboselog.Write(LogEventLevel.Verbose, ex, message);
        }

        public void Fatal(Exception ex, string message)
        {
            Fatallog.Write(LogEventLevel.Fatal, ex, message);
        }

        public void Information(Exception ex, string message)
        {
            Fatallog.Write(LogEventLevel.Fatal, ex, message);
        }

    }
}