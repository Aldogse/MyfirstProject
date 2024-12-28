using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubberBreakFast.Contracts.Contracts
{
    public record CreateBreakFastRequest(
        
        string Name,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        string tasteOfOrigin
    );
    
    
}
