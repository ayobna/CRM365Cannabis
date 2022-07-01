using CannabisPlugins.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;


namespace CannabisPlugins.Common
{
    public class Queries
    {

        IOrganizationService service;

        public Queries(IOrganizationService service)
        {
            this.service = service;

        }



        internal DataCollection<Entity> GetLicenseByPatient( Guid patientId)
        {
            QueryExpression query = new QueryExpression("new_license");
            query.ColumnSet = new ColumnSet("new_amount", "statuscode");
            query.Criteria = new FilterExpression();
            query.Criteria.AddCondition("new_patientid", ConditionOperator.Equal, patientId);

            EntityCollection License = service.RetrieveMultiple(query);

            return License.Entities;


        }

        internal DataCollection<Entity> CheckDoctorUser(Guid usertId)
        {
            QueryExpression query = new QueryExpression("new_doctor");

            query.ColumnSet = new ColumnSet("new_name");
            query.Criteria = new FilterExpression();

            query.Criteria.AddCondition("new_systemuserid", ConditionOperator.Equal, usertId);
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, (int)StateCode.Active);

            EntityCollection CheckUser = service.RetrieveMultiple(query);
            return CheckUser.Entities;
        }

        internal DataCollection<Entity> GetEntityByIdAndEntityNameAndKeyAndFildsName(Guid id,string logicalName, string EntityKey, string fildsName)
        {
            QueryExpression query = new QueryExpression(logicalName);
            query.ColumnSet = new ColumnSet(fildsName);
            query.Criteria = new FilterExpression();
            query.Criteria.AddCondition(EntityKey, ConditionOperator.Equal, id);
            EntityCollection subjects = service.RetrieveMultiple(query);
            return subjects.Entities;
        }
     
        internal EntityCollection getNamesInByLogicalName(string name,string logicalName)
        {
            QueryExpression query = new QueryExpression(logicalName);
            query.ColumnSet = new ColumnSet(true);
            query.Criteria = new FilterExpression();
            query.Criteria.AddCondition("new_name", ConditionOperator.Equal, name);

            EntityCollection CheckName = service.RetrieveMultiple(query);
            return CheckName;

        }

        internal Entity GetAutonumberEntityName(string logicalName)
        {
            Entity res = null;

            QueryExpression query = new QueryExpression("new_autonumbering");

            query.ColumnSet = new ColumnSet(true);
            query.Criteria = new FilterExpression();
            query.Criteria.AddCondition("new_entity", ConditionOperator.Equal, logicalName);


            var autonumbers = service.RetrieveMultiple(query);
            if (autonumbers.Entities.Count == 1)
            {
                res = autonumbers.Entities[0];
            }
            return res;
        }

        internal new_systemsettings GetSystemSettingByName(string keyname)
        {
            using (ServiceContext svcContext=new ServiceContext(service))
            {
                var res = from s in svcContext.new_systemsettingsSet
                          where s.new_name == keyname
                          select s;
                return res.FirstOrDefault();
            }
        }
    }
}