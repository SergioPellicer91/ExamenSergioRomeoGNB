using ExamenSergioRomeoGNB.Models;
using System.Collections.Generic;

namespace ExamenSergioRomeoGNB.ServiceRequests
{
    public interface IRateService
    {
        IEnumerable<Rate> GetAllRates();
    }
}