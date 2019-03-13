using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models
{
    public class User
    {
        public User()
        {

        }

        public User(string username, string password, string email, string name, decimal balance)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Name = name;
            this.Balance = balance;
            this.Bets = new List<Bet>();
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Username", TypeName = "varchar(50)")]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Password", TypeName = "varchar(50)")]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Email", TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Name", TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required]
        [Column("Balance", TypeName = "money")]
        public decimal Balance { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}
