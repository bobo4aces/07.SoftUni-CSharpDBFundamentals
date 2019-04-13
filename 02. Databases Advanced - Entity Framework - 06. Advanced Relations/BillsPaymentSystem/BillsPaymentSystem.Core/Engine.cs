using BillsPaymentSystem.Core.Contracts;
using BillsPaymentSystem.Core.IO.Contracts;
using BillsPaymentSystem.Data;
using BillsPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillsPaymentSystem.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly BillsPaymentSystemContext context;

        public Engine(IReader reader, IWriter writer, BillsPaymentSystemContext context)
        {
            this.reader = reader;
            this.writer = writer;
            this.context = context;
        }

        public void Run()
        {
            this.writer.WriteLine(OutputMessages.WelcomeString());

            while (true)
            {
                try
                {
                    this.writer.WriteLine(OutputMessages.MenuOptions());

                    string result = string.Empty;
                    string[] command = this.reader.ReadLine().Split();

                    if (command[0] == "1")
                    {
                        this.writer.WriteLine(OutputMessages.EnterId);

                        int userId = int.Parse(this.reader.ReadLine());

                        this.writer.WriteLine(OutputMessages.Loading);
                        result = this.FindUser(userId);
                    }
                    else if (command[0] == "2")
                    {
                        this.writer.WriteLine(OutputMessages.EnterIdAndAmount);

                        string[] tokens = this.reader.ReadLine().Split();
                        int userId = int.Parse(tokens[0]);
                        decimal amount = decimal.Parse(tokens[1]);

                        this.writer.WriteLine(OutputMessages.Loading);
                        result = this.PayBills(userId, amount);

                        this.context.SaveChanges();
                    }
                    else if (command[0] == "9")
                    {
                        this.writer.WriteLine("Bye!");
                        Environment.Exit(0);
                    }

                    this.writer.WriteLine(result);
                }
                catch (Exception)
                {
                    this.writer.WriteLine(OutputMessages.InvalidCommand);
                }
            }
        }

        private string FindUser(int userId)
        {
            var userInfo = context.Users
                    .Select(u => new
                    {
                        Id = u.UserId,
                        Name = u.FirstName + " " + u.LastName,
                        BankAccounts = u.PaymentMethods
                            .Where(x => x.BankAccount != null)
                            .Select(pm => pm.BankAccount)
                            .ToList(),
                        CreditCards = u.PaymentMethods
                            .Where(x => x.CreditCard != null)
                            .Select(pm => pm.CreditCard)
                            .ToList(),
                    })
                    .SingleOrDefault(u => u.Id == userId);


            StringBuilder sb = new StringBuilder();

            if (DoesExist(userInfo))
            {
                sb.AppendLine("User: " + userInfo.Name);

                sb.AppendLine("Bank Accounts:");
                foreach (var bankAccount in userInfo.BankAccounts)
                {
                    sb.AppendLine(bankAccount.ToString());
                }

                sb.AppendLine("Credit Cards:");
                foreach (var creditCard in userInfo.CreditCards)
                {
                    sb.AppendLine(creditCard.ToString());
                }
            }
            else
            {
                sb.AppendLine($"User with id {userId} not found!");
            }

            return sb.ToString().TrimEnd();
        }

        private bool DoesExist(object userInfo) => userInfo != null;

        private string PayBills(int userId, decimal billsAmount)
        {
            var user = this.context.Users
                .Include(x => x.PaymentMethods)
                .ThenInclude(x => x.BankAccount)
                .Include(x => x.PaymentMethods)
                .ThenInclude(x => x.CreditCard)
                .FirstOrDefault(x => x.UserId == userId);

            decimal total = FindTotalMoney(user);

            StringBuilder sb = new StringBuilder();
            if (total < billsAmount)
            {
                sb.AppendLine("Not enough money!");
            }
            else
            {
                var accounts = user.PaymentMethods
                    .Where(x => x.BankAccount != null)
                    .Select(x => x.BankAccount)
                    .OrderBy(x => x.BankAccountId)
                    .ToList();

                foreach (var account in accounts)
                {
                    if (account.Balance < billsAmount)
                    {
                        billsAmount -= account.Balance;
                        account.Withdraw(account.Balance);
                    }
                    else
                    {
                        account.Withdraw(billsAmount);
                        billsAmount = 0;
                        break;
                    }
                }

                List<CreditCard> creditCards = user.PaymentMethods
                   .Where(x => x.CreditCard != null)
                   .Select(x => x.CreditCard)
                   .OrderBy(x => x.CreditCardId)
                   .ToList();

                foreach (var creditCard in creditCards)
                {
                    if (creditCard.LimitLeft < billsAmount)
                    {
                        billsAmount -= creditCard.LimitLeft;
                        creditCard.Withdraw(creditCard.LimitLeft);
                    }
                    else
                    {
                        creditCard.Withdraw(billsAmount);
                        billsAmount = 0;
                        break;
                    }
                }

                sb.AppendLine("Success!");
            }

            return sb.ToString().TrimEnd();
        }

        private static decimal FindTotalMoney(User user)
        {
            decimal bankAccountTotalAmount = user.PaymentMethods
                .Where(x => x.BankAccount != null)
                .Sum(x => x.BankAccount.Balance);
            decimal creditCardTotalAmount = user.PaymentMethods
                .Where(x => x.CreditCard != null)
                .Sum(x => x.CreditCard.LimitLeft);
            decimal total = bankAccountTotalAmount + creditCardTotalAmount;
            return total;
        }

    }
}
