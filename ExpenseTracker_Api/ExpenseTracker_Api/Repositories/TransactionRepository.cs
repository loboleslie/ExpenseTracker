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
            DateTimeOffset _fromDate;
            DateTimeOffset _toDate = DateTimeOffset.UtcNow;
            var _accountId = 0;
            List<Transaction> _transactionList;
            int totalPages = 0;

            if (fromDate == null)
                _fromDate = DateTimeOffset.UtcNow - TimeSpan.FromHours(24);
            

            if (toDate == null)
                _toDate = DateTimeOffset.UtcNow;

            if (accountId == 0)
            {
                totalPages = _context.Transactions
                                     .Where(i => i.TransactionDate >= fromDate
                                      && i.TransactionDate <= toDate).Count();

                return new PaginatedTransactionList()
                {
                    Transactions = _context.Transactions
                                           .Where(i => string.IsNullOrEmpty(searchTerm) ||
                                                  i.Description.Contains(searchTerm) &&
                                                  i.TransactionDate >= fromDate
                                                  && i.TransactionDate <= toDate)
                                            .Skip((pageNumber - 1) * pageSize)
                                            .Take(pageSize)
                                            .OrderBy((s) => s.Description)
                                            .ToList(),
                    TotalCount = totalPages
                };
            }
            else
            {
                    totalPages = _context.Transactions
                                         .Where(i => i.TransactionDate <= toDate && 
                                                i.AccountId == accountId).Count();

                return new PaginatedTransactionList()
                {
                    Transactions = _context.Transactions
                                           .Where(i => string.IsNullOrEmpty(searchTerm) ||
                                                i.Description.Contains(searchTerm) &&
                                                i.TransactionDate >= fromDate && 
                                                i.TransactionDate <= toDate && 
                                                i.AccountId == accountId)
                                            .Skip((pageNumber - 1) * pageSize)
                                            .Take(pageSize)
                                            .OrderBy((s) => s.Description)
                                            .ToList(),
                    TotalCount = totalPages
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
