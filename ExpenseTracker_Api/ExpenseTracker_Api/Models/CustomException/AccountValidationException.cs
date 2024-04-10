namespace ExpenseTracker_Api.Models.CustomException
{
    public class AccountValidationException : Exception
    {
        public AccountValidationException() { }

        public AccountValidationException(string message)
       : base(message)
        {
        }

        public AccountValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
