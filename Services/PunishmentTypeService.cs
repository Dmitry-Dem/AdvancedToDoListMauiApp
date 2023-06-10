using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedToDoListMauiApp.Data;
using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Interfaces;

namespace AdvancedToDoListMauiApp.Services
{
    class PunishmentTypeService : IPunishmentTypeService
	{
		private ApplicationDb _db = new ApplicationDb();
		public List<PunishmentType> GetAllPunishmentTypes()
		{
			return _db.GetAllPunishmentTypes();
		}
		public PunishmentType GetPunishmentTypeById(int Id)
		{
			return _db.GetPunishmentTypeById(Id);
		}
		public void AddNewPunishmentType(PunishmentType punishmentType)
		{
			_db.AddNewPunishmentType(punishmentType);
		}
		public void UpdatePunishmentType(PunishmentType punishmentType)
		{
			_db.UpdatePunishmentType(punishmentType);
		}
		public void DeletePunishmentType(PunishmentType punishmentType)
		{
			_db.DeletePunishmentType(punishmentType);
		}
	}
}
