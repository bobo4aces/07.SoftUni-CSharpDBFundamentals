using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_SalesDatabase.Data.Models
{
    public class Product
    {
        public Product()
        {

        }
        public Product(string name, double quantity, decimal price)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.Price = price;
            this.Sales = new List<Sale>();
        }

        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public double Quantity { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [StringLength(250)]
        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }

        public List<Sale> Sales { get; set; }
        //TODO
    }
}
