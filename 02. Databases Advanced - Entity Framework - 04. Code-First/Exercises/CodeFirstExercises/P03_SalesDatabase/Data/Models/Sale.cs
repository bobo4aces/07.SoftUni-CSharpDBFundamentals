using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P03_SalesDatabase.Data.Models
{
    public class Sale
    {
        public Sale()
        {

        }
        public Sale(DateTime date, Product product, Customer customer, Store store)
        {

        }

        [Key]
        public int SaleId { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        
        public int ProductId { get; set; }

        [Required]
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        public int StoreId { get; set; }

        [Required]
        [ForeignKey(nameof(StoreId))]
        public Store Store { get; set; }
    }
}
