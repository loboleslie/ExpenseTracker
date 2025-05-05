using ExpenseTracker_Api.Models;
using ExpenseTracker_Api.Models.DTO;
using ExpenseTracker_Api.Models.Responses;

namespace ExpenseTracker_Api.Interface.Services
{
    public interface ITransactionService
    {
        Task<ApiResponses> SaveTransaction(TransactionDto account);
        Task<ApiResponses> ModifyTransaction(int transactionId, TransactionDto transaction);
        Task<ApiResponses> RemoveTransaction(int transactionId);
        Task<ApiResponses> RetrieveTransaction(int transactionId);
        Task<ApiResponses> RetrieveAllTransaction(int pageNumber, int pageSize, DateTimeOffset fromDate, DateTimeOffset toDate, int accountId, string searchTerm = "");
    }
}
