using CannabisPlugins.Common;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisPlugins.Doctor
{
    public class DoctorPreCreate: Plugin
    {
        public override void Execute()
        {

            Entity target = (Entity)context.InputParameters["Target"];
            DoctorBL bl = new DoctorBL(service, trace);
            bl.PreCreate(target);
        }
    }
}
