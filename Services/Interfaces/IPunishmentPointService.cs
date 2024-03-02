namespace AdvancedToDoListMauiApp.Services.Interfaces;

public interface IPunishmentPointService
{
    Task<int> GetPointValueAsync();
    Task<int> AddValueAsync(int value);
}