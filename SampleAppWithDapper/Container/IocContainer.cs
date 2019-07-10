using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Installer;


namespace SampleAppWithDapper.Container
{
    public class IocContainer
    {
        private static IWindsorContainer _container;

        public static void Setup()
        {
            _container = new WindsorContainer().Install(FromAssembly.This());

            WindsorControllerFactory controllerFactory = new WindsorControllerFactory(_container);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
  
        }
    }
}