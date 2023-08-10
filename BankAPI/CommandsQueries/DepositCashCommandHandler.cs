using BankAPI.Helper;
using BankAPI.Model;
using BankAPI.Repositories;
using MediatR;

namespace BankAPI.CommandsQueries
{
    public class DepositCashCommandHandler : IRequestHandler<DepositCashCommand, Account>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountHelper _accountHelper;

        public DepositCashCommandHandler(IAccountRepository accountRepository, IAccountHelper accountHelper)
        {
            _accountRepository = accountRepository;
            _accountHelper = accountHelper;
        }

        public async Task<Account> Handle(DepositCashCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccount(request.Id);

            if (account == null)
                throw new Exception("Account not found");

            if (!_accountHelper.IsAccountVerified(request.Password, account.Password))
                throw new Exception("Invalid password");

            var newBalance = _accountHelper.GetBalanceAfterDeposit(account.Balance, request.DepositAmount);
            account.Balance = newBalance;

            await _accountRepository.UpdateAccount(request.Id, account);

            return account;
        }
    }
}
