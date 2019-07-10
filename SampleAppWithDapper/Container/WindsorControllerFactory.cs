using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;
using Castle.Windsor;

namespace SampleAppWithDapper.Container
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {


        readonly IWindsorContainer container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            this.container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType != null && container.Kernel.HasComponent(controllerType))
                return (IController)container.Resolve(controllerType);
            if (controllerType != null)
                return base.GetControllerInstance(requestContext, controllerType);
            else return null;
        }

        public override void ReleaseController(IController controller)
        {
            container.Release(controller);
        }
    }
}
