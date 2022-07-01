using CannabisPlugins.Common;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisPlugins.subjects
{
    public class SubjectsPreCreatee : Plugin
    {
        public override void Execute()
        {
            new_subjects target = ((Entity)context.InputParameters["Target"]).ToEntity<new_subjects>();
            SubjectsBL bl = new SubjectsBL(service, trace);
            bl.PreCreate(target);
        }
    }
}
