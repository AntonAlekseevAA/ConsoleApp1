using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferLib
{
    public class Account
    {
        public Account(decimal amount, int id)
        {
            m_amount = amount;
            Id = id;
        }

        public decimal Amount => m_amount;

        private decimal m_amount = 0.0M;

        public object m_balanceLock = new object();
        public int Id { get; set; } // Unique bank account ID

        public void Deposit(decimal amount)
        {
            lock (m_balanceLock)
            {
                m_amount += amount;
            }
        }

        public void Withdraw(decimal amount)
        {
            lock (m_balanceLock)
            {
                if (m_amount < amount)
                {
                    throw new Exception("Insufficient funds");
                }

                m_amount -= amount;
            }
        }
    }
}