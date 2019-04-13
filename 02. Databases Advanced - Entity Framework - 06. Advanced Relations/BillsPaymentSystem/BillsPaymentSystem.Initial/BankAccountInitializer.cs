using BillsPaymentSystem.Models;

namespace BillsPaymentSystem.Initial
{
    public class BankAccountInitializer
    {
        public static BankAccount[] GetBankAccounts()
        {
            BankAccount[] bankAccounts = new BankAccount[]
            {
                new BankAccount(11.1m, "zzzzzzzzz", "ww"),
                new BankAccount(111.1m, "zzzzzzzz", "www"),
                new BankAccount(1111.1m, "zzzzzzz", "wwww"),
                new BankAccount(11111.1m, "zzzzzzz", "wwwww"),
                new BankAccount(111111.1m, "zzzzzz", "wwwwww"),
                new BankAccount(1111111.1m, "zzzz", "wwwwwww"),
                new BankAccount(11111111.1m, "zz", "wwwwwwww")
            };

            return bankAccounts;
        }
    }
}
