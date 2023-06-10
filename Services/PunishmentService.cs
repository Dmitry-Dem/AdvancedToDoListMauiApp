using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedToDoListMauiApp.Data;
using AdvancedToDoListMauiApp.Models;

namespace AdvancedToDoListMauiApp.Services
{
    class PunishmentService : IPunishmentService
	{
        private ApplicationDb _db = new ApplicationDb();
        public List<Punishment> GetAllPunishments()
        {
            return _db.GetAllPunishments();
        }
		public Punishment GetPunishmentById(int Id)
		{
			return _db.GetPunishmentById(Id);
		}
		public void AddNewPunishment(Punishment punishment)
		{
			_db.AddNewPunishment(punishment);
		}
		public void UpdatePunishment(Punishment punishment)
		{
			_db.UpdatePunishment(punishment);
		}
		public void DeletePunishment(Punishment punishment)
		{
			_db.DeletePunishment(punishment);
		}
		public Punishment DecreasePunishmentValueById(int Id)
        {
            var punish = _db.GetPunishmentById(Id);

			punish.Value += (punish.ValueDecreaser < 0) ? punish.ValueDecreaser : punish.ValueDecreaser * -1;

			_db.UpdatePunishment(punish);

			return punish;
        }
	}
}
