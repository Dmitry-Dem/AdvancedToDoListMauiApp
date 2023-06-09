namespace AdvancedToDoListMauiApp.Interfaces
{
    public interface IPunishmentPointService
    {
        int GetPointValue();
        void AddValue(int value);
    }
}