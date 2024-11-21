using AutoMapper;
using ExpenseTracker_Api.Models;
using ExpenseTracker_Api.Models.DTO;

namespace ExpenseTracker_Api.Mapping
{
    public class Automapper : Profile
    {
        public Automapper() 
        {
            CreateMap<AccountDto, Account>();
        }
        
    }
}
