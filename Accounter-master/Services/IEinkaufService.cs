using Accounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounter.Services
{
    public interface IEinkaufService
    {
        Task<List<Einkauf>> GetEinkaufsList();
        Task AddEinkauf(Einkauf einkauf);
        Task UpdateEinkauf(Einkauf einkauf);
        Task DeleteEinkauf(Einkauf einkauf);
        Task DeleteAllEinkaeufe();
    }
}
