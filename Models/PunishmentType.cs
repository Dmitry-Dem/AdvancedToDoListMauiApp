using SQLite;

namespace AdvancedToDoListMauiApp.Models
{
	[Table("PunishmentTypes")]
	public class PunishmentType : BaseEntity
    {
		public int Value { get; set; }
		public int PunishmentPoint { get; set; }
		public string Description { get; set; } = null!;
	}
}