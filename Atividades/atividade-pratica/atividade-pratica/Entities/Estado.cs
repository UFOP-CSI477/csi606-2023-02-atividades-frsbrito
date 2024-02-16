using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atividade_pratica.Entities
{
    public class Estado : BaseEntity
    {
        public string? Nome { get; set; }
        public string? Sigla { get; set; }
    }
}
