using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaporStore.Data.Models
{
    public class GameTag
    {
        //        •	GameId – integer, Primary Key, foreign key(required)
        //•	TagId – integer, Primary Key, foreign key(required)
        //•	Game – Game
        //•	Tag – Tag
        public GameTag()
        {

        }
        public int GameId { get; set; }
        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }
        public int TagId { get; set; }
        [ForeignKey(nameof(TagId))]
        public Tag Tag { get; set; }
    }
}