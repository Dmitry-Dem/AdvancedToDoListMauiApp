using SQLite;

namespace AdvancedToDoListMauiApp.Models
{
	[Table("PunishmentPoints")]
	public class PunishmentPoint : BaseEntity
    {
        public int Value { get; set; }
    }
}