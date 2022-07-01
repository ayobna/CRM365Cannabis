using CannabisPlugins.Common;
using CannabisPlugins.Models;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisPlugins.Request
{
    class RequestBL
    {
        IOrganizationService service;
        ITracingService trace;

        Queries queries;
        public RequestBL(IOrganizationService service, ITracingService trace)
        {
            this.service = service;
            this.trace = trace;
            queries = new Queries(service);
        }
      

        internal void PreCreate(new_request target)
        {
            CalcuationSlaDate(target);

        }

        internal void PreUpdate(new_request target, new_request preImage)
        {
            SetValueForSLAexception(target, preImage);

        }
        internal void PreUpdateAsync(new_request target, new_request preImage)
        {
            CreateNewTreatmentlines(target, preImage);

        }
        private void SetValueForSLAexception(new_request target, new_request preImage)
        {
            if (target.Contains("new_status"))
            {
                if (target.new_status.Value.Equals((int)RequestStatus.EndOfTreatment))
                {
                    if (DateTime.Now > preImage.new_slaend)
                    {
                        target.new_slaexception = true;
                    }
                }
            }
          
        }
   

        private void CalcuationSlaDate(new_request target)
        {
            if (target.Contains("new_requestnumber"))
            {
                Guid id = target.new_topic.Id;
                DataCollection<Entity> subjects = queries.GetEntityByIdAndEntityNameAndKeyAndFildsName(id, "new_subjects", "new_subjectsid", "new_sla");
                if (subjects != null)
                {
                    new_subjects s = subjects[0].ToEntity<new_subjects>();
                    target.new_slaend = DateTime.Now.AddDays((int)s.new_SLA);
                }
            }
        }


        private void CreateNewTreatmentlines(new_request target, new_request preImage)
        {
            if (target.Contains("new_status"))
            {
                new_treatmentlines treatmentlines = new new_treatmentlines();
                treatmentlines.new_status = target.new_status;

                treatmentlines.new_description = target.Contains("new_treatmentdescription") ?
                target.new_treatmentdescription :
                preImage.new_treatmentdescription;

                treatmentlines.new_request = new EntityReference("new_request", target.Id);

                service.Create(treatmentlines);
            }
        }


    }
}
