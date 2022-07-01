using CannabisPlugins.Common;
using CannabisPlugins.License;
using Microsoft.Xrm.Sdk;
using System;

namespace CannabisPlugins
{
    public class LicensePreUpdate : Plugin
    {
        public override void Execute()
        {

            Entity target = (Entity)context.InputParameters["Target"];
            Entity preImage = context.PreEntityImages["PreImage"];

            LicenceBL bl = new LicenceBL(service, trace);
            bl.PreUpdate(target, preImage);
            
        }
    }
}




