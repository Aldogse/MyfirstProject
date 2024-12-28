using BubberBreakFastAPI.Data;
using BubberBreakFastAPI.Interfaces;
using BubberBreakFastAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BubberBreakFastAPI.Repository
{
    public class BreakFastRepository : IBreakFastRepository
    {
        private readonly ApplicationDBContext _context;

        public BreakFastRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public  async Task<BreakFast> GetBreakFast(Guid id)
        {
            return await _context.BreakFasts.FirstOrDefaultAsync(breakfast => breakfast.id == id);
        }

        //Database calls


        public  List<BreakFast> GetBreakFasts()
        {
            return  _context.BreakFasts.ToList();
        }

    }
}
