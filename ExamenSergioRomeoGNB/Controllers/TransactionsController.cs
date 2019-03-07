using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ExamenSergioRomeoGNB.Models;
using ExamenSergioRomeoGNB.Repositories;
using ExamenSergioRomeoGNB.ServiceRequests;
using System.Linq;
using ExamenSergioRomeoGNB.Lib;

namespace ExamenSergioRomeoGNB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IRepository<Transaction> rep;
        private readonly ITransactionService transactionsvc;

        public TransactionsController(IRepository<Transaction> TransactionsRepository, ITransactionService TransactionService)
        {
            this.rep = TransactionsRepository;
            this.transactionsvc = TransactionService;
        }


        // GET: api/Transactions/GetAllTransactions
        [HttpGet("GetTransactions")]
        public IEnumerable<Transaction> GetTransactions()
        {
            return transactionsvc.GetAllTransactions();
        }


        [HttpGet("GetTransactions/GetLocal")]
        public IEnumerable<Transaction> GetLocal()
        {
            return rep.GetAll();
        }

        [HttpGet("GetTransactions/SaveLocal")]
        public IEnumerable<Transaction> SaveLocal()
        {
            IEnumerable<Transaction> ServiceTransactions = transactionsvc.GetAllTransactions();
            rep.DeleteAll();
            var res = rep.CreateMultiple(ServiceTransactions);
            return rep.GetAll();
        }

        [HttpGet("GetTransactionsBySku/GetLocal/{sku}")]
        public IQueryable<Transaction> GetLocalTransactionsBySku([FromRoute] string sku)
        {
            return rep.GetAllByField("Sku", sku);
        }

        [HttpGet("GetTransactionsBySku/{sku}")]
        public IEnumerable<Transaction> GetTransactionsBySku([FromRoute] string sku)
        {
            return transactionsvc.GetTransactionsBySku(sku);
        }


    }
}