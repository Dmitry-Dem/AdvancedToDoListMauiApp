using AdvancedToDoListMauiApp.Models;

namespace AdvancedToDoListMauiApp.Services.Interfaces;

public interface IPunishmentTypeService
{
    Task<List<PunishmentType>> GetAllPunishmentTypesAsync();
    Task<PunishmentType?> GetPunishmentTypeByIdAsync(int Id);
    Task<int> AddPunishmentTypeAsync(PunishmentType item);
    Task<int> UpdatePunishmentTypeAsync(PunishmentType item);
    Task<int> DeletePunishmentTypeAsync(PunishmentType item);
}