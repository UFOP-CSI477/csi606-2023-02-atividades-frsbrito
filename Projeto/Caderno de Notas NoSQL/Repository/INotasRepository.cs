using System;
using Caderno_de_Notas_NoSQL.Models;

namespace Caderno_de_Notas_NoSQL.Repository
{
    public interface INotasRepository
    {
        Task<Nota> Get(string id);
        Task<IEnumerable<Nota>> ListarAsync(string query);
        Task<IEnumerable<Nota>> GetAll();
        Task<bool> InserirNota(Nota value);
        Task<bool> Update(Nota value);
        Task<bool> Delete(Nota value);
    }
}

