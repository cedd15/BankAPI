using BankAPI.Model;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BankAPI.CommandsQueries
{
    public class TransferMoneyCommand : IRequest<Account>
    {
        [Required]
        public int FromAccountId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public decimal TransferAmount { get; set; }
        [Required]
        public int ToAccountId { get; set; }
    }
}
