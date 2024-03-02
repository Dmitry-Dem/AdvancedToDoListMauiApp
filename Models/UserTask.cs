using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AdvancedToDoListMauiApp.Models
{
	[Table("UserTasks")]
	public class UserTask : BaseEntity
    {
        public string Description { get; set; } = null!;
        public bool IsDone { get; set; }
        public int PunishmentPoint { get; set; }
        [ForeignKey(typeof(TaskGroup))]
        public int TaskGroupId { get; set; }
    }
}