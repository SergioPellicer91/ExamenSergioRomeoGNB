using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ExamenSergioRomeoGNB.Models;
using ExamenSergioRomeoGNB.ServiceRequests;
using ExamenSergioRomeoGNB.Repositories;
using System.Linq;

namespace ExamenSergioRomeoGNB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IRepository<Rate> rep;
        private readonly IRateService ratesvc;

        public RatesController(IRepository<Rate> RatesRepository, IRateService RateService)
        {
            this.rep = RatesRepository;
            this.ratesvc = RateService;
        }

        // GET: api/Rates/GetAllRates
        [HttpGet("GetRates")]
        public IEnumerable<Rate> GetRates()
        {
            return ratesvc.GetAllRates();
        }

        [HttpGet("GetRates/GetLocal")]
        public IQueryable<Rate> GetLocal()
        {
            return rep.GetAll();
        }

        [HttpGet("GetRates/SaveLocal")]
        public IEnumerable<Rate> SaveLocal()
        {
            IEnumerable<Rate> ServiceRates = ratesvc.GetAllRates();
            rep.DeleteAll();
            var res = rep.CreateMultiple(ServiceRates);
            return rep.GetAll();
        }


    }
}