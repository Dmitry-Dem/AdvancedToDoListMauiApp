using AdvancedToDoListMauiApp.Models;

namespace AdvancedToDoListMauiApp.Services.Interfaces;

public interface IPunishmentService
{
    Task<List<Punishment>> GetAllPunishmentsAsync();
    Task<Punishment?> GetPunishmentByIdAsync(int Id);
    Task<Punishment?> GetPunishmentByTypeIdAsync(int Id);
    Task<int> AddPunishmentAsync(Punishment item);
    Task<int> UpdatePunishmentAsync(Punishment item);
    Task<int> DeletePunishmentAsync(Punishment item);
    Task<Punishment?> DecreasePunishmentValueByIdAsync(int Id);
}