using ExpenseTracker_Api.Data;
using ExpenseTracker_Api.Interface.Repositories;
using ExpenseTracker_Api.Models;
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

        public Transaction AddTransaction(Transaction transaction)
        {
            _context.Add(transaction);
            _context.SaveChanges();
            return transaction;
        }

        public int DeleteTransaction(int transactionId)
        {
            var transaction = _context.Transactions.FirstOrDefault(x => x.Id == transactionId);
            _context.Remove(transaction);
            _context.SaveChanges();
            return transactionId;
        }

        public Transaction? GetTransaction(int transactionId)
        {
            return _context.Transactions.FirstOrDefault(x => x.Id == transactionId);
        }


        public Transaction UpdateTransaction(Transaction transaction)
        {
             _context.Update(transaction);
            _context.SaveChanges();
            return transaction;
        }
    }
}
