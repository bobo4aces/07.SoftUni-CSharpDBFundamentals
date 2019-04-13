using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.Datasets.Dto
{
    public class ExportAllGamesByGenreDto
    {
        //        "Id": 4,
        //    "Genre": "Violent",
        //    "Games": [
        //      {
        //        "Id": 49,
        //        "Title": "Warframe",
        //        "Developer": "Digital Extremes",
        //        "Tags": "Single-player, In-App Purchases, Steam Trading Cards, Co-op, Multi-player, Partial Controller Support",
        //        "Players": 6
        //      },
        //"TotalPlayers": 10

        public int Id { get; set; }
        public string Genre { get; set; }
        public ExportGameDto[] Games { get; set; }
        public int TotalPlayers { get; set; }
    }
}
