using System.Collections.Generic;

namespace ExpenseTracker_Api.Models.Responses
{
    public class ApiResponses
    {

        public ApiResponses() 
        {
            Errors = new List<string>();
        }
        public int StatusCode { get; set; }
        public List<String>? Errors { get; set; }
        public Object Result { get; set; }
    }
}
