using SQLiteNetExtensions.Attributes;
using SQLite;

namespace AdvancedToDoListMauiApp.Models;

[Table("PunishmentChanges")]
public class PunishmentChanges : BaseEntity
{
    public int Value { get; set; }
    public bool ValueIncreased { get; set; } = true;
    [ForeignKey(typeof(Punishment))]
    public int PunishmentId { get; set; }
}
