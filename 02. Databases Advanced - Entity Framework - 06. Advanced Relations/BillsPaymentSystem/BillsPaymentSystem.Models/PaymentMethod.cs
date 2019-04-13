using BillsPaymentSystem.Models.Attributes;
using BillsPaymentSystem.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillsPaymentSystem.Models
{
    public class PaymentMethod
    {
        public PaymentMethod()
        {

        }
        public PaymentMethod(PaymentType type, int userId, int paymentTypeId)
        {
            this.Type = type;
            this.UserId = userId;
            if (type == PaymentType.BankAccount)
            {
                this.BankAccountId = paymentTypeId;
            }
            else
            {
                this.CreditCardId = paymentTypeId;
            }

        }
        public int Id { get; set; }
        public PaymentType Type { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Xor(nameof(CreditCardId))]
        public int? BankAccountId { get; set; }
        [ForeignKey(nameof(BankAccountId))]
        public BankAccount BankAccount { get; set; }

        public int? CreditCardId { get; set; }
        [ForeignKey(nameof(CreditCardId))]
        public CreditCard CreditCard { get; set; }
    }
}
