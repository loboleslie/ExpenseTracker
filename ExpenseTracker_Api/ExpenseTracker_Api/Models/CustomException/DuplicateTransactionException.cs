﻿namespace ExpenseTracker_Api.Models.CustomException
{
    public class DuplicateTransactionException : Exception
    {
        public DuplicateTransactionException() { }

        public DuplicateTransactionException(string message)
       : base(message)
        {
        }

        public DuplicateTransactionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
