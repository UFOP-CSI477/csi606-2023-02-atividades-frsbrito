using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atividade_pratica.Entities
{
    public class Pessoa : BaseEntity
    {
        public string? Nome { get; set; }
        public string? Rua { get; set; }
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? RG { get; set; }

        [ForeignKey("Cidade")]
        public int CidadeId { get; set; }
        public Cidade? Cidade { get; set; }

        [ForeignKey("TipoSanguineo")]
        public int TipoId { get; set; }
        public TipoSanguineo? TipoSanguineo { get; set; }

        internal DateTime Data_Hora_Brasilia()
        {
            throw new NotImplementedException();
        }
    }
}
