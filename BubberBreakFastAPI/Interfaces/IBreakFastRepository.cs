using BubberBreakFastAPI.Models;

namespace BubberBreakFastAPI.Interfaces
{
    public interface IBreakFastRepository
    {
        List<BreakFast>GetBreakFasts();
        Task<BreakFast> GetBreakFast(Guid id);
        
    }
}
