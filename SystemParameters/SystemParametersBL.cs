using CannabisPlugins.Common;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisPlugins.SystemParameters
{
    class SystemParametersBL
    {
        IOrganizationService service;
        ITracingService trace;
        Queries queries;
        public SystemParametersBL(IOrganizationService service, ITracingService trace)
        {
            this.service = service;
            this.trace = trace;
            queries = new Queries(service);
        }
        internal void PreUpdate(Entity target)
        {
            ifNameExists(target);
            
        }

        internal void PreCreate(Entity target)
        {
              ifNameExists(target);
        }
        private void ifNameExists(Entity target)
        {

            if (target.Contains("new_name"))
            {
                new_systemsettings systemsettings = (target).ToEntity<new_systemsettings>();
                string name = systemsettings.new_name;

                EntityCollection allSystemsettingsEqual = queries.getNamesInByLogicalName(name, "new_systemsettings");

                if (allSystemsettingsEqual != null && allSystemsettingsEqual.Entities.Count > 0)
                {
                    throw new InvalidPluginExecutionException($"Duplicate name!");
                }
            }
     
        }
    }
}
