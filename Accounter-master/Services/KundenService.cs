using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounter.Models;
using SQLite;

namespace Accounter.Services
{
    public class KundenService : IKundenService
    {
        private SQLiteAsyncConnection dbConnection;
        public KundenService()
        {
            _ = Init();
        }

        private async Task Init()
        {
            if (dbConnection != null)
            {
                return;
            }
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Accounter.db");
            dbConnection = new SQLiteAsyncConnection(dbPath);
            await dbConnection.CreateTableAsync<Kunde>();
        }
        public async Task AddKunde(Kunde kunde)
        {
            await Init();
            await dbConnection.InsertAsync(kunde);
        }

        public async Task DeleteAllKunden()
        {
            await Init();
            await dbConnection.DeleteAllAsync<Kunde>();
        }

        public async Task DeleteKunde(Kunde kunde)
        {
            await Init();
            await dbConnection.DeleteAsync(kunde);
        }

        public async Task<List<Kunde>> GetKundenList()
        {
            await Init();
            return await dbConnection.Table<Kunde>().ToListAsync();
        }

        public async Task UpdateKunde(Kunde kunde)
        {
            await Init();
            await dbConnection.UpdateAsync(kunde);
        }
    }
}
