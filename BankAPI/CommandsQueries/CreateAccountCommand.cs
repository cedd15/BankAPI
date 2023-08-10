using BankAPI.Model;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BankAPI.CommandsQueries
{
    public class CreateAccountCommand : IRequest<Account>
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public decimal DepositAmount { get; set; }
    }
}
