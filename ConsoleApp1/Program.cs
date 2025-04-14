namespace ConsoleApp1
{
    internal class ProgramTest
    {
        static async Task Main(string[] args)
        {
            var transferHepler = new TransferHelper();

            var accountOne = new Account(1000);
            var accountTwo = new Account(250);

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
            Console.WriteLine($@"Balance of account A is {accountOne.Amount}, balance of account B is {accountTwo.Amount}");
            Console.WriteLine($@"Balance of account A is 0: {accountOne.Amount == 0}, balance of account B is 1250: {accountTwo.Amount == 1250}");
            Console.ReadKey();
        }
    }
}
