using System;
using System.Text;

namespace BillsPaymentSystem.Models
{
    public class BankAccount
    {
        public BankAccount(decimal balance, string bankName, string swiftCode)
        {
            this.Balance = balance;
            this.BankName = bankName;
            this.SwiftCode = swiftCode;
        }
        public int BankAccountId { get; set; }
        public decimal Balance { get; set; }
        public string BankName { get; set; }
        public string SwiftCode { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public void Deposit(decimal amount)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException("Amount must be greater than 0!");
            }
            this.Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException("Amount must be greater than 0!");
            }
            if (amount > this.Balance)
            {
                throw new InvalidOperationException("Not enough money in the card!");
            }
            this.Balance -= amount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--ID: " + this.BankAccountId);
            sb.AppendLine("---Balance: " + this.Balance);
            sb.AppendLine("---Bank: " + this.BankName);
            sb.AppendLine("---SWIFT: " + this.SwiftCode);

            return sb.ToString().TrimEnd();
        }

    }
}
