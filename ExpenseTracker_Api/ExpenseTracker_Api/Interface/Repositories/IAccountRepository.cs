using ExpenseTracker_Api.Models;
using ExpenseTracker_Api.Models.Responses;

namespace ExpenseTracker_Api.Interface.Repositories
{
    public interface IAccountRepository
    {
        PaginatedAccountList GetAll(int page, int pageSize, string searchTerm);
        Account Add(Account account);
        Account Update(Account account);
        Account Delete(int accountId);
        Account Get(int id);
        Account GetByName(string accountName);
    }
}
