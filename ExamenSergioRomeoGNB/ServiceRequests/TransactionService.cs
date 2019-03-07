using ExamenSergioRomeoGNB.AppConfig;
using ExamenSergioRomeoGNB.Models;
using Microsoft.Extensions.Options;
using NLog;
using System.Linq;

namespace ExamenSergioRomeoGNB.ServiceRequests
{
    public class TransactionService : BaseService<Transaction>, ITransactionService
    {
        private readonly string url;
        private readonly Logger Log;

        public TransactionService(IOptions<UrlConfig> config, LogFactory Factory)
        {
            url = config.Value.TransactionsUrl;
            Log = Factory.GetCurrentClassLogger();
        }

        public IQueryable<Transaction> GetAllTransactions()
        {
            Log.Info("Obteniendo transacciones desde el webservice.");
            return base.GetAll(url,Log);
        }

        public IQueryable<Transaction> GetTransactionsBySku(string productSku)
        {
            IQueryable<Transaction> all = base.GetAll(url,Log);
            return all.Where(x => x.Sku.Equals(productSku));
        }
    }
}
