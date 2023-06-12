using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedToDoListMauiApp.Models
{
	[Table("UserRules")]
	public class UserRule
    {
		[PrimaryKey, AutoIncrement, Column("Id")]
		public int Id { get; set; }
        public string Name { get; set; }
        public int PunishmentPoint { get; set; }
    }
}
