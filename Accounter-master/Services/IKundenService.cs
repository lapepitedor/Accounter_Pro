using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounter.Models;

namespace Accounter.Services
{
    public interface IKundenService
    {
        Task<List<Kunde>> GetKundenList();
        Task AddKunde(Kunde kunde);
        Task UpdateKunde(Kunde kunde);
        Task DeleteKunde(Kunde kunde);
        Task DeleteAllKunden();
    }
}
