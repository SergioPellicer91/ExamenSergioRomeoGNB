using ExamenSergioRomeoGNB.Models;
using System.Linq;

namespace ExamenSergioRomeoGNB.ServiceRequests
{
    public interface IRateService
    {
        IQueryable<Rate> GetAllRates();
    }
}