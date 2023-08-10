using BankAPI.Helper;

namespace BankAPITest
{
    [TestFixture]
    public class AccountHelperTests
    {
        private IAccountHelper _accountHelper;

        [SetUp]
        public void Setup()
        {
            _accountHelper = new AccountHelper();
        }

        [Test]
        public void HashPassword_ValidInput_ReturnsHashedPassword()
        {
            string password = "testPassword";
            string hashedPassword = _accountHelper.HashPassword(password);

            // Assert that the hashed password is not equal to the original password
            Assert.AreNotEqual(password, hashedPassword);
        }

        [TestCase("password", "5f4dcc3b5aa765d61d8327deb882cf99")] // MD5 hash of "password"
        [TestCase("123456", "e10adc3949ba59abbe56e057f20f883e")] // MD5 hash of "123456"
        public void HashPassword_ValidInput_CorrectHashedPassword(string password, string expectedHash)
        {
            string hashedPassword = _accountHelper.HashPassword(password);

            Assert.AreEqual(expectedHash, hashedPassword);
        }

        [Test]
        public void IsAccountVerified_CorrectPassword_ReturnsTrue()
        {
            string password = "testPassword";
            string hashedPassword = _accountHelper.HashPassword(password);

            bool isVerified = _accountHelper.IsAccountVerified(password, hashedPassword);

            Assert.IsTrue(isVerified);
        }

        [Test]
        public void IsAccountVerified_IncorrectPassword_ReturnsFalse()
        {
            string correctPassword = "testPassword";
            string incorrectPassword = "wrongPassword";
            string hashedPassword = _accountHelper.HashPassword(correctPassword);

            bool isVerified = _accountHelper.IsAccountVerified(incorrectPassword, hashedPassword);

            Assert.IsFalse(isVerified);
        }

        [Test]
        public void GetBalanceAfterWithdrawal_ValidWithdrawal_ReturnsCorrectBalance()
        {
            decimal initialBalance = 1000;
            decimal withdrawalAmount = 200;
            decimal expectedBalance = initialBalance - withdrawalAmount;

            decimal newBalance = _accountHelper.GetBalanceAfterWithdrawal(initialBalance, withdrawalAmount);

            Assert.AreEqual(expectedBalance, newBalance);
        }

        [Test]
        public void GetBalanceAfterWithdrawal_InsufficientBalance_ThrowsException()
        {
            decimal initialBalance = 100;
            decimal withdrawalAmount = 200;

            Assert.Throws<Exception>(() => _accountHelper.GetBalanceAfterWithdrawal(initialBalance, withdrawalAmount));
        }

        [Test]
        public void GetBalanceAfterWithdrawal_NegativeWithdrawalAmount_ThrowsException()
        {
            decimal initialBalance = 100;
            decimal withdrawalAmount = -50;

            Assert.Throws<Exception>(() => _accountHelper.GetBalanceAfterWithdrawal(initialBalance, withdrawalAmount));
        }

        [Test]
        public void GetBalanceAfterDeposit_ValidDeposit_ReturnsCorrectBalance()
        {
            decimal initialBalance = 1000;
            decimal depositAmount = 200;
            decimal expectedBalance = initialBalance + depositAmount;

            decimal newBalance = _accountHelper.GetBalanceAfterDeposit(initialBalance, depositAmount);

            Assert.AreEqual(expectedBalance, newBalance);
        }

        [Test]
        public void GetBalanceAfterDeposit_NegativeDepositAmount_ThrowsException()
        {
            decimal initialBalance = 100;
            decimal depositAmount = -50;

            Assert.Throws<Exception>(() => _accountHelper.GetBalanceAfterDeposit(initialBalance, depositAmount));
        }

        [Test]
        public void GetBalanceAfterDeposit_ZeroDepositAmount_ThrowsException()
        {
            decimal initialBalance = 100;
            decimal depositAmount = 0;

            Assert.Throws<Exception>(() => _accountHelper.GetBalanceAfterDeposit(initialBalance, depositAmount));
        }
    }
}