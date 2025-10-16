namespace ExpenseTracker_Api.Models
{
    public class Payee
    {
        public int Id { get; set; }
        public string PayeeName { get; set; } = "";
        public ICollection<AccountPayee>? AccountPayees { get; set; }
    }
}

