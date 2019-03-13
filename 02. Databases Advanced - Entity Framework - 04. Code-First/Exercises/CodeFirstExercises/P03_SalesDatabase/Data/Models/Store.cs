using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P03_SalesDatabase.Data.Models
{
    public class Store
    {
        public Store()
        {

        }

        public Store(string name)
        {
            this.Name = name;
            this.Sales = new List<Sale>();
        }

        [Key]
        public int StoreId { get; set; }

        [Required]
        [StringLength(80)]
        [Column(TypeName = "nvarchar(80)")]
        public string Name { get; set; }

        public List<Sale> Sales { get; set; }
    }
}
