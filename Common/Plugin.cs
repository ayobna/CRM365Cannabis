using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisPlugins.Common
{
    public abstract class Plugin : IPlugin
    {
        protected ITracingService trace;
        protected IPluginExecutionContext context;
        protected IOrganizationService service;
        //פלאג אין שעשינו בשיעורים
        public void Execute(IServiceProvider serviceProvider)
        {
            trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            service = serviceFactory.CreateOrganizationService(context.UserId);

            Execute();
        }

        public abstract void Execute();
    }
}
