using ExamenSergioRomeoGNB.Models;
using System.Linq;

namespace ExamenSergioRomeoGNB.ServiceRequests
{
    public interface ITransactionService
    {
        IQueryable<Transaction> GetAllTransactions();
        IQueryable<Transaction> GetTransactionsBySku(string productSku);
    }
}
