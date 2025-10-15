using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using ExpenseTracker_Api.Interface.Repositories;
using ExpenseTracker_Api.Interface.Services;
using ExpenseTracker_Api.Models;

namespace ExpenseTracker_Api.Services;

public class BulkProcessResult
{
    public int SuccessCount { get; set; }
    public int FailureCount { get; set; }
    public List<string> ErrorMessages { get; set; } = new();
}

public class BulkProcessService : IBulkProcessService
{
    private readonly ITransactionRepository _transactionRepository;

    public BulkProcessService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public BulkProcessResult ProcessCsv(string csvContent, int accountId)
    {
        var result = new BulkProcessResult();
        using var reader = new StringReader(csvContent);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<dynamic>();
        int row = 1;
        foreach (var record in records)
        {
            string dateStr = record.Date;
            string amountStr = record.Amount;
            string payee = record.Payee;

            if (DateTimeOffset.TryParse(dateStr, out var transactionDate) &&
                decimal.TryParse(amountStr, out var amount) &&
                !string.IsNullOrWhiteSpace(payee))
            {
                var transaction = new Transaction
                {
                    TransactionDate = transactionDate,
                    Amount = amount,
                    Description = payee,
                    AccountId = accountId
                };
                _transactionRepository.Add(transaction);
                result.SuccessCount++;
            }
            else
            {
                result.FailureCount++;
                result.ErrorMessages.Add($"Row {row}: Invalid data");
            }
            row++;
        }
        return result;
    }
}