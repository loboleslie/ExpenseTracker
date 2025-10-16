using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker_Api.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }

        public ICollection<AccountPayee>? AccountPayees { get; set; }
    }
}
