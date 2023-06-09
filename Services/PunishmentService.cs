using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedToDoListMauiApp.Data;
using AdvancedToDoListMauiApp.Models;

namespace AdvancedToDoListMauiApp.Services
{
    class PunishmentService : IPunishmentService
	{
        private ApplicationDb _db = new ApplicationDb();
        public List<Punishment> GetAllPunishments()
        {
            return _db.GetAllPunishments();
        }
    }
}
