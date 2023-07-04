using AdvancedToDoListMauiApp.Args;
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
		public static event EventHandler<PunishmentPointValueChangedEventArgs> PunishmentPointChanged;
		
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

			PunishmentPointValueChanged(new PunishmentPointValueChangedEventArgs("Value changed!", value));
		}
		private PunishmentPoint GetFirstInstanceOrCreateNew()
		{
			var PunishPoint = _db.GetAllPunishmentPoints().FirstOrDefault();

			if (PunishPoint == null)
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
		private void PunishmentPointValueChanged(PunishmentPointValueChangedEventArgs e)
		{
			EventHandler<PunishmentPointValueChangedEventArgs> temp = Volatile.Read(ref PunishmentPointChanged);

			if (temp != null) temp(this, e);
		}
	}
}
