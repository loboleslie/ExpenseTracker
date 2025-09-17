using ExpenseTracker_Api.Data;
using ExpenseTracker_Api.Interface.Repositories;
using ExpenseTracker_Api.Models;
using ExpenseTracker_Api.Models.DTO;
using ExpenseTracker_Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker_Api.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ExpenseTrackerDbContext _context;

        public TransactionRepository(ExpenseTrackerDbContext context)
        {
            _context = context;

        }

        public Transaction Add(Transaction transaction)
        {
            _context.Add(transaction);
            _context.SaveChanges();
            return transaction;
        }

        public int Delete(int transactionId)
        {
            var transaction = _context.Transactions.FirstOrDefault(x => x.Id == transactionId);
            _context.Remove(transaction);
            _context.SaveChanges();
            return transactionId;
        }

        public Transaction? Get(int transactionId)
        {
            return _context.Transactions.FirstOrDefault(x => x.Id == transactionId);
        }

        public PaginatedTransactionList GetAll(int pageNumber, 
        int pageSize, string searchTerm, DateTimeOffset? fromDate, 
        DateTimeOffset? toDate, int accountId)
        {
           
                
            List<Transaction> _transactionList;
            int totalPages = 0;
            int totalCount = 0;
            
            if (accountId == 0)
            {
                                      
                totalCount = _context.Transactions
                                     .Where(i => i.TransactionDate >= fromDate.Value.UtcDateTime
                                      && i.TransactionDate < toDate.Value.UtcDateTime).Count();

               
                totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                return new PaginatedTransactionList()
                {
                    Transactions = _context.Transactions
                                           .Where(i => (string.IsNullOrEmpty(searchTerm) ||
                                                i.Description.Contains(searchTerm))
                                                 && i.TransactionDate >= fromDate.Value.UtcDateTime
                                      && i.TransactionDate < toDate.Value.UtcDateTime)
                                            .Take(pageSize)
                                            .OrderBy((s) => s.Description)
                                            .ToList()
                                            ,
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                };
            }
            else
            {
                    totalPages = _context.Transactions
                                         .Where(i => i.TransactionDate <= toDate.Value.UtcDateTime && 
                                                i.AccountId == accountId).Count();
                    
                    totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                return new PaginatedTransactionList()
                {
                    Transactions = _context.Transactions
                                           .Where(i => string.IsNullOrEmpty(searchTerm) ||
                                                i.Description.Contains(searchTerm) &&
                                                i.TransactionDate >= fromDate.Value.UtcDateTime && 
                                                i.TransactionDate < toDate.Value.UtcDateTime && 
                                                i.AccountId == accountId)
                                            .Skip((pageNumber - 1) * pageSize)
                                            .Take(pageSize)
                                            .OrderBy((s) => s.Description)
                                            .ToList(),
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                };

            }

        }


        public Transaction Update(Transaction transaction)
        {
            _context.Update(transaction);
            _context.SaveChanges();
            return transaction;
        }

        public Transaction? GetTransaction(TransactionDto transactionDto)
        {
            return _context.Transactions.FirstOrDefault(x => x.Description == transactionDto.Description
                                                             && x.TransactionDate == transactionDto.TransactionDate
                                                             && x.Amount == transactionDto.Amount);
        }
    }
}
