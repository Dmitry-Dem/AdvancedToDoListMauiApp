using SQLite;

namespace AdvancedToDoListMauiApp.Models
{
	[Table("PunishmentPoints")]
	public class PunishmentPoint
    {
		[PrimaryKey, AutoIncrement, Column("Id")]
		public int Id { get; set; }
        public int Value { get; set; }
    }
}