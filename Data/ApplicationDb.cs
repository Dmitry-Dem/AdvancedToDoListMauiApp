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
            _conn.CreateTable<UserRule>();
            _conn.CreateTable<UserTask>();
        }
        //Punishment methods
        public List<Punishment> GetAllPunishments()
		{
            return _conn.Table<Punishment>().ToList();
		}
        public Punishment GetPunishmentById(int Id)
        {
            var punishList = GetAllPunishments();

            return punishList.FirstOrDefault(p => p.Id == Id);
        }
        public void AddNewPunishment(Punishment punishment)
        {
            _conn.Insert(punishment);
        }
        public void UpdatePunishment(Punishment punishment)
        {
            _conn.Update(punishment);
        }
		public void DeletePunishment(Punishment punishment)
		{
			_conn.Delete(punishment);
		}
		//Punishment Point methods
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
		//Punishment Types methods
        public List<PunishmentType> GetAllPunishmentTypes()
        {
            return _conn.Table<PunishmentType>().ToList();
        }
		public PunishmentType GetPunishmentTypeById(int Id)
		{
			var punishTypesList = GetAllPunishmentTypes();

			return punishTypesList.FirstOrDefault(pt => pt.Id == Id);
		}
		public void AddNewPunishmentType(PunishmentType punishmentType)
		{
			_conn.Insert(punishmentType);
		}
		public void UpdatePunishmentType(PunishmentType punishmentType)
		{
			_conn.Update(punishmentType);
		}
		public void DeletePunishmentType(PunishmentType punishmentType)
		{
			_conn.Delete(punishmentType);
		}
		//User Rules methods
		public List<UserRule> GetAllUserRules()
		{
			return _conn.Table<UserRule>().ToList();
		}
		public UserRule GetUserRuleById(int Id)
		{
			var userRulesList = GetAllUserRules();

			return userRulesList.FirstOrDefault(r => r.Id == Id);
		}
		public void AddNewUserRule(UserRule userRule)
		{
			_conn.Insert(userRule);
		}
		public void UpdateUserRule(UserRule userRule)
		{
			_conn.Update(userRule);
		}
		public void DeleteUserRule(UserRule userRule)
		{
			_conn.Delete(userRule);
		}
	}
}
