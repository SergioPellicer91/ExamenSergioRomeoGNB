using Microsoft.EntityFrameworkCore;

namespace ExamenSergioRomeoGNB.Models
{
    public static class Extensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}
