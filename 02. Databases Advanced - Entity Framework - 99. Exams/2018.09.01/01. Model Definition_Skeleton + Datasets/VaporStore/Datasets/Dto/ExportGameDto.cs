namespace VaporStore.Datasets.Dto
{
    public class ExportGameDto
    {
        //      {
        //        "Id": 49,
        //        "Title": "Warframe",
        //        "Developer": "Digital Extremes",
        //        "Tags": "Single-player, In-App Purchases, Steam Trading Cards, Co-op, Multi-player, Partial Controller Support",
        //        "Players": 6
        //      },

        public int Id { get; set; }
        public string Title { get; set; }
        public string Developer { get; set; }
        public string Tags { get; set; }
        public int Players { get; set; }
    }
}