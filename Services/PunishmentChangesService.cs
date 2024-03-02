using AdvancedToDoListMauiApp.Services.Interfaces;
using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Data;

namespace AdvancedToDoListMauiApp.Services
{
    public class PunishmentChangesService : IPunishmentChangesService
    {
        private readonly ApplicationDb _db = new();
        public async Task<List<PunishmentChanges>> GetAllPunishmentChangesAsync()
        {
            return await _db.GetAllAsync<PunishmentChanges>();
        }
        public async Task<PunishmentChanges?> GetPunishmentChangesByIdAsync(int Id)
        {
            return await _db.GetByIdAsync<PunishmentChanges>(Id);
        }
        public async Task<int> AddPunishmentChangesAsync(PunishmentChanges item)
        {
            return await _db.AddAsync(item);
        }
        public async Task<int> UpdatePunishmentChangesAsync(PunishmentChanges item)
        {
            return await _db.UpdateAsync(item);
        }
        public async Task<int> DeletePunishmentChangesAsync(PunishmentChanges item)
        {
            return await _db.DeleteAsync(item);
        }
    }
}
