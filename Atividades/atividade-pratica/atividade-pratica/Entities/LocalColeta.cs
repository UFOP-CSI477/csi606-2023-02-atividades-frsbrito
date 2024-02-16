using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atividade_pratica.Entities
{
    public class LocalColeta : BaseEntity
    {
        public string? Nome { get; set; }
        public string? Rua { get; set; }
        public string? Numero { get; set; }
        public string? Complemento { get; set; }

        [ForeignKey("Cidade")]
        public int CidadeId { get; set; }
        public Cidade? Cidade { get; set; }
    }
}
