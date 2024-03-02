using SQLite;

namespace AdvancedToDoListMauiApp.Models
{
    [Table("Punishments")]
    public class Punishment : BaseEntity
    {
		public int Value { get; set; }
        public int ValueDecreaser { get; set; }
        public string Description { get; set; } = null!;
        public int PunishmentTypeId { get; set; }
    }
}