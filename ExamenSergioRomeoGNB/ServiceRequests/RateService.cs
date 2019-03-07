using ExamenSergioRomeoGNB.AppConfig;
using ExamenSergioRomeoGNB.Models;
using Microsoft.Extensions.Options;
using NLog;
using System.Linq;

namespace ExamenSergioRomeoGNB.ServiceRequests
{
    public class RateService : BaseService<Rate>, IRateService
    {
        private readonly string url;
        private readonly Logger Log;

        public RateService(IOptions<UrlConfig> config, LogFactory Factory)
        {
            url = config.Value.RatesUrl;
            Log = Factory.GetCurrentClassLogger();
        }

        public IQueryable<Rate> GetAllRates()
        {
            Log.Info("Obteniendo cambios desde el WebService.");
            return base.GetAll(url,Log);
        }
    }
}
