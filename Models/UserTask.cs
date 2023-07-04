using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AdvancedToDoListMauiApp.Models
{
	[Table("UserTasks")]
	public class UserTask
    {
		[PrimaryKey, AutoIncrement, Column("Id")]
		public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public int PunishmentPoint { get; set; }
        [ForeignKey(typeof(TaskGroup))]
        public int TaskGroupId { get; set; }
    }
}