using AdvancedToDoListMauiApp.Models;

namespace AdvancedToDoListMauiApp.Interfaces
{
	public interface IPunishmentTypeService
	{
		public List<PunishmentType> GetAllPunishmentTypes();
		public PunishmentType GetPunishmentTypeById(int Id);
		public void AddNewPunishmentType(PunishmentType punishmentType);
		public void UpdatePunishmentType(PunishmentType punishmentType);
		public void DeletePunishmentType(PunishmentType punishmentType);
	}
}