using ExamenSergioRomeoGNB.AppConfig;
using ExamenSergioRomeoGNB.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using System.Linq;

namespace ExamenSergioRomeoGNB.ServiceRequests
{
    public class TransactionServiceFake : BaseService<Transaction>, ITransactionService
    {
        private readonly Logger Log;
        private readonly IQueryable<Transaction> _transactions;

        public TransactionServiceFake(LogFactory Factory)
        {
            Log = Factory.GetCurrentClassLogger();
            _transactions = JsonConvert.DeserializeObject<IQueryable<Transaction>>(@"{ 'sku':'F8017','amount':32.20,'currency':'CAD'},{ 'sku':'H6875','amount':19.00,'currency':'CAD'},{ 'sku':'M6913','amount':19.50,'currency':'AUD'},{ 'sku':'M6913','amount':21.50,'currency':'CAD'},{ 'sku':'O2266','amount':17.50,'currency':'CAD'},{ 'sku':'T6935','amount':32.40,'currency':'USD'},{ 'sku':'O2266','amount':16.00,'currency':'CAD'},{ 'sku':'N2555','amount':20.80,'currency':'EUR'},{ 'sku':'Y0036','amount':22.90,'currency':'USD'},{ 'sku':'N2555','amount':16.20,'currency':'AUD'},{ 'sku':'H6875','amount':20.60,'currency':'USD'},{ 'sku':'H6875','amount':32.30,'currency':'CAD'},{ 'sku':'I6321','amount':31.00,'currency':'EUR'},{ 'sku':'B4356','amount':20.40,'currency':'EUR'},{ 'sku':'N2555','amount':30.10,'currency':'AUD'},{ 'sku':'G4643','amount':27.50,'currency':'USD'},{ 'sku':'M6913','amount':32.40,'currency':'USD'},{ 'sku':'N2555','amount':15.60,'currency':'USD'},{ 'sku':'G4643','amount':22.90,'currency':'USD'},{ 'sku':'K2440','amount':22.40,'currency':'USD'}");
        }

        public IQueryable<Transaction> GetAllTransactions()
        {
            Log.Info("Obteniendo transacciones desde el webservice fake.");
            return _transactions;
        }

        public IQueryable<Transaction> GetTransactionsBySku(string productSku)
        {
            return _transactions.Where(x => x.Sku.Equals(productSku));
        }
    }
}
