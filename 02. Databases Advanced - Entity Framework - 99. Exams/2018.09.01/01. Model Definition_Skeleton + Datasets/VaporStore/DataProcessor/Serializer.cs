namespace VaporStore.DataProcessor
{
	using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.Datasets.Dto;

    public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
		{
            ExportAllGamesByGenreDto[] gamesDto = context.Genres
                .Where(g => genreNames.Contains(g.Name))
                .Select(g => new ExportAllGamesByGenreDto()
                {
                    Id = g.Id,
                    Genre = g.Name,
                    Games = g.Games
                        .Where(ga=>ga.Purchases.Any())
                        .Select(ga => new ExportGameDto()
                        {
                            Id = ga.Id,
                            Title = ga.Name,
                            Developer = ga.Developer.Name,
                            Tags = string.Join(", ", ga.GameTags
                                .Select(gt => gt.Tag.Name)),
                            Players = ga.Purchases.Count
                        })
                        .OrderByDescending(ga=>ga.Players)
                        .ThenBy(ga=>ga.Id)
                        .ToArray(),
                    TotalPlayers = g.Games.Sum(ga => ga.Purchases.Count)
                })
                .OrderByDescending(g=>g.TotalPlayers)
                .ThenBy(g=>g.Id)
                .ToArray();

            string games = JsonConvert.SerializeObject(gamesDto, Newtonsoft.Json.Formatting.Indented);

            return games;
		}

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
            PurchaseType currentEnum = Enum.Parse<PurchaseType>(storeType);
            ExportUserPurchasesByTypeDto[] userPurchasesByTypeDtos = context.Users
                .Select(u => new ExportUserPurchasesByTypeDto()
                {
                    Username = u.Username,
                    Purchases = u.Cards
                                 .SelectMany(c => c.Purchases)
                                 .Where(p => p.Type == currentEnum)
                                 .Select(p => new ExportPurchaseDto()
                                 {
                                     Card = p.Card.Number,
                                     Cvc = p.Card.Cvc,
                                     Date = p.Date.ToString("yyyy-MM-dd HH:mm",CultureInfo.InvariantCulture),
                                     Game = new ExportXmlGameDto()
                                     {
                                         Title = p.Game.Name,
                                         Genre = p.Game.Genre.Name,
                                         Price = p.Game.Price.ToString("0.00")
                                     }
                                 })
                                 .OrderBy(x=>x.Date)
                                 .ToArray(),
                    TotalSpent = u.Cards
                                    .SelectMany(p => p.Purchases)
                                    .Where(p => p.Type == currentEnum)
                                    .Sum(p => p.Game.Price)
                })
                .OrderByDescending(u=>u.TotalSpent)
                .ThenBy(u=>u.Username)
                .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportUserPurchasesByTypeDto[]), new XmlRootAttribute("Users"));
            StringBuilder sb = new StringBuilder();
            xmlSerializer.Serialize(new StringWriter(sb), userPurchasesByTypeDtos);

            return sb.ToString().TrimEnd();
        }
	}
}