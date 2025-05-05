using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker_Api.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        public int AccountId { get; set;}
        
        public DateTimeOffset TransactionDate { get; set; }

        public Account Account { get; set; }
        public decimal Amount { get; set; }
    }
}
