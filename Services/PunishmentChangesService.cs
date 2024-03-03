using AdvancedToDoListMauiApp.Services.Interfaces;
using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Data;
using AdvancedToDoListMauiApp.Models.DTOs;
using System;

namespace AdvancedToDoListMauiApp.Services
{
    public class PunishmentChangesService : IPunishmentChangesService
    {
        private readonly ApplicationDb _db = new();
        public async Task<List<PunishmentChanges>> GetAllPunishmentChangesAsync()
        {
            return await _db.GetAllAsync<PunishmentChanges>();
        }
        public async Task<List<PunishmentLogDto>> GetPunishmentLogsAsync(TimeSpan timeSpan)
        {
            var list = await GetAllPunishmentChangesAsync();

            DateTime startDate = DateTime.Now - timeSpan;

            list = list.Where(p => p.CreatedAt >= startDate && p.CreatedAt <= DateTime.Now && p.ValueIncreased).ToList();

            var groups = list.GroupBy(p => p.Name);

            var logs = new List<PunishmentLogDto>();

            foreach (var group in groups)
            {
                var log = new PunishmentLogDto()
                {
                    Name = group.Key,
                    Amount = 0
                };

                foreach (var change in group)
                    log.Amount += change.Value;

                logs.Add(log);
            }

            return logs;
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
