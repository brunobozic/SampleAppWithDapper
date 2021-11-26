using AspNetCoreHero.ToastNotification;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SampleAppWithDapper;
using SampleAppWithDapper.Controllers;
using SampleAppWithDapper.DataAccess;
using SampleAppWithDapper.DataAccess.Repositories.Contact;
using SampleAppWithDapper.Domain.DomainModels.Identity;
using SampleAppWithDapper.EmailSending;
using SampleAppWithDapper.ServicePattern;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Diagnostics;
using System.IO;

namespace ContactManager.Web.MVC
{
    public class Startup
    {
        private const string OutputTemplate = "{Timestamp:HH:mm} [{Level}] [{Address}] {Site}: {Message} || CommandType: [{Command_Type}], CommandId: [{Command_Id}], Application: [{Application}], Machine: [{MachineName}], User: [{EnvironmentUserName}], CorrelationId: [{CorrelationId}], DebuggerAttached: [{DebuggerAttached}] {NewLine}";
        private static readonly string @namespace = typeof(Program).Namespace;
        public static readonly string Namespace = @namespace;

        public static readonly string AppName = Namespace;

        public IWebHostEnvironment Env { get; set; }

        public Serilog.ILogger Logger { get; }

        public IConfiguration Configuration { get; }

        public ILoggerFactory LoggerFactory { get; set; }

        private MapperConfiguration MapperConfiguration { get; set; }

        public Startup(
            IConfiguration configuration
                  , ILoggerFactory loggerFactory
                  , IWebHostEnvironment env
              )
        {
            this.Configuration = configuration;
            this.LoggerFactory = loggerFactory;
            this.Env = env;
            //this.Logger = ConfigureLogger(configuration);
            //this.LoggerFactory.AddSerilog(Logger);
            //this.Logger.Information("Logger configured");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory, SqlConnectionFactory>(sp =>
            {
                var conn = this.Configuration.GetConnectionString("DefaultConnection");

                return new SqlConnectionFactory(conn);
            });
            // services.AddToastify(config => { config.DurationInSeconds = 5; config.Position = Position.Right; config.Gravity = Gravity.Bottom; });
            services.AddNotyf(config => { config.DurationInSeconds = 5; config.Position = NotyfPosition.BottomRight; });
            services.AddScoped(typeof(IContactRepositoryAsync), typeof(ContactRepositoryAsync));
            services.AddSingleton<IDbConnectionProvider, DatabaseConnectionManager>(sp =>
            {
                var conn = this.Configuration.GetConnectionString("DefaultConnection");

                return new DatabaseConnectionManager(conn);
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IService<,>), typeof(GenericService<,>));
            services.AddScoped(typeof(IServiceAsync<,>), typeof(GenericServiceAsync<,>));

            services.AddSingleton<DatabaseConnectionManager, DatabaseConnectionManager>(sp =>
            {
                var conn = this.Configuration.GetConnectionString("DefaultConnection");

                return new DatabaseConnectionManager(conn);
            });

            // services.AddSingleton(typeof(ILogger), typeof(Logger));
            services.AddScoped(typeof(IEmailSender), typeof(EmailSender));
            services.AddScoped(typeof(UserManager<>), typeof(UserManager<>));
            services.AddScoped(typeof(SignInManager<>), typeof(SignInManager<>));
            services.AddScoped(typeof(IUserStore<>), typeof(MyUserStore<>));
            services.AddScoped(typeof(IRoleStore<>), typeof(MyRoleStore<>));
            //container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>());
            //container.Register(Component.For(typeof(IService<,>)).ImplementedBy(typeof(GenericService<,>)).LifestyleTransient());
            //container.Register(Component.For(typeof(IServiceAsync<,>)).ImplementedBy(typeof(GenericServiceAsync<,>)).LifestyleTransient());
            //container.Register(Component.For(typeof(IConnectionFactory)).ImplementedBy(typeof()).DependsOn(Dependency.OnValue("connectionString", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)).LifestyleSingleton());
            //container.Register(Component.For(typeof(ILoggingHelper)).ImplementedBy(typeof(LoggingHelper.LoggingHelper)).LifestyleSingleton());
            //container.Register(Component.For(typeof(IContactRepositoryAsync)).ImplementedBy(typeof(ContactRepositoryAsync)).LifestyleTransient());
            //container.Register(Component.For(typeof(IDbConnectionProvider)).ImplementedBy(typeof(DatabaseConnectionManager)).DependsOn(Dependency.OnValue("connection", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)).LifestyleSingleton());
            //container.Register(Component.For(typeof(ILogger)).ImplementedBy<Logger>());
            //container.Register(Component.For(typeof(IEmailSender)).ImplementedBy<EmailSender>());
            //container.Register(Component.For(typeof(UserManager<ApplicationUser>)).ImplementedBy(typeof(UserManager<ApplicationUser>)).LifestyleTransient());
            //container.Register(Component.For(typeof(SignInManager<ApplicationUser>)).ImplementedBy(typeof(SignInManager<ApplicationUser>)).LifestyleTransient());
            //container.Register(Component.For(typeof(IUserStore<>)).ImplementedBy(typeof(UserStore)).LifestyleTransient());
            //container.Register(Component.For(typeof(IRoleStore<>)).ImplementedBy(typeof(MyRoleStore)).LifestyleTransient());

            services.AddIdentity<ApplicationUser, ApplicationRole>().AddDefaultTokenProviders();

            // Normal AddLogging
            services.AddLogging();

            // Additional code to register the ILogger as a ILogger<T> where T is the Startup class
            services.AddSingleton(typeof(Microsoft.Extensions.Logging.ILogger), typeof(Logger<Startup>));
            //services.Configure<IdentityOptions>(options =>
            //{
            //    // Password settings.
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequiredUniqueChars = 1;

            //    // Lockout settings.
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //    options.Lockout.MaxFailedAccessAttempts = 5;
            //    options.Lockout.AllowedForNewUsers = true;

            //    // User settings.
            //    options.User.AllowedUserNameCharacters =
            //    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //    options.User.RequireUniqueEmail = false;
            //});
            ////services.ConfigureApplicationCookie(options =>
            ////{
            ////    // Cookie settings
            ////    options.Cookie.HttpOnly = true;
            ////    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            ////    options.LoginPath = "/Identity/Account/Login";
            ////    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            ////    options.SlidingExpiration = true;
            ////});
            services.AddControllersWithViews(config =>
            {
                // using Microsoft.AspNetCore.Mvc.Authorization;
                // using Microsoft.AspNetCore.Authorization;
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

            });
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            // services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy());

            // services.AddHttpLogging(logging =>
            // {
            //    // Customize HTTP logging here.
            //    logging.LoggingFields = HttpLoggingFields.All;
            //    logging.RequestHeaders.Add("My-Request-Header");
            //    logging.ResponseHeaders.Add("My-Response-Header");
            //    logging.MediaTypeOptions.AddText("application/javascript");
            //    logging.RequestBodyLogLimit = 4096;
            //    logging.ResponseBodyLogLimit = 4096;
            // });
        }

        private Serilog.ILogger ConfigureLogger(IConfiguration configuration)
        {
            var appInstanceName = configuration["InstanceName"];
            var environment = configuration["Environment"];

            if (string.IsNullOrEmpty(appInstanceName)) { appInstanceName = AppName; }
            var path = Path.Combine(AppContext.BaseDirectory, appInstanceName + ".log");

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
                .WriteTo.Console(
                    theme: AnsiConsoleTheme.Code,
                    outputTemplate:
                    OutputTemplate)
                .WriteTo.File("Logs/" + appInstanceName + "-" + ".log", rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: null)
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapRazorPages();
                // endpoints.MapControllers();
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
