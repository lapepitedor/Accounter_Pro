using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounter.Models;
using SQLite;

namespace Accounter.Services
{
    class DefektService : IDefektService
    {
        private SQLiteAsyncConnection dbConnection;

        public DefektService()
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
            await dbConnection.CreateTableAsync<Defekt>();
        }
        public async Task AddDefekt(Defekt defekt)
        {
            await Init();
            await dbConnection.InsertAsync(defekt);
        }

        public async Task DeleteAllDefekte()
        {
            await Init();
            await dbConnection.DeleteAllAsync<Defekt>();
        }

        public async Task DeleteDefekt(Defekt defekt)
        {
            await Init();
            await dbConnection.DeleteAsync(defekt);
        }

        public async Task<List<Defekt>> GetDefektList()
        {
            await Init();
            return await dbConnection.Table<Defekt>().ToListAsync();
        }

        public async Task UpdateDefekt(Defekt defekt)
        {
            await Init();
            await dbConnection.UpdateAsync(defekt);
        }
    }
}
