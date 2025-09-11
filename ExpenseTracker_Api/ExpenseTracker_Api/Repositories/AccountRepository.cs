using ExpenseTracker_Api.Data;
using ExpenseTracker_Api.Interface;
using ExpenseTracker_Api.Interface.Repositories;
using ExpenseTracker_Api.Interface.Services;
using ExpenseTracker_Api.Models;
using ExpenseTracker_Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public PaginatedAccountList GetAll(int pageNumber, int pageSize, string searchTerm = "")
        {
            var totalCount = _context.Accounts.Where(i => string.IsNullOrEmpty(searchTerm) || i.Name.Contains(searchTerm)).Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            
            return new PaginatedAccountList()
            {
                Accounts = _context.Accounts.Where(i => string.IsNullOrEmpty(searchTerm) || i.Name.Contains(searchTerm)).Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderBy((s) => s.Name).ToList(),
                TotalCount = totalCount,
                TotalPages = totalPages,
            };
        }

        public Account GetByName(string accountName)
        {
            return _context.Accounts.FirstOrDefault(x => x.Name == accountName);
        }

        public async Task<bool> AccountExists(string accountName)
        {
            return await _context.Accounts.AnyAsync(x => x.Name == accountName);
        }

        public Account Update(Account account)
        {
            _context.Entry(account).State = EntityState.Modified;
            _context.SaveChanges();
            return account;
        }
    }
}
