using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using Castle.MicroKernel;

namespace SampleAppWithDapper.Container
{
    public class WindsorActionInvoker : AsyncControllerActionInvoker
    {
        private readonly IKernel _kernel;


        public WindsorActionInvoker(IKernel kernel)
        {
            _kernel = kernel;
        }

        protected override ActionExecutedContext InvokeActionMethodWithFilters(ControllerContext controllerContext,
            IList<IActionFilter> filters,
            ActionDescriptor actionDescriptor,
            IDictionary<string, object> parameters)
        {
            foreach (IActionFilter actionFilter in filters)
            {
                _kernel.InjectProperties(actionFilter);
            }
            return base.InvokeActionMethodWithFilters(controllerContext, filters, actionDescriptor, parameters);
        }
    }
}