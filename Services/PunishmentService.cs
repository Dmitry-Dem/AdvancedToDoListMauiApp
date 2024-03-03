using AdvancedToDoListMauiApp.Services.Interfaces;
using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Data;

namespace AdvancedToDoListMauiApp.Services
{
    class PunishmentService : IPunishmentService
	{
		private readonly IPunishmentChangesService _punishmentChangesService = new PunishmentChangesService();

        private readonly ApplicationDb _db = new();
        public async Task<List<Punishment>> GetAllPunishmentsAsync()
        {
            return await _db.GetAllAsync<Punishment>();
        }
        public async Task<Punishment?> GetPunishmentByIdAsync(int Id)
        {
            return await _db.GetByIdAsync<Punishment>(Id);
        }
        public async Task<Punishment?> GetPunishmentByTypeIdAsync(int Id)
        {
            var list = await GetAllPunishmentsAsync().ConfigureAwait(false);

            return list.FirstOrDefault(p => p.PunishmentTypeId == Id);
        }
        public async Task<int> AddPunishmentAsync(Punishment item)
        {
            return await _db.AddAsync(item);
        }
        public async Task<int> UpdatePunishmentAsync(Punishment item)
        {
            return await _db.UpdateAsync(item);
        }
        public async Task<int> DeletePunishmentAsync(Punishment item)
        {
            return await _db.DeleteAsync(item);
        }
        public async Task<Punishment?> DecreasePunishmentValueByIdAsync(int Id)
        {
            var punish = await _db.GetByIdAsync<Punishment>(Id);

            if (punish == null)
                return default;

            var value = (punish.ValueDecreaser < 0) ? punish.ValueDecreaser : punish.ValueDecreaser * -1;

            punish.Value += value;

            var change = new PunishmentChanges()
            {
                Name = punish.Description,
                PunishmentId = punish.Id,
                Value = value * -1,
                ValueIncreased = true
            };

            var result = await _db.UpdateAsync(punish);

            if (result > 0)
                await _punishmentChangesService.AddPunishmentChangesAsync(change);

            return punish;
        }
    }
}
