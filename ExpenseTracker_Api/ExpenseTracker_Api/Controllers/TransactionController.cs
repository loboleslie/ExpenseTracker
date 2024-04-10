using ExpenseTracker_Api.DTO;
using ExpenseTracker_Api.Interface.Repositories;
using ExpenseTracker_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository  transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            

            return Ok();
        }

        [HttpPost]
        public IActionResult Add(TransactionRequest transactionRequest)
        {
            var transaction = new Transaction { Description = transactionRequest.Description, AccountId = transactionRequest.AccountId };
            _transactionRepository.AddTransaction(transaction);

            return Ok(transaction);
        }

        [HttpPut]
        public IActionResult Edit(Transaction transaction)
        {
            _transactionRepository.UpdateTransaction(transaction);
            return Ok(transaction);
        }

        [HttpDelete]
        public IActionResult Delete(int id) 
        {
            _transactionRepository?.DeleteTransaction(id);
            return Ok();
        }

    }
}
