using System.Security.Cryptography;
using System.Text;

namespace BankAPI.Helper
{
    public interface IAccountHelper
    {
        string HashPassword(string password);
        bool IsAccountVerified(string inputPassword, string accountPassword);
        decimal GetBalanceAfterWithdrawal(decimal accountBalance, decimal withdrawalAmount);
        decimal GetBalanceAfterDeposit(decimal accountBalance, decimal depositAmount);
    }

    public class AccountHelper : IAccountHelper
    {
        /// <summary>
        /// Convert password to hash
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Hashed password</returns>
        public string HashPassword(string password)
        {
            byte[] data = MD5.HashData(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();

            foreach (byte b in data)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }

        /// <summary>
        /// Verifies the input password
        /// </summary>
        /// <param name="inputPassword"></param>
        /// <param name="accountPassword"></param>
        /// <returns>Returns true if verified otherwise false</returns>
        public bool IsAccountVerified(string inputPassword, string accountPassword)
        {
            var hashPassword = HashPassword(inputPassword);

            return hashPassword == accountPassword;
        }

        public decimal GetBalanceAfterWithdrawal(decimal accountBalance, decimal withdrawalAmount)
        {
            if (accountBalance < withdrawalAmount)
                throw new Exception("Withdrawal amount must be less than account balance");

            if (withdrawalAmount <= 0)
                throw new Exception("Withdrawal amount must be greater than zero");

            return accountBalance - withdrawalAmount;
        }

        public decimal GetBalanceAfterDeposit(decimal accountBalance, decimal depositAmount)
        {
            if (depositAmount <= 0)
                throw new Exception("Deposit amount must be greater than zero");

            return accountBalance + depositAmount;
        }

    }
}
