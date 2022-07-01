using CannabisPlugins.Common;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisPlugins.License
{
    public class LicensePostUpdateAsync:Plugin
    {


        public override void Execute()
        {
            new_license target =((Entity)context.InputParameters["Target"]).ToEntity<new_license>();

            new_license preImage =((Entity)context.PreEntityImages["PreImage"]).ToEntity<new_license>(); 
            trace.Trace("in LicensePostUpdateAsync");
            var bl = new LicenceBL(service, trace);
            bl.PostUpdateAsync(target, preImage);         
        }
    }
}
