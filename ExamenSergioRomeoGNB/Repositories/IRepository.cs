using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenSergioRomeoGNB.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAllByField(string field, string value);
        T GetSingle(int ID);
        Task<T> GetSingleAsync(int ID);
        int Create(T Entity);
        int CreateMultiple(IEnumerable<T> Entities);
        bool Update(T Entity);
        bool Delete(T Entity);
        bool DeleteAll();
        int Commit();
        Task CommitAsync();
    }
}


