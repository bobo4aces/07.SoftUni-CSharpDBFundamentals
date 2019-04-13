namespace VaporStore.Data.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class Tag
	{
		public Tag()
		{
		}

		public Tag(string name)
		{
			this.Name = name;
		}

		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public ICollection<GameTag> GameTags { get; set; }
	}
}