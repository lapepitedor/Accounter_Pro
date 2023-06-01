using Accounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Accounter.Services
{
    public class EinkaufService : IEinkaufService
    {
        static SQLiteAsyncConnection dbConnection;
        public EinkaufService()
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
            await dbConnection.CreateTableAsync<Einkauf>();
        }

        public async Task AddEinkauf(Einkauf einkauf)
        {
            await Init();
            await dbConnection.InsertAsync(einkauf);
        }

        public async Task DeleteEinkauf(Einkauf einkauf)
        {
            await Init();
            await dbConnection.DeleteAsync(einkauf);
        }

        public async Task<List<Einkauf>> GetEinkaufsList()
        {
            await Init();
            return await dbConnection.Table<Einkauf>().ToListAsync();
        }

        public async Task UpdateEinkauf(Einkauf einkauf)
        {
            await Init();
            await dbConnection.UpdateAsync(einkauf);
        }

        public async Task DeleteAllEinkaeufe()
        {
            await Init();
            await dbConnection.DeleteAllAsync<Einkauf>();
        }
    }
}
