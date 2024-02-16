using Caderno_de_Notas_NoSQL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caderno_de_Notas_NoSQL.Controllers
{
    public interface INotasAPIController
    {
        Task<Nota> Get(string id);
        Task<IEnumerable<Nota>> GetAll();
        Task<Nota> Create(Nota Nota);
        Task<Nota> Update(string id, Nota Nota);

        Task<bool> Delete(string id);
    }
}

