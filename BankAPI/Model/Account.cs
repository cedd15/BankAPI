﻿namespace BankAPI.Model
{
    public class Account
    {
        /// <summary>
        /// Customer's account ID
        /// </summary>
        public int Id { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
    }
}
