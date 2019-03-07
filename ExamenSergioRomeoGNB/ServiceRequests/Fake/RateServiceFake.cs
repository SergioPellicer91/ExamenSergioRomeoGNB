using ExamenSergioRomeoGNB.Models;
using Newtonsoft.Json;
using NLog;
using System.Linq;

namespace ExamenSergioRomeoGNB.ServiceRequests
{
    public class RateServiceFake : BaseService<Rate>, IRateService
    {
        private readonly Logger Log;
        private readonly IQueryable<Rate> _rates;

        public RateServiceFake(LogFactory Factory)
        {
            Log = Factory.GetCurrentClassLogger();
            _rates = JsonConvert.DeserializeObject<IQueryable<Rate>>(@"{'from':'AUD','to':'USD','rate':0.58},{'from':'USD','to':'AUD','rate':1.72},{'from':'AUD','to':'CAD','rate':1.03},{'from':'CAD','to':'AUD','rate':0.97},{'from':'USD','to':'EUR','rate':0.83},{'from':'EUR','to':'USD','rate':1.20}");
        }

        public IQueryable<Rate> GetAllRates()
        {
            Log.Info("Obteniendo cambios desde el WebService Fake.");
            return _rates;
        }
    }
}
