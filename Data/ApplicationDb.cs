using AdvancedToDoListMauiApp.Models;
using SQLite;

namespace AdvancedToDoListMauiApp.Data
{
	public class ApplicationDb
	{
		private SQLiteConnection _conn;
        public ApplicationDb()
        {
			Init();
        }
        public void Init()
        {
            _conn = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);

            _conn.CreateTable<Punishment>();
            _conn.CreateTable<PunishmentPoint>();
            _conn.CreateTable<PunishmentType>();
            _conn.CreateTable<UserTask>();
        }
        public List<Punishment> GetAllPunishments()
		{
            return _conn.Table<Punishment>().ToList();
		}
        public void AddNewPunishment(Punishment punishment)
        {
            _conn.Insert(punishment);
        }
		public List<PunishmentPoint> GetAllPunishmentPoints()
		{
			return _conn.Table<PunishmentPoint>().ToList();
		}
		public void AddNewPunishmentPoint(PunishmentPoint punishmentPoint)
		{
			_conn.Insert(punishmentPoint);
		}
        public void UpdatePunishmentPoint(PunishmentPoint punishmentPoint)
        {
            _conn.Update(punishmentPoint);
        }
	}
}
