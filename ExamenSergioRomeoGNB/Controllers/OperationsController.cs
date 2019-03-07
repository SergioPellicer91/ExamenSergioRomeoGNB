using ExamenSergioRomeoGNB.Lib;
using ExamenSergioRomeoGNB.Models;
using ExamenSergioRomeoGNB.Repositories;
using ExamenSergioRomeoGNB.ServiceRequests;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExamenSergioRomeoGNB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IRepository<Transaction> transactionRep;
        private readonly IRepository<Rate> rateRep;
        private readonly ITransactionService transactionsvc;
        private readonly IRateService ratesvc;

        public OperationsController(IRepository<Transaction> TransactionsRepository, ITransactionService TransactionService, IRepository<Rate> RatesRepository, IRateService RateService)
        {
            this.transactionRep = TransactionsRepository;
            this.rateRep = RatesRepository;
            this.transactionsvc = TransactionService;
            this.ratesvc = RateService;
        }


        [HttpGet("GetTransactionsInEurSku/{sku}")]
        public IQueryable<Transaction> GetTransactionsInEur([FromRoute] string sku)
        {
            IQueryable<Transaction> trs = transactionRep.GetAllByField("Sku", sku);
            IQueryable<Rate> rts = rateRep.GetAll();
            trs = TransactionConverter.CalculateAlgorithm(rts, trs, "EUR");
            return trs;
        }
    }
}


