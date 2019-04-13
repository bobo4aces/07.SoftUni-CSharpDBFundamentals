using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string LastName { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Salary { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Address { get; set; }
    }
}
