namespace ExpenseTracker_Api.Models
{
    public class AccountPayee
    {
        public int AccountId { get; set; }
        public required Account Account { get; set; }
        public int PayeeId { get; set; }
        public required Payee Payee { get; set; }
    }
}

