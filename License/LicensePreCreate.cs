using CannabisPlugins.Common;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisPlugins.License
{
   public class LicensePreCreate:Plugin
    {
        public override void Execute()
        {
          
    
            Entity target = (Entity)context.InputParameters["Target"];
            LicenceBL bl = new LicenceBL(service, trace);
            bl.PreCreate(target);
       
        }
       
    }
}
