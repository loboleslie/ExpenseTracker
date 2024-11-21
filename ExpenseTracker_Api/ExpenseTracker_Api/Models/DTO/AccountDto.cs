using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExpenseTracker_Api.Models.DTO;

public class AccountDto
{
    [MaxLength(255)] public string? Name { get; set; }
}