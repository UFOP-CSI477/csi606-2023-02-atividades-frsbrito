using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atividade_pratica.Entities
{
    public class Cidade : BaseEntity
    {
        public string? Nome { get; set; }

        [ForeignKey("Estado")]
        public int EstadoId { get; set; }
        public Estado? Estado { get; set; }
    }
}
