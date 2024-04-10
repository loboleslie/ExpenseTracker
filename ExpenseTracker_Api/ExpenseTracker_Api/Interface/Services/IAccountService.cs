using ExpenseTracker_Api.Models;
using ExpenseTracker_Api.Models.DTO;
using ExpenseTracker_Api.Models.Responses;

namespace ExpenseTracker_Api.Interface.Services
{
    public interface IAccountService
    {
        Task<ApiResponses> SaveAccount(AccountDTO account);
        Task<ApiResponses> ModifyAccount(AccountDTO account);
        Task<ApiResponses> RemoveAccount(int accountId);
        Task<ApiResponses> RetrieveAccount(int accountId);
        Task<ApiResponses> RetrieveAllAccounts();
    }
}
