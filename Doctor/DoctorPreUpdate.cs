using CannabisPlugins.Common;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisPlugins.Doctor
{
    public class DoctorPreUpdate: Plugin
    {
        public override void Execute()
        {

            Entity target = (Entity)context.InputParameters["Target"];
            Entity preImage = context.PreEntityImages["PreImage"];

            DoctorBL bl = new DoctorBL(service, trace);
            bl.PreUpdate(target, target);

        }
    }
}
