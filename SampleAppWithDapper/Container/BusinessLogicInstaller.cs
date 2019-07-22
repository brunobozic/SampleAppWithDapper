using System.Configuration;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.AspNet.Identity;
using SampleAppWithDapper.DataAccess;
using SampleAppWithDapper.DataAccess.Repositories;
using SampleAppWithDapper.DataAccess.Repositories.AppUsers;
using SampleAppWithDapper.DataAccess.Repositories.Contact;
using SampleAppWithDapper.Domain.DomainModels.Identity;
using SampleAppWithDapper.LoggingHelper;
using SampleAppWithDapper.ServicePattern;

namespace SampleAppWithDapper.Container
{
    public class BusinessLogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());
            container.Register(Classes.FromThisAssembly().BasedOn<FilterAttribute>().LifestyleTransient());
            container.Register(Component.For<IActionInvoker>().ImplementedBy<WindsorActionInvoker>().DependsOn(Dependency.OnValue("container", container)).LifestyleTransient());
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>());
            container.Register(Component.For(typeof(IService<,>)).ImplementedBy(typeof(GenericService<,>)).LifestyleTransient());
            container.Register(Component.For(typeof(IServiceAsync<,>)).ImplementedBy(typeof(GenericServiceAsync<,>)).LifestyleTransient());

            container.Register(Component.For<IUserRepositoryAsync>().ImplementedBy<UserRepositoryAsync>()
            , Component.For(typeof(IUserStore<AppUser>)).Named("UserStore").ImplementedBy(typeof(UserRepositoryAsync)).LifestyleTransient()
            , Component.For(typeof(IUserLoginStore<AppUser>)).Named("UserLoginStore").ImplementedBy(typeof(UserRepositoryAsync)).LifestyleTransient()
            , Component.For(typeof(IUserPasswordStore<AppUser>)).Named("UserPasswordStore").ImplementedBy(typeof(UserRepositoryAsync)).LifestyleTransient()
            , Component.For(typeof(IUserSecurityStampStore<AppUser>)).Named("UserSecurityStampStore").ImplementedBy(typeof(UserRepositoryAsync)).LifestyleTransient());
            container.Register(Component.For(typeof(UserManager<>)).ImplementedBy(typeof(UserManager<>)).LifestyleTransient());
            container.Register(Component.For(typeof(IConnectionFactory)).ImplementedBy(typeof(SqlConnectionFactory)).DependsOn(Dependency.OnValue("connectionString", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)).LifestyleSingleton());

            container.Register(Component.For(typeof(ILoggingHelper)).ImplementedBy(typeof(LoggingHelper.LoggingHelper)).LifestyleSingleton());
            container.Register(Component.For(typeof(IContactRepository)).ImplementedBy(typeof(ContactRepository)).LifestyleTransient());
            container.Register(Component.For(typeof(IDbConnectionProvider)).ImplementedBy(typeof(DatabaseConnectionManager)).DependsOn(Dependency.OnValue("connection", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)).LifestyleSingleton());
            
            //kernel.Bind<IConnectionFactory>().To<SqlConnectionFactory>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            //kernel.Bind<IUserRepository>().To<UserRepository>();
            //kernel.Bind<IUserStore<User>>().To<UserRepository>();
            //kernel.Bind<IUserLoginStore<User>>().To<UserRepository>();
            //kernel.Bind<IUserPasswordStore<User>>().To<UserRepository>();
            //kernel.Bind<IUserSecurityStampStore<User>>().To<UserRepository>();
            //kernel.Bind(typeof(UserManager<>)).ToSelf();
        }
    }
}