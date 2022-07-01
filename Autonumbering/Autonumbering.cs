using CannabisPlugins.Common;
using Microsoft.Xrm.Sdk;


namespace CannabisPlugins.Autonumbering
{
    public class Autonumbering : Plugin
    {
        public override void Execute()
        {
            Entity target = (Entity)context.InputParameters["Target"];

            AutonumberingBL bl = new AutonumberingBL( service,trace);

            bl.Execute(target);

            // Query the db
          


            //     LicenceBL licenceBL = new LicenceBL();
            //     licenceBL.PreUpdate(target, preImage);
        }
    }
}
