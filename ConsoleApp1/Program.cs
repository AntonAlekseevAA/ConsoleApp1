namespace ConsoleApp1
{
    internal class ProgramTest
    {
        static object locker = new object();
        static async Task Main(string[] args)
        {
            var transferHepler = new TransferHelper();

            var accountOne = new Account();
            accountOne.Amount = 1000;

            var accountTwo = new Account();
            accountTwo.Amount = 250;

            /*var tasks = new Task[100];

            for (var i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(async() =>
                {
                    transferHepler.Transfer(accountOne, accountTwo, 10);
                    Console.WriteLine($@"Balance of account A is {accountOne.Amount}, balance of account B is {accountTwo.Amount}");
                });
            }*/

            const int threadsCount = 100;
            var threads = new List<Thread>();

            for (var i = 0; i < threadsCount; i++)
            {
                int threadId = i;
                var thread = new Thread(() =>
                {
                    Thread.Sleep(1000);
                    lock (locker)
                    {
                        transferHepler.Transfer(accountOne, accountTwo, 10);
                        Console.WriteLine($@"Balance of account A is {accountOne.Amount}, balance of account B is {accountTwo.Amount}");
                    }
                });
                thread.IsBackground = true;

                threads.Add(thread);
                
                thread.IsBackground = true;
            }

            threads.ForEach(thread => thread.Start());

            Console.ReadKey();
        }
    }
}
