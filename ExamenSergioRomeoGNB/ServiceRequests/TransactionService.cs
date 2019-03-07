using ExamenSergioRomeoGNB.AppConfig;
using ExamenSergioRomeoGNB.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace ExamenSergioRomeoGNB.ServiceRequests
{
    public class TransactionService : BaseService<Transaction>, ITransactionService
    {
        private readonly string url;

        public TransactionService(IOptions<UrlConfig> config)
        {
            url = config.Value.TransactionsUrl;
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return base.GetAll(url);
        }

        public IEnumerable<Transaction> GetTransactionsBySku(string productSku)
        {
            IEnumerable<Transaction> all = base.GetAll(url);
            return all.Where(x => x.Sku.Equals(productSku));
        }
    }
}
