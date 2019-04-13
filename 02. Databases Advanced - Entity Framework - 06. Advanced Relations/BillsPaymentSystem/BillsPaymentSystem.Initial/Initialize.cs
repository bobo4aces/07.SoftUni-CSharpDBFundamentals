using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BillsPaymentSystem.Data;

namespace BillsPaymentSystem.Initial
{
    public class Initialize
    {
        public static void Seed(BillsPaymentSystemContext context)
        {
            InsertValues(UserInitializer.GetUsers(), context);
            InsertValues(CreditCardInitializer.GetCreditCards(), context);
            InsertValues(BankAccountInitializer.GetBankAccounts(), context);
            InsertValues(PaymentMethodInitializer.GetPaymentMethods(), context);
        }

        private static void InsertValues<T>(T[] entities, BillsPaymentSystemContext context)
            where T: class
        {
            foreach (var entity in entities)
            {
                if (IsValid(entity))
                {
                    context.Add(entity);
                }
            }

            context.SaveChanges();
        }

        private static bool IsValid<T>(T entity)
            where T : class
        {
            ValidationContext validationContext = new ValidationContext(entity);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(entity, validationContext, validationResults, true);
        }
    }
}
