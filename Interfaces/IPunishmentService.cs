using AdvancedToDoListMauiApp.Models;

namespace AdvancedToDoListMauiApp.Services
{
	internal interface IPunishmentService
	{
		List<Punishment> GetAllPunishments();
		Punishment GetPunishmentById(int Id);
		void AddNewPunishment(Punishment punishment);
		void UpdatePunishment(Punishment punishment);
		void DeletePunishment(Punishment punishment);
		Punishment DecreasePunishmentValueById(int Id);
	}
}