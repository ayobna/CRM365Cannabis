using CannabisPlugins.Common;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisPlugins.Autonumbering
{
    class AutonumberingBL
    {
        IOrganizationService service;
        ITracingService trace;
        public AutonumberingBL(IOrganizationService service, ITracingService trace)
        {
            this.trace = trace;
            this.service = service;

        }
        internal void Execute(Entity target)
        {

            Queries queries = new Queries(service);
            Entity autonumber = queries.GetAutonumberEntityName(target.LogicalName);


            // Logic
            if (autonumber !=null)
            {
              
                int number = 1;

                if (autonumber.Contains("new_autonumber"))
                {
                    number = autonumber.GetAttributeValue<int>("new_autonumber") + 1;
                }

                // Insert the numvber to the new entity
                target[autonumber.GetAttributeValue<string>("new_fieldname")] = number.ToString();

                // Update the autonumbering entity
                autonumber["new_autonumber"] = number;
                service.Update(autonumber);

            }
            else
            {
                trace.Trace("Autonumber not found.");
            }
        }
    }
}
