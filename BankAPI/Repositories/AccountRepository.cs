using BankAPI.Data;
using BankAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Repositories
{

    public interface IAccountRepository
    {
        Task<Account> GetAccount(int id);
        Task<Account> CreateAccount(string username, string password, string firstName, string lastName, decimal depositAmount);
    }

    public class AccountRepository : IAccountRepository
    {
        private readonly AccountContext _accountContext;

        public AccountRepository(AccountContext accountContext) 
        {
            _accountContext = accountContext;
        }

        public async Task<Account> GetAccount(int id)
        {
            return await _accountContext.Account.FindAsync(id);
        }

        public async Task<Account> CreateAccount(string username, string password, string firstName, string lastName, decimal depositAmount)
        {
            var account = new Account()
            {
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Balance = depositAmount
            };

            _accountContext.Account.Add(account);
            await _accountContext.SaveChangesAsync();

            return account;
        }
    }
}
