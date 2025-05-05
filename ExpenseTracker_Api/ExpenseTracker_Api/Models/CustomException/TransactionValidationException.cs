namespace ExpenseTracker_Api.Models.CustomException
{
    public class TransactionValidationException : Exception
    {
        public TransactionValidationException() { }

        public TransactionValidationException(string message)
       : base(message)
        {
        }

        public TransactionValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
