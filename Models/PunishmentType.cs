using SQLite;

namespace AdvancedToDoListMauiApp.Models
{
	[Table("PunishmentTypes")]
	public class PunishmentType
    {
		[PrimaryKey, AutoIncrement, Column("Id")]
		public int Id { get; set; }
		public int Value { get; set; }
		public int PunishmentPoint { get; set; }
		public string Description { get; set; }
	}
}