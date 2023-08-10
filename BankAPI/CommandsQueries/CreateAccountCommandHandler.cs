using BankAPI.Helper;
using BankAPI.Model;
using BankAPI.Repositories;
using MediatR;

namespace BankAPI.CommandsQueries
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Account>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountHelper _accountHelper;

        public CreateAccountCommandHandler(IAccountRepository accountRepository, IAccountHelper accountHelper)
        {
            _accountRepository = accountRepository;
            _accountHelper = accountHelper;
        }

        public async Task<Account> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = _accountHelper.HashPassword(request.Password);
            return await _accountRepository.CreateAccount(hashedPassword, request.FirstName, request.LastName, request.DepositAmount);
        }
    }
}
