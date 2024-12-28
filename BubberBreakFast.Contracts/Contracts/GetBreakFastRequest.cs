using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubberBreakFast.Contracts.Contracts
{
    public record GetBreakFastRequest(

        Guid id,
        string Name,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        DateTime LastModifiedDate
    );
    
    
}
