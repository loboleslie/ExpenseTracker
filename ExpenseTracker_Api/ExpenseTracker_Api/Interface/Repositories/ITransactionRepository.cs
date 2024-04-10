using ExpenseTracker_Api.Models;

namespace ExpenseTracker_Api.Interface.Repositories
{
    public interface ITransactionRepository
    {
        Transaction AddTransaction(Transaction transaction);
        Transaction? GetTransaction(int transactionId);
        Transaction UpdateTransaction(Transaction account);
        int DeleteTransaction(int transactionId);

    }
}
