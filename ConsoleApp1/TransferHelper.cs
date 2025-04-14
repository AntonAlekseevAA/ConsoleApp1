using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class TransferHelper
    {
        // private readonly object _locker = new object();

        public void Transfer(Account a, Account b, decimal amount)
        {
            if (a.Id < b.Id)
            {
                Monitor.Enter(a.m_balanceLock); // A first
                Monitor.Enter(b.m_balanceLock); // then B
            }
            else
            {
                Monitor.Enter(b.m_balanceLock); // B first
                Monitor.Enter(a.m_balanceLock); // then A
            }

            try
            {
                a.Withdraw(amount);
                b.Deposit(amount);
            }
            finally
            {
                Monitor.Exit(a.m_balanceLock);
                Monitor.Exit(b.m_balanceLock);
            }


            /*lock (_locker)
            {
                
                 a.Amount -= amount;
                 b.Amount += amount;
                 // Console.WriteLine($@"Balance of account A is {a.Amount}, balance of account B is {b.Amount}");
            }*/
        }
    }
}
