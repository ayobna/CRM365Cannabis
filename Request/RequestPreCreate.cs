using CannabisPlugins.Common;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisPlugins.Request
{
    public class RequestPreCreate : Plugin
    {
        public override void Execute()
        {
            new_request target = ((Entity)context.InputParameters["Target"]).ToEntity<new_request>();
             RequestBL bl = new RequestBL(service, trace);
            bl.PreCreate(target);
            
        }

    }
}
