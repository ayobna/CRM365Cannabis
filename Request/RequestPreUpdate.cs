using CannabisPlugins.Common;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisPlugins.Request
{
 public   class RequestPreUpdate: Plugin
    {
        public override void Execute()
        {
            new_request target = ((Entity)context.InputParameters["Target"]).ToEntity<new_request>();
            Entity preImage = context.PreEntityImages["PreImage"];
            RequestBL bl = new RequestBL(service, trace);
            bl.PreUpdate(target, preImage.ToEntity<new_request>());
        }
    }
}
