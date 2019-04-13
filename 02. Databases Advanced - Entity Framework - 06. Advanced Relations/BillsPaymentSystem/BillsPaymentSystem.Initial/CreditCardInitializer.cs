using BillsPaymentSystem.Models;
using System;

namespace BillsPaymentSystem.Initial
{
    public class CreditCardInitializer
    {
        public static CreditCard[] GetCreditCards()
        {
            CreditCard[] creditCards = new CreditCard[]
            {
                new CreditCard(11.1m, 1m, new DateTime(2020,1,1)),
                new CreditCard(111.1m, 11m, new DateTime(2020,2,1)),
                new CreditCard(1111.1m, 111m, new DateTime(2020,3,1)),
                new CreditCard(11111.1m, 1111m, new DateTime(2020,4,1)),
                new CreditCard(111111.1m, 11111m, new DateTime(2020,5,1)),
                new CreditCard(1111111.1m, 111111m, new DateTime(2020,6,1)),
                new CreditCard(11111111.1m, 1111111m, new DateTime(2020,7,1))
            };

            return creditCards;
        }
    }
}
