using AdvancedToDoListMauiApp.Services.Interfaces;
using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Args;
using AdvancedToDoListMauiApp.Data;

namespace AdvancedToDoListMauiApp.Services
{
    public class PunishmentPointService : IPunishmentPointService
	{
		public static event EventHandler<PunishmentPointValueChangedEventArgs> PunishmentPointChanged = default!;
		
		private readonly ApplicationDb _db = new();
		public async Task<int> GetPointValueAsync()
		{
			var PunishPoint = await GetFirstInstanceOrCreateNewAsync();

			return PunishPoint.Value;
		}
        public async Task<int> AddValueAsync(int value)
		{
			var punishPoint = await GetFirstInstanceOrCreateNewAsync();

			punishPoint.Value += value;

			var result = await _db.UpdateAsync(punishPoint);

			if (result > 0)
				PunishmentPointValueChanged(new PunishmentPointValueChangedEventArgs("Value changed!", punishPoint.Value, value));

			return result;
        }
		private async Task<PunishmentPoint> GetFirstInstanceOrCreateNewAsync()
		{
			var PunishPoint = (await _db.GetAllAsync<PunishmentPoint>().ConfigureAwait(false)).FirstOrDefault();

			if (PunishPoint == null)
			{
				var newPunishPoint = new PunishmentPoint
				{
					Value = 0
				};

				await _db.AddAsync(newPunishPoint);

				return newPunishPoint;
			}

			return PunishPoint;
		}
		private void PunishmentPointValueChanged(PunishmentPointValueChangedEventArgs e)
		{
			var temp = Volatile.Read(ref PunishmentPointChanged);

            temp(this, e);
		}
	}
}
