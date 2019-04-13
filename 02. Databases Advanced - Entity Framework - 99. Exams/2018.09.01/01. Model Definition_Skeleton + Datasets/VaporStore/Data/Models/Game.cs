﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaporStore.Data.Models
{
	public class Game
    {
        //        •	Id – integer, Primary Key
        //•	Name – text(required)
        //•	Price – decimal (non-negative, minimum value: 0) (required)
        //•	ReleaseDate – Date(required)
        //•	DeveloperId – integer, foreign key(required)
        //•	Developer – the game’s developer(required)
        //•	GenreId – integer, foreign key(required)
        //•	Genre – the game’s genre(required)
        //•	Purchases - collection of type Purchase
        //•	GameTags - collection of type GameTag.Each game must have at least one tag.

        public Game()
        {
            this.Purchases = new List<Purchase>();
            this.GameTags = new List<GameTag>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(max)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Range(typeof(decimal),"0", "79228162514264337593543950335")]
        public decimal Price { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int DeveloperId { get; set; }

        [Required]
        [ForeignKey(nameof(DeveloperId))]
        public Developer Developer { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Required]
        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
        public ICollection<GameTag> GameTags { get; set; }
    }
}