using Accounter.Models;
using Accounter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounter.Services
{
    public interface IArtikelService
    {
        Task<List<Artikel>> GetArtikelList();
        Task AddArtikel(Artikel artikel);
        Task UpdateArtikel(Artikel artikel);
        Task DeleteArtikel(Artikel artikel);
        Task DeleteAllArtikels();
    }
}
