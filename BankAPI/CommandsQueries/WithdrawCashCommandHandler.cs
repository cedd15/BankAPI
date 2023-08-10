using BankAPI.Helper;
using BankAPI.Model;
using BankAPI.Repositories;
using MediatR;

namespace BankAPI.CommandsQueries
{
    public class WithdrawCashCommandHandler : IRequestHandler<WithdrawCashCommand, Account>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountHelper _accountHelper;

        public WithdrawCashCommandHandler(IAccountRepository accountRepository, IAccountHelper accountHelper)
        {
            _accountRepository = accountRepository;
            _accountHelper = accountHelper;
        }

        public async Task<Account> Handle(WithdrawCashCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccount(request.Id);

            if (account == null)
                throw new Exception("Account not found");

            if (!_accountHelper.IsAccountVerified(request.Password, account.Password))
                throw new Exception("Invalid password");

            var remainingBalance = _accountHelper.GetBalanceAfterWithdrawal(account.Balance, request.WithdrawalAmount);
            account.Balance = remainingBalance;

            await _accountRepository.UpdateAccount(request.Id, account);

            return account;
        }
    }
}
