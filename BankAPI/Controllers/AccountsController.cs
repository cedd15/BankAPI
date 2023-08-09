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

        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        //// GET: api/Accounts
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Account>>> GetAccount()
        //{
        //  if (_context.Account == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.Account.ToListAsync();
        //}

        

        // POST: api/CreateAccount
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateAccount(string username, string password, string firstName, string lastName, decimal depositAmount)
        {
            try
            {
                var account = await _accountRepository.CreateAccount(username, AccountHelper.HashPassword(password), firstName, lastName, depositAmount);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/GetBalance/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBalance(int id, string password)
        {
            try
            {
                var account = await _accountRepository.GetAccount(id);

                if (account == null) return NotFound();

                var hashPassword = AccountHelper.HashPassword(password);

                if (hashPassword != account.Password) return BadRequest("Invalid password");

                return Ok($"Your account balance is {account.Balance}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Account>> PostAccount(Account account)
        //{
        //  if (_context.Account == null)
        //  {
        //      return Problem("Entity set 'AccountContext.Account'  is null.");
        //  }
        //    _context.Account.Add(account);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        //}

        //// DELETE: api/Accounts/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAccount(int id)
        //{
        //    if (_context.Account == null)
        //    {
        //        return NotFound();
        //    }
        //    var account = await _context.Account.FindAsync(id);
        //    if (account == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Account.Remove(account);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool AccountExists(int id)
        //{
        //    return (_context.Account?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

    }
}
