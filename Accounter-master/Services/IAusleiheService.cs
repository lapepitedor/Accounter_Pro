using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounter.Models;

namespace Accounter.Services
{
    public interface IAusleiheService
    {
        Task<List<Ausleihe>> GetAusleiheList();
        Task AddAusleihe(Ausleihe artikel);
        Task UpdateAusleihe(Ausleihe artikel);
        Task DeleteAusleihe(Ausleihe artikel);
        Task DeleteAllAusleihe();
    }
}
