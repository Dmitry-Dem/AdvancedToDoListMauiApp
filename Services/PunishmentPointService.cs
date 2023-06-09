using AdvancedToDoListMauiApp.Data;
using AdvancedToDoListMauiApp.Interfaces;
using AdvancedToDoListMauiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedToDoListMauiApp.Services
{
    public class PunishmentPointService : IPunishmentPointService
	{
		private ApplicationDb _db = new ApplicationDb();
		public int GetPointValue()
		{
			var PunishPoint = GetFirstInstanceOrCreateNew();

			return PunishPoint.Value;
		}
		public void AddValue(int value)
		{
			var punishPoint = GetFirstInstanceOrCreateNew();

			punishPoint.Value += value;

			_db.UpdatePunishmentPoint(punishPoint);
		}
		private PunishmentPoint GetFirstInstanceOrCreateNew()
		{
			var PunishPoint = _db.GetAllPunishmentPoints().FirstOrDefault();

			if (PunishPoint == null) // Create new PP
			{
				var newPunishPoint = new PunishmentPoint
				{
					Value = 0
				};

				_db.AddNewPunishmentPoint(newPunishPoint);

				return newPunishPoint;
			}

			return PunishPoint;
		}
    }
}
