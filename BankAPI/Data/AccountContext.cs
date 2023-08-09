using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BankAPI.Model;

namespace BankAPI.Data
{
    public class AccountContext : DbContext
    {
        public AccountContext (DbContextOptions<AccountContext> options)
            : base(options)
        {
        }

        public DbSet<BankAPI.Model.Account> Account { get; set; } = default!;
    }
}
