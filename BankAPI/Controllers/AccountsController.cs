using MediatR;
using BankAPI.CommandsQueries; 
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(IMediator mediator, ILogger<AccountsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Registers a bank account. Customer name, password and deposit amount are required.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Returns the account created.</returns>
        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount(CreateAccountCommand command)
        {
            try
            {
                var account = await _mediator.Send(command);
                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves the balance of a customer by customer id.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Returns the account details with the balance.</returns>
        [HttpPost("GetBalance")]
        public async Task<IActionResult> GetBalance(GetAccountBalanceQuery query)
        {
            try
            {
                var balanceMessage = await _mediator.Send(query);
                return Ok(balanceMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Withdraws cash from customer balance.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Returns the account details with the updated balance.</returns>
        [HttpPut("WithdrawCash")]
        public async Task<IActionResult> WithdrawCash(WithdrawCashCommand command)
        {
            try
            {
                var account = await _mediator.Send(command);
                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deposit cash to customer's balance
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Returns the account details with the updated balance.</returns>
        [HttpPut("DepositCash")]
        public async Task<IActionResult> DepositCash(DepositCashCommand command)
        {
            try
            {
                var account = await _mediator.Send(command);
                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Transfers money to another account
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Returns the account details of the origin account with the updated balance.</returns>
        [HttpPut("TransferMoney")]
        public async Task<IActionResult> TransferMoney(TransferMoneyCommand command)
        {
            try
            {
                var account = await _mediator.Send(command);
                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }
    }
}