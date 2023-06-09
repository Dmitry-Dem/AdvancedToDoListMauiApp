using SQLite;

namespace AdvancedToDoListMauiApp.Models
{
    [Table("Punishments")]
    public class Punishment : PunishmentDto
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
    }
}