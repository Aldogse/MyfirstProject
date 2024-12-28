using BubberBreakFastAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BubberBreakFastAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions <ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<BreakFast> BreakFasts { get; set; }


    }
}
