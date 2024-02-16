using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atividade_pratica.Entities
{
    public class Doacao : BaseEntity
    {
        [ForeignKey("Pessoa")]
        public int PessoaId { get; set; }
        public Pessoa? Pessoa { get; set; }
        [ForeignKey("LocalColeta")]
        public int LocalId { get; set; }
        public LocalColeta? LocalColeta { get; set; }
        public DateTime Data { get; set; }
    }
}
