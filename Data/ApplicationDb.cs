using AdvancedToDoListMauiApp.Models;
using SQLite;

namespace AdvancedToDoListMauiApp.Data
{
	public class ApplicationDb
	{
		private readonly SQLiteAsyncConnection _conn;
        public ApplicationDb()
        {
            _conn = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            Init();
        }
        protected void Init()
        {
            _conn.CreateTableAsync<Punishment>();
            _conn.CreateTableAsync<PunishmentChanges>();
            _conn.CreateTableAsync<PunishmentPoint>();
            _conn.CreateTableAsync<PunishmentType>();
            _conn.CreateTableAsync<UserRule>();
            _conn.CreateTableAsync<TaskGroup>();
            _conn.CreateTableAsync<UserTask>();
        }

		public async Task<List<T>> GetAllAsync<T>() where T : BaseEntity, new()
		{
			return await _conn.Table<T>().ToListAsync();
		}
        public async Task<T?> GetByIdAsync<T>(int Id) where T : BaseEntity, new()
        {
            var list = await GetAllAsync<T>();

            return list.FirstOrDefault(p => p.Id == Id);
        }
        public async Task<int> AddAsync<T>(T entity) where T : BaseEntity, new()
        {
            return await _conn.InsertAsync(entity);
        }
        public async Task<int> UpdateAsync<T>(T entity) where T : BaseEntity, new()
        {
            return await _conn.UpdateAsync(entity);
        }
		public async Task<int> DeleteAsync<T>(T entity) where T : BaseEntity, new()
        {
            return await _conn.DeleteAsync(entity);
		}
	}
}
