using AdvancedToDoListMauiApp.Models;
using AdvancedToDoListMauiApp.Models.DTOs;

namespace AdvancedToDoListMauiApp.Services.Interfaces;

public interface IPunishmentChangesService
{
    Task<List<PunishmentChanges>> GetAllPunishmentChangesAsync();
    Task<List<PunishmentLogDto>> GetPunishmentLogsAsync(TimeSpan timeSpan);
    Task<PunishmentChanges?> GetPunishmentChangesByIdAsync(int Id);
    Task<int> AddPunishmentChangesAsync(PunishmentChanges item);
    Task<int> UpdatePunishmentChangesAsync(PunishmentChanges item);
    Task<int> DeletePunishmentChangesAsync(PunishmentChanges item);
}
