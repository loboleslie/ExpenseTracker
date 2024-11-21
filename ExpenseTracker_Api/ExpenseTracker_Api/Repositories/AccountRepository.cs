using ExpenseTracker_Api.Data;
using ExpenseTracker_Api.Interface;
using ExpenseTracker_Api.Interface.Repositories;
using ExpenseTracker_Api.Interface.Services;
using ExpenseTracker_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker_Api.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ExpenseTrackerDbContext _context;

        public AccountRepository(ExpenseTrackerDbContext context)
        {
            _context = context;
        }

        public Account Add(Account account)
        {
            _context.Add(account);
            _context.SaveChanges();
            return account;
        }
       

        public Account Delete(int accountId)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Id == accountId);
            _context.Remove(account);
            _context.SaveChanges();
            return account;
        }

        public Account? Get(int accountId)
        {
            return _context.Accounts.FirstOrDefault(x => x.Id == accountId);
        }

        public List<Account> GetAll()
        {
            return _context.Accounts.ToList();
        }

        public Account GetByName(string accountName)
        {
            return _context.Accounts.FirstOrDefault(x => x.Name == accountName);
        }

        public Account Update(Account account)
        {
            _context.Update(account);
            _context.SaveChanges();
            return account;
        }
    }
}
