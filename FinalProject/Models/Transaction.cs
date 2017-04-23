using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class Transaction
    {
        public string TransactionId { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        public DateTime Inserted { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public uint UserId { get; set; }
        public User User { get; set; }
    }

    public enum TransactionType
    {
        Withdrawal = 0,
        Deposit = 1
    }
}
