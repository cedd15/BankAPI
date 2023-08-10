using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankAPI.Data;
using BankAPI.Model;
using BankAPI.Repositories;
using BankAPI.Helper;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountHelper _accountHelper;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IAccountRepository accountRepository, IAccountHelper accountHelper, ILogger<AccountsController> logger)
        {
            _accountRepository = accountRepository;
            _accountHelper = accountHelper;
            _logger = logger;
        }

        // POST: api/CreateAccount
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount(string password, string firstName, string lastName, decimal depositAmount)
        {
            try
            {
                var account = await _accountRepository.CreateAccount(_accountHelper.HashPassword(password), firstName, lastName, depositAmount);
                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/GetBalance/5
        [HttpGet("GetBalance/{id}")]
        public async Task<IActionResult> GetBalance(int id, string password)
        {
            try
            {
                var account = await _accountRepository.GetAccount(id);

                if (account == null) return NotFound();

                if (!_accountHelper.IsAccountVerified(password, account.Password)) 
                    return BadRequest("Invalid password");

                return Ok($"Your account balance is {account.Balance}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("WithdrawCash/{id}")]
        public async Task<IActionResult> WithdrawCash(int id, string password, decimal withdrawalAmount)
        {
            try
            {
                var account = await _accountRepository.GetAccount(id);

                if (account == null) return NotFound();

                if (_accountHelper.IsAccountVerified(password, account.Password))
                    return BadRequest("Invalid password");

                var remainingBalance = _accountHelper.GetBalanceAfterWithdrawal(account.Balance, withdrawalAmount);
                account.Balance = remainingBalance;

                await _accountRepository.UpdateAccount(id, account);

                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("DepositCash/{id}")]
        public async Task<IActionResult> DepositCash(int id, string password, decimal depositAmount)
        {
            try
            { 
                var account = await _accountRepository.GetAccount(id);

                if (account == null) return NotFound();

                if (_accountHelper.IsAccountVerified(password, account.Password))
                    return BadRequest("Invalid password");

                var newBalance = _accountHelper.GetBalanceAfterDeposit(account.Balance, depositAmount);
                account.Balance = newBalance;

                await _accountRepository.UpdateAccount(id, account);

                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("TransferMoney")]
        public async Task<IActionResult> TransferMoney(int fromId, string password, decimal transferAmount, int toId)
        {
            try
            {
                var fromAccount = await _accountRepository.GetAccount(fromId);
                var toAccount = await _accountRepository.GetAccount(toId);

                if (fromAccount == null || toAccount == null) return NotFound("Origin/destination account does not exist.");

                if (_accountHelper.IsAccountVerified(password, fromAccount.Password))
                    return BadRequest("Invalid password");

                var remainingBalance = _accountHelper.GetBalanceAfterWithdrawal(fromAccount.Balance, transferAmount);
                fromAccount.Balance = remainingBalance;

                var newBalance = _accountHelper.GetBalanceAfterDeposit(toAccount.Balance, transferAmount);
                toAccount.Balance = newBalance;

                await _accountRepository.UpdateAccount(fromId, fromAccount);
                await _accountRepository.UpdateAccount(toId, toAccount);

                return Ok(fromAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

    }
}
