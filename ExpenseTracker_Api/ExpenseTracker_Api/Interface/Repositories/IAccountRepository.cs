using ExpenseTracker_Api.Models;

namespace ExpenseTracker_Api.Interface.Repositories
{
    public interface IAccountRepository
    {
        List<Account> GetAll();
        Account Add(Account account);
        Account Update(Account account);
        Account Delete(int accountId);
        Account Get(int id);
        Account GetByName(string accountName);
    }
}
