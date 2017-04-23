using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using FinalProject.Helpers;

namespace FinalProject.Models
{
    public class User
    {
        [Key]
        public uint UserId { get; set; }

        [Required]
        [Range(0, 9999)]
        public uint Pin { get; set; }

        public decimal Balance { get; set; }

        public List<Transaction> Transactions { get; set; }

        public async Task UpdateBalance()
        {
            using (var db = new UserContext())
            {
                Balance = (await db.Users.SingleAsync(u => u.UserId == UserId)).Balance;
            }
        }

        public async Task UpdateTransactions()
        {
            using (var db = new UserContext())
            {
                Transactions = await db.Transactions.Where(t => t.UserId == UserId).ToListAsync();
            }
        }

        public async Task<Result<Transaction>> Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                return new Result<Transaction>(null, false, "Amount must be greater than 0.00");
            }
            else if (amount > 1000)
            {
                return new Result<Transaction>(null, false, "Maximum withdrawal amount is $1000.00");
            }

            if (Balance < amount)
            {
                return new Result<Transaction>(null, false, "Insufficient funds");
            }

            using (var db = new UserContext())
            {
                var user = await db.Users.Include(u => u.Transactions).SingleAsync(u => u.UserId == UserId);

                var currentDate = DateTime.UtcNow.Date;
                var numTransactions = user.Transactions
                    .Where(t => t.UserId == UserId)
                    .Where(t => t.Type == TransactionType.Withdrawal)
                    .Where(t => t.Inserted.Date == currentDate)
                    .Count();

                if (numTransactions >= 10)
                {
                    return new Result<Transaction>(null, false, "Maximum of 10 withdrawals per day");
                }
                else
                {
                    var transaction = new Transaction
                    {
                        Type = TransactionType.Withdrawal,
                        Amount = amount,
                        Inserted = DateTime.UtcNow
                    };

                    user.Transactions.Add(transaction);

                    user.Balance -= amount;

                    await db.SaveChangesAsync();

                    return new Result<Transaction>(transaction, true, null);
                }
            }
        }
    }
}
