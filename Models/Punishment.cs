using SQLite;

namespace AdvancedToDoListMauiApp.Models
{
    [Table("Punishments")]
    public class Punishment
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
		public int Value { get; set; }
        public int ValueDecreaser { get; set; }
        public string Description { get; set; }
        public int PunishmentTypeId { get; set; }
    }
}