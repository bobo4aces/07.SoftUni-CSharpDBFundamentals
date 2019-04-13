using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BillsPaymentSystem.Models
{
    public class CreditCard
    {
        public CreditCard(decimal limit, decimal moneyOwned, DateTime expirationDate)
        {
            this.Limit = limit;
            this.MoneyOwned = moneyOwned;
            this.ExpirationDate = expirationDate;
        }
        public int CreditCardId { get; set; }
        public decimal Limit { get; set; }
        public decimal MoneyOwned { get; set; }

        [NotMapped]
        public decimal LimitLeft
        {
            get
            {
                return this.Limit - this.MoneyOwned;
            }
        }
        public DateTime ExpirationDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public void Deposit(decimal amount)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException("Amount must be greater than 0!");
            }
            this.MoneyOwned -= amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException("Amount must be greater than 0!");
            }
            if (amount > this.LimitLeft)
            {
                throw new InvalidOperationException("Not enough money in the card!");
            }
            this.MoneyOwned += amount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--ID: " + this.CreditCardId);
            sb.AppendLine("---Limit: " + this.Limit);
            sb.AppendLine("---Money Owed: " + this.MoneyOwned);
            sb.AppendLine("---Limit Left: " + this.LimitLeft);
            sb.AppendLine("---Expiration Date: " + this.ExpirationDate);

            return sb.ToString().TrimEnd();
        }
    }
}
