using Microsoft.EntityFrameworkCore;

namespace GNBSergioRP.Models
{
    public class GnbContext : DbContext
    {
        public GnbContext(DbContextOptions<GnbContext> options)
            : base(options)
        {

        }

        public DbSet<Rate> Rates { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

    }
}
