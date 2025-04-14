using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class TransferHelper
    {
        private readonly object _locker = new object();

        public void Transfer(Account a, Account b, decimal amount)
        {
            lock (_locker)
            {
                a.Amount -= amount;
                b.Amount += amount;
                // Console.WriteLine($@"Balance of account A is {a.Amount}, balance of account B is {b.Amount}");
            }
        }
    }
}
