using System.Linq;
using VendingMachine;

namespace VendingMachine.Tests
{
    public class UnitTest1
    {
        VMManager sut = new VMManager();

        [Fact]
        public void ProductsListSize()
        {
            Assert.Equal(9, sut.Products.Count);
        }

        [Fact]
        public void DenomCheck()
        {
            Assert.Equal(1, sut.denomination[0]);
            Assert.Equal(5, sut.denomination[1]);
            Assert.Equal(10, sut.denomination[2]);
            Assert.Equal(20, sut.denomination[3]);
            Assert.Equal(50, sut.denomination[4]);
            Assert.Equal(100, sut.denomination[5]);
            Assert.Equal(500, sut.denomination[6]);
            Assert.Equal(1000, sut.denomination[7]);
        }


        [Fact]
        public void BillCoinAmountTest()
        {
            sut.availableFunds = 2999;
            int[] billcoinAmount = sut.EndTransaction();
            Assert.Equal(2, billcoinAmount[7]);
            Assert.Equal(1, billcoinAmount[6]);
            Assert.Equal(4, billcoinAmount[5]);
            Assert.Equal(1, billcoinAmount[4]);
            Assert.Equal(2, billcoinAmount[3]);
            Assert.Equal(0, billcoinAmount[2]);
            Assert.Equal(1, billcoinAmount[1]);
            Assert.Equal(4, billcoinAmount[0]);
        }

        [Fact]
        public void ZeroFundsTest()
        {
            sut.availableFunds = 2999;
            sut.EndTransaction();
            Assert.Equal(0, sut.availableFunds);
        }
    }
}