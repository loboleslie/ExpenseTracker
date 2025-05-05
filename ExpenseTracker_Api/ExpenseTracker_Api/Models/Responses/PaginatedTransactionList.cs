using System;

namespace ExpenseTracker_Api.Models.Responses;

public class PaginatedTransactionList
{
    public List<Transaction>? Transactions { get; set;}
    public int TotalCount { get; set;}
}
