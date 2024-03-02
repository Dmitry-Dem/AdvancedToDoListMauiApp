using AdvancedToDoListMauiApp.Models;

namespace AdvancedToDoListMauiApp.Services.Interfaces;

public interface IPunishmentChangesService
{
    Task<List<PunishmentChanges>> GetAllPunishmentChangesAsync();
    Task<PunishmentChanges?> GetPunishmentChangesByIdAsync(int Id);
    Task<int> AddPunishmentChangesAsync(PunishmentChanges item);
    Task<int> UpdatePunishmentChangesAsync(PunishmentChanges item);
    Task<int> DeletePunishmentChangesAsync(PunishmentChanges item);
}
