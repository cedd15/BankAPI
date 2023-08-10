using BankAPI.Helper;
using BankAPI.Model;
using BankAPI.Repositories;
using MediatR;

namespace BankAPI.CommandsQueries
{
    public class TransferMoneyCommandHandler : IRequestHandler<TransferMoneyCommand, Account>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountHelper _accountHelper;

        public TransferMoneyCommandHandler(IAccountRepository accountRepository, IAccountHelper accountHelper)
        {
            _accountRepository = accountRepository;
            _accountHelper = accountHelper;
        }

        public async Task<Account> Handle(TransferMoneyCommand request, CancellationToken cancellationToken)
        {
            var fromAccount = await _accountRepository.GetAccount(request.FromAccountId);
            var toAccount = await _accountRepository.GetAccount(request.ToAccountId);

            if (fromAccount == null || toAccount == null)
                throw new Exception("Origin/destination account does not exist.");

            if (!_accountHelper.IsAccountVerified(request.Password, fromAccount.Password))
                throw new Exception("Invalid password");

            var remainingBalance = _accountHelper.GetBalanceAfterWithdrawal(fromAccount.Balance, request.TransferAmount);
            fromAccount.Balance = remainingBalance;

            var newBalance = _accountHelper.GetBalanceAfterDeposit(toAccount.Balance, request.TransferAmount);
            toAccount.Balance = newBalance;

            await _accountRepository.UpdateAccount(request.FromAccountId, fromAccount);
            await _accountRepository.UpdateAccount(request.ToAccountId, toAccount);

            return fromAccount;
        }
    }
}
