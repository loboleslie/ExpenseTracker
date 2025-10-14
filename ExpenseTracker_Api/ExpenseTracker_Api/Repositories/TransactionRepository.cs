using ExpenseTracker_Api.Data;
using ExpenseTracker_Api.Interface.Repositories;
using ExpenseTracker_Api.Models;
using ExpenseTracker_Api.Models.DTO;
using ExpenseTracker_Api.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker_Api.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ExpenseTrackerDbContext context) : base(context)
        {
        }

        public Transaction? GetTransaction(TransactionDto transactionDto)
        {
            return GetQueryable()
                .FirstOrDefault(x => x.Description == transactionDto.Description
                    && x.TransactionDate == transactionDto.TransactionDate
                    && x.Amount == transactionDto.Amount);
        }

        public PaginatedTransactionList GetAll(int pageNumber, int pageSize, string searchTerm, 
            DateTimeOffset? fromDate, DateTimeOffset? toDate, int accountId)
        {
            var query = GetQueryable();

            // Apply date range filter
            if (fromDate.HasValue && toDate.HasValue)
            {
                query = query.Where(i => i.TransactionDate >= fromDate.Value.UtcDateTime
                    && i.TransactionDate < toDate.Value.UtcDateTime);
            }

            // Apply account filter if specified
            if (accountId != 0)
            {
                query = query.Where(i => i.AccountId == accountId);
            }

            // Apply search term filter if provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(i => i.Description.Contains(searchTerm));
            }

            // Get total count for pagination
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Apply pagination and ordering
            var transactions = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(s => s.Description)
                .ToList();

            return new PaginatedTransactionList
            {
                Transactions = transactions,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }
    }
}
