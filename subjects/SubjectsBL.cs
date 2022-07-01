using CannabisPlugins.Common;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisPlugins.subjects
{
    class SubjectsBL
    {
        IOrganizationService service;
        ITracingService trace;

        Queries queries;

        public SubjectsBL(IOrganizationService service, ITracingService trace)
        {
            this.trace = trace;
            this.service = service;
            queries = new Queries(service);
        }

        internal void PreUpdate(new_subjects target)
        {
            ifNameExists(target);

        }


        internal void PreCreate(new_subjects target)
        {
            ifNameExists(target);

        }
        internal void ifNameExists(new_subjects target) { 
            if (target.Contains("new_name"))
            {
             
                string name = target.new_name;
                EntityCollection allSystemsettingsEqual = queries.getNamesInByLogicalName(name, "new_subjects");

                if (allSystemsettingsEqual != null && allSystemsettingsEqual.Entities.Count > 0)
                {
                    throw new InvalidPluginExecutionException($"Duplicate name!");
                }

            }
        }



    }
}
