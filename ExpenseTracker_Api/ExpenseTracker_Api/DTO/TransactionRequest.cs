using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker_Api.DTO
{
    public class TransactionRequest
    {

        public int Id { get; set; }
        
        public string? Description { get; set; }

        public int AccountId { get; set; }
        
        public DateTimeOffset? TransactionDate { get; set; }
    }
}
