using AdvancedToDoListMauiApp.Services.Interfaces;
using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Data;

namespace AdvancedToDoListMauiApp.Services
{
    class PunishmentTypeService : IPunishmentTypeService
	{
		private readonly ApplicationDb _db = new();
        public async Task<List<PunishmentType>> GetAllPunishmentTypesAsync()
        {
            return await _db.GetAllAsync<PunishmentType>();
        }
        public async Task<PunishmentType?> GetPunishmentTypeByIdAsync(int Id)
        {
            return await _db.GetByIdAsync<PunishmentType>(Id);
        }
        public async Task<int> AddPunishmentTypeAsync(PunishmentType item)
        {
            return await _db.AddAsync(item);
        }
        public async Task<int> UpdatePunishmentTypeAsync(PunishmentType item)
        {
            return await _db.UpdateAsync(item);
        }
        public async Task<int> DeletePunishmentTypeAsync(PunishmentType item)
        {
            return await _db.DeleteAsync(item);
        }
    }
}
