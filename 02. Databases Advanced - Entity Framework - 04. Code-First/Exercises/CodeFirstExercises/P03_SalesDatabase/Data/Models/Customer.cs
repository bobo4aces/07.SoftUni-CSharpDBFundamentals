using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P03_SalesDatabase.Data.Models
{
    public class Customer
    {
        public Customer()
        {

        }

        public Customer(string name, string email, string creditCardNumber)
        {
            this.Name = name;
            this.Email = email;
            this.CreditCardNumber = creditCardNumber;
            this.Sales = new List<Sale>();
        }

        [Key]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Required]
        [StringLength(80)]
        [Column(TypeName = "varchar(80)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string CreditCardNumber { get; set; }

        public List<Sale> Sales { get; set; }
    }
}
