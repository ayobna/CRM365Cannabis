using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannabisPlugins.Models
{
    internal enum StateCode { 
    Active=0,
    InActive=1
    }

  internal enum LicenceStatus
    {
        Approved= 100000002

    }
    internal enum RequestStatus
    {
        New = 1,
        InTreatment = 2,
        EndOfTreatment = 3
    }   
}
