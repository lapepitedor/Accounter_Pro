using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounter.Models;

namespace Accounter.Services
{
    public interface IDefektService
    {
        Task<List<Defekt>> GetDefektList();
        Task AddDefekt(Defekt defekt);
        Task UpdateDefekt(Defekt defekt);
        Task DeleteDefekt(Defekt defekt);
        Task DeleteAllDefekte();
    }
}
