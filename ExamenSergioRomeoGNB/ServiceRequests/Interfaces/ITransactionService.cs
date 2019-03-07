using ExamenSergioRomeoGNB.Models;
using System.Collections.Generic;

namespace ExamenSergioRomeoGNB.ServiceRequests
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetAllTransactions();
        IEnumerable<Transaction> GetTransactionsBySku(string productSku);
    }
}
