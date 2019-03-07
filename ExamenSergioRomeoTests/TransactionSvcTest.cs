using ExamenSergioRomeoGNB.ServiceRequests;
using NLog;
using System.Linq;
using Xunit;

namespace ExamenSergioRomeoTests
{
    public class TransactionSvcTest
    {
        private readonly TransactionServiceFake _service;

        public TransactionSvcTest ()
        {
            _service = new TransactionServiceFake(new LogFactory());
        }

        [Fact]
        public void Get_Transactions()
        {
            //Act
            var rates = _service.GetAllTransactions();
            Assert.Equal("F8017", rates.FirstOrDefault().Sku);

        }
    }
}
