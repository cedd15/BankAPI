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