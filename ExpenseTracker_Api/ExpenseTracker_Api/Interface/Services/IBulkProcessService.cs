using ExpenseTracker_Api.Services;

namespace ExpenseTracker_Api.Interface.Services;

public interface IBulkProcessService
{
    BulkProcessResult ProcessCsv(string csvContent, int accountId);
}