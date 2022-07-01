
using CannabisPlugins.Common;
using CannabisPlugins.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisPlugins.License
{
    class LicenceBL
    {
        IOrganizationService service;
        ITracingService trace;


        const int MAX_AMOUNT = 200;
        public LicenceBL(IOrganizationService service, ITracingService trace)
        {
            this.trace = trace;
            this.service = service;

        }

        internal void PreUpdate(Entity target, Entity preImage)
        {
            CheckAmount(target, preImage);
            SetStatusDate(target);

        }

        internal void PreCreate(Entity target)
        {
            // EnforceRequiredFields(target, target);
            CheckAmount(target, target);
            SetStatusDate(target);
        }
        private void SetStatusDate(Entity target)
        {
            if (target.Contains("statuscode"))
            {
                target["new_statusdate"] = DateTime.Now.ToLocalTime();
                CreateLicenseStatus(service, target);
            }
        }
        private void CreateLicenseStatus(IOrganizationService service, Entity target)
        {
            Entity e1 = new Entity("new_licensestatus");
            e1["new_status"] = target.GetAttributeValue<OptionSetValue>("statuscode");
            e1["new_licenseid"] = new EntityReference("new_license", target.Id);
            service.Create(e1);
        }

        private void CheckAmount(Entity target, Entity preImage)
        {
            Queries queries = new Queries(service);

            OptionSetValue status = target.Contains("statuscode") ?
                   target.GetAttributeValue<OptionSetValue>("statuscode") :
                preImage.GetAttributeValue<OptionSetValue>("statuscode");
            if (status.Value == (int)LicenceStatus.Approved)
            {


                int currentAmount = target.Contains("new_amount") ?
                  target.GetAttributeValue<int>("new_amount") :
                    preImage.GetAttributeValue<int>("new_amount");

                EntityReference Patient = target.Contains("new_patientid") ?
                target.GetAttributeValue<EntityReference>("new_patientid") :
                preImage.GetAttributeValue<EntityReference>("new_patientid");

                int totalAmounte = currentAmount;

                if (Patient != null)
                {
                    DataCollection<Entity> licenses = queries.GetLicenseByPatient(Patient.Id);
                    if (licenses.Count > 0)
                    {
                        foreach (var license in licenses)
                        {
                            if (license.Id != target.Id &&
                                license.GetAttributeValue<OptionSetValue>("statuscode").Value == (int)LicenceStatus.Approved)
                            {
                                totalAmounte += license.GetAttributeValue<int>("new_amount");
                            }
                        }
                    }
                }
                if (totalAmounte > MAX_AMOUNT)
                {
                    throw new InvalidPluginExecutionException($"Total anount is grater then: 200");
                }

            }

        }
        internal void PostUpdateAsync(new_license target, new_license preImage)
        {
            if (target.Contains("statuscode")
                && target.statuscode.Value == (int)LicenceStatus.Approved)
            {
                Queries queries = new Queries(service);

                new_systemsettings sendMail = queries.GetSystemSettingByName("SendEmails");
                if (sendMail.new_value == "1")
                {
                    new_email email = new new_email();
                    email.new_New_body = "רשיון מספר " + preImage.new_Number + "אושר";
                    email.new_licenseid = target.ToEntityReference();
                    email.new_subject = "אישור רשיון";


                    new_patient patient = service.Retrieve(preImage.new_patientid.LogicalName, preImage.new_patientid.Id
                        , new ColumnSet("emailaddress")).ToEntity<new_patient>();
                    email.new_to = patient.EmailAddress;
                    service.Create(email);
                }
            }
        }

    }
}
