using BankAPI.Model;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BankAPI.CommandsQueries
{
    public class DepositCashCommand : IRequest<Account>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public decimal DepositAmount { get; set; }
    }
}
