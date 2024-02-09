using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleAppWithDapper.Domain.DomainModels.Identity;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Diagnostics;
using System.IO;

namespace ContactManager.Web.MVC
{
    public class Program
    {
        private const string OutputTemplate = "{Timestamp:HH:mm} [{Level}] [{Address}] {Site}: {Message} || CommandType: [{Command_Type}], CommandId: [{Command_Id}], Application: [{Application}], Machine: [{MachineName}], User: [{EnvironmentUserName}], CorrelationId: [{CorrelationId}], DebuggerAttached: [{DebuggerAttached}] {NewLine}";
        public static readonly string Namespace = typeof(Program).Namespace;

        public static readonly string AppName = Namespace;

        [Obsolete]
        public static int Main(string[] args)
        {
            var configuration = GetConfiguration();

            var host = CreateWebHostBuilder(args)
            .Build();

            using (var newScope = host.Services.CreateScope())
            {
                try
                {
                    var userManager = newScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                    var exists = userManager.FindByNameAsync("Admin").Result;

                    if (exists == null)
                    {
                        var adminUser = new ApplicationUser
                        {
                            UserName = "Admin",
                            Email = "bruno.bozic@gmail.com",
                            NormalizedEmail = "BRUNO.BOZIC@GMAIL.COM",
                            EmailConfirmed = true,
                            PhoneNumber = "0994721471",
                            PhoneNumberConfirmed = true,
                            IsActive = true,
                            LastName = "Bozic",
                            FirstName = "Bruno"
                        };

                        var created = userManager.CreateAsync(adminUser, "Admin123456!").Result;

                        if (!created.Succeeded) { Log.Error("Admin user not created, possibly the password is not strong enough?"); }
                    }
                }
                catch (Exception seedEx)
                {
                    Log.Fatal("Error while applying migrations...", seedEx);

                    Debug.WriteLine(seedEx.Message);

                    Console.WriteLine(seedEx.Message);

                    return 1;
                }
            }

            Log.Logger = CreateSerilogLogger(configuration);

            Log.Warning("Starting web host ({ApplicationContext})...", AppName);

            host.Run();

            return 0;
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
#pragma warning restore 1591
        {
            return WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(false)
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIIS() // <===== For use in "in process" IIS scenarios: 
                .UseUrls("http://*:5000")
                .UseSerilog();
        }

#pragma warning disable 1591
        public static IWebHost BuildWebHost(IConfiguration configuration, string[] args)
#pragma warning restore 1591
        {
            return WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(false)
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(configuration)
                .UseIIS() // <===== For use in "in process" IIS scenarios: 
                .UseUrls("http://*:5000")
                .UseSerilog()
                .Build();
        }

        [Obsolete]
        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            var appInstanceName = configuration["InstanceName"];
            var environment = configuration["Environment"];
            if (string.IsNullOrEmpty(appInstanceName)) { appInstanceName = AppName; }
            var path = Path.Combine(AppContext.BaseDirectory, appInstanceName + ".log");

            // var kafkaProducerForLogging = container.Resolve<IKafkaLoggingProducer>();
            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("Application", appInstanceName)
                .Enrich.WithProperty("Environment", environment)
                .Enrich.WithAssemblyName()
                .Enrich.WithAssemblyVersion()
                .Enrich.WithEnvironmentUserName() // environments are tricky when using a windows service
                                                  // .Enrich.WithExceptionData()
                                                  // .Enrich.WithExceptionStackTraceHash()
                                                  // .Enrich.WithMemoryUsage()
                .Enrich.WithThreadId()
                .Enrich.WithThreadName()
                .Enrich.FromLogContext()
                .Enrich.WithProcessName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithEnvironment(environment)
                .Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached)
                .ReadFrom.ConfigurationSection(configuration.GetSection("Serilog"))
                // .WriteTo.Kafka(kafkaProducerForLogging, new EcsTextFormatter()) // this is how we make the sink use a custom text formatter, in this case, we needed the Elastic compatible formatter
                .WriteTo.Console(theme: AnsiConsoleTheme.Code,
                    outputTemplate:
                    OutputTemplate)
                .WriteTo.File("Logs/" + appInstanceName + "-" + ".log", rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: null)
                .CreateLogger();
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", // beware this will default to Production appsettings if no ENV is defined on the OS
                    true)
                .AddJsonFile("appsettings.local.json", true, true) // load local settings (usually used for local debugging sessions)
                .AddEnvironmentVariables();

            var config = builder.Build();

            return config;
        }
    }
}
