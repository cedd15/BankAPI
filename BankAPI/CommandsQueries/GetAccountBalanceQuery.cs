using BankAPI.Model;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BankAPI.CommandsQueries
{
    public class GetAccountBalanceQuery : IRequest<Account>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
