namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.Datasets.Dto;

    public static class Deserializer
	{
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
            ImportGameDto[] gameDtos = JsonConvert.DeserializeObject<ImportGameDto[]>(jsonString);
            List<Game> games = new List<Game>();
            List<Developer> developers = new List<Developer>();
            List<Genre> genres = new List<Genre>();
            List<Tag> tags = new List<Tag>();
            StringBuilder sb = new StringBuilder();

            foreach (var gameDto in gameDtos)
            {
                if (!IsValid(gameDto) || !gameDto.Tags.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }
                
                Developer developer = developers.FirstOrDefault(d => d.Name == gameDto.Developer);
                if (developer == null)
                {
                    developer = new Developer()
                                {
                                    Name = gameDto.Developer
                                };
                    developers.Add(developer);
                }
                Genre genre = genres.FirstOrDefault(g => g.Name == gameDto.Genre);
                if (genre == null)
                {
                    genre = new Genre()
                    {
                        Name = gameDto.Genre
                    };
                    genres.Add(genre);
                }

                List<Tag> gameTags = new List<Tag>();
                foreach (var gameDtoTag in gameDto.Tags)
                {
                    Tag tag = tags.FirstOrDefault(t => t.Name == gameDtoTag);
                    if (tag == null)
                    {
                        tag = new Tag()
                        {
                            Name = gameDtoTag
                        };
                        tags.Add(tag);
                    }
                    gameTags.Add(tag);
                }
                Game game = new Game()
                {
                    Name = gameDto.Name,
                    Price = gameDto.Price,
                    ReleaseDate = DateTime.Parse(gameDto.ReleaseDate, CultureInfo.InvariantCulture),
                    Developer = developer,
                    Genre = genre,
                    GameTags = gameTags.Select(gt => new GameTag() { Tag = gt }).ToArray()
                };
                games.Add(game);
                sb.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {game.GameTags.Count} tags");
            }

            context.Games.AddRange(games);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
            ImportUserDto[] usersDtos = JsonConvert.DeserializeObject<ImportUserDto[]>(jsonString);
            List<User> users = new List<User>();
            StringBuilder sb = new StringBuilder();

            foreach (var usersDto in usersDtos)
            {
                if (!IsValid(usersDto) || !usersDto.Cards.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }
                User user = new User()
                {
                    FullName = usersDto.FullName,
                    Username = usersDto.Username,
                    Age = usersDto.Age,
                    Email = usersDto.Email,
                    Cards = usersDto.Cards.Select(c => new Card()
                    {
                        Number = c.Number,
                        Cvc = c.CVC,
                        Type = Enum.Parse<CardType>(c.Type)
                    }).ToArray()
                };
                users.Add(user);
                sb.AppendLine($"Imported {user.Username} with {user.Cards.Count} cards");
            }
            context.AddRange(users);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportPurchaseDto[]), new XmlRootAttribute("Purchases"));
            ImportPurchaseDto[] purchaseDtos = (ImportPurchaseDto[])xmlSerializer.Deserialize(new StringReader(xmlString));
            List<Purchase> purchases = new List<Purchase>();
            StringBuilder sb = new StringBuilder();
            foreach (var purchaseDto in purchaseDtos)
            {
                if (!IsValid(purchaseDto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }
                Purchase purchase = new Purchase()
                {
                    Date = DateTime.ParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    ProductKey = purchaseDto.Key,
                    Type = Enum.Parse<PurchaseType>(purchaseDto.Type),
                    Card = context.Cards.FirstOrDefault(c => c.Number == purchaseDto.Card),
                    Game = context.Games.FirstOrDefault(g => g.Name == purchaseDto.Game)
                };
                purchases.Add(purchase);
                sb.AppendLine($"Imported {purchase.Game.Name} for {purchase.Card.User.Username}");
            }
            context.Purchases.AddRange(purchases);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}