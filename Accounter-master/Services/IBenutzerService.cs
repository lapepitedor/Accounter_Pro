using Accounter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounter.Services
{
    public interface IBenutzerService
    {
        Task<List<Benutzer>> GetBenutzerList();
        Task AddBenutzer(Benutzer benutzer);
        Task UpdateBenutzer(Benutzer benutzer);
        Task DeleteBenutzer(Benutzer benutzer);
    }
}
