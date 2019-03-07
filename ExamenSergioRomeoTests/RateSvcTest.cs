using ExamenSergioRomeoGNB.ServiceRequests;
using NLog;
using System.Linq;
using Xunit;

namespace ExamenSergioRomeoTests
{
    public class RateSvcTest
    {
        private readonly RateServiceFake _service;

        public RateSvcTest()
        {
            _service = new RateServiceFake(new LogFactory());
        }

        [Fact]
        public void Get_Rates()
        {
            //Act
            var rates = _service.GetAllRates();
            Assert.Equal("AUD", rates.FirstOrDefault().From);

        }
    }
}
