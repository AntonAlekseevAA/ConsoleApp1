using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Account
    {
        public Account(decimal amount)
        {
            m_amount = amount;
        }

        public decimal Amount => m_amount;

        private decimal m_amount = 0.0M;

        public object m_balanceLock = new object();
        public int Id { get; set; } // Unique bank account ID

        public void Deposit(decimal delta)
        {
            lock (m_balanceLock)
            {
                m_amount += delta;
            }
        }

        public void Withdraw(decimal delta)
        {
            lock(m_balanceLock)
            {
                if (m_amount < delta)
                {
                    throw new Exception("Insufficient funds");
                }

                m_amount -= delta;
            }
        }
    }
}