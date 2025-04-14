using TransferLib;

namespace TransferHelperTests
{
    public class TransferHelperTests
    {
        [Fact]
        public void Transfer_ShouldTransferExactAmountInManyThreads()
        {
            var transferHepler = new TransferHelper();

            var accountOne = new Account(1000, 1);
            var accountTwo = new Account(250, 2);

            const int threadsCount = 100;
            var threads = new List<Thread>();

            for (var i = 0; i < threadsCount; i++)
            {
                var thread = new Thread(() =>
                {
                    Thread.Sleep(1000);
                    transferHepler.Transfer(accountOne, accountTwo, 10);
                });
                thread.IsBackground = true;

                threads.Add(thread);
            }

            threads.ForEach(thread => thread.Start());
            threads.ForEach(thread => thread.Join());
            Assert.Equal(0, accountOne.Amount);
            Assert.Equal(1250, accountTwo.Amount);
        }
    }
}