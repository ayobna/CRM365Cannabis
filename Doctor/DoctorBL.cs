using CannabisPlugins.Common;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisPlugins.Doctor
{
    class DoctorBL
    {
        IOrganizationService service;
        ITracingService trace;

        public DoctorBL(IOrganizationService service, ITracingService trace)
        {
            this.service = service;
            this.trace = trace;
        }
        internal void PreUpdate(Entity target, Entity preImage)
        {
            EnforceRequiredFields(target, preImage);

        }

        internal void PreCreate(Entity target)
        {
            EnforceRequiredFields(target, target);
        }

        private void EnforceRequiredFields(Entity target, Entity preImage)
        {

            Queries queries = new Queries(service);

            EntityReference user = target.GetAttributeValue<EntityReference>("new_systemuserid");



            if (user != null)
            {
                DataCollection<Entity> Checkuser = queries.CheckDoctorUser(user.Id);
                if (Checkuser != null && Checkuser.Count > 0)
                {
                    throw new InvalidPluginExecutionException($"User allready exist {user.Name}");
                }
            }



        }
    }
}
