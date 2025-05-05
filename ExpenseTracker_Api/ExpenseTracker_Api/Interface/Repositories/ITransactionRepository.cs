using ExpenseTracker_Api.Models;
using ExpenseTracker_Api.Models.DTO;
using ExpenseTracker_Api.Models.Responses;

namespace ExpenseTracker_Api.Interface.Repositories
{
    public interface ITransactionRepository
    {
        Transaction Add(Transaction transaction);
        Transaction? Get(int transactionId);
        public PaginatedTransactionList GetAll(int pageNumber, int pageSize, string searchTerm, DateTimeOffset? fromDate, DateTimeOffset? toDate, int accountId);
        Transaction? GetTransaction(TransactionDto transactionDto);
        Transaction Update(Transaction account);
        int Delete(int transactionId);

    }
}
