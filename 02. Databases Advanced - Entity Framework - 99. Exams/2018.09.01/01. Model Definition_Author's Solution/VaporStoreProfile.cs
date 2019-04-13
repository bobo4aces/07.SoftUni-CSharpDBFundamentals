namespace VaporStore
{
	using AutoMapper;
	using System.Linq;
	using Data.Models.Enums;
	using Data.Models;
	using DataProcessor.Dto.Export;

	public class VaporStoreProfile : Profile
	{
		// Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
		public VaporStoreProfile()
		{
			CreateMap<Genre, GenreDto>()
				.ForMember(dto => dto.Genre,
					opt => opt.MapFrom(genre => genre.Name))
				.ForMember(dto => dto.TotalPlayers,
					opt => opt.MapFrom(genre => genre.Games.Sum(g => g.Purchases.Count)))
				.ForMember(dto => dto.Games,
					opt => opt.MapFrom(genre =>
						genre.Games
							.Where(g => g.Purchases.Any())
							.OrderByDescending(game => game.Purchases.Count)
							.ThenBy(game => game.Id)));

			CreateMap<Game, GameDto>()
				.ForMember(dto => dto.Title,
					opt => opt.MapFrom(g => g.Name))
				.ForMember(dto => dto.Developer,
					opt => opt.MapFrom(g => g.Developer.Name))
				.ForMember(dto => dto.Tags,
					opt => opt.MapFrom(game => string.Join(", ", game.GameTags.Select(g => g.Tag.Name))))
				.ForMember(dto => dto.Players,
					opt => opt.MapFrom(game => game.Purchases.Count));
		}
	}
}