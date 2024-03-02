using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedToDoListMauiApp.Models
{
    [Table("TaskGroups")]
	public class TaskGroup : BaseEntity
    {
        public string Name { get; set; } = null!;

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<UserTask> Tasks { get; set; } = new List<UserTask>();
    }
}
