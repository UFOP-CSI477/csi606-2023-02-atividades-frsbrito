using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atividade_pratica.Entities
{
    public class TipoSanguineo : BaseEntity
    {
        public string? Tipo { get; set; }
        public string? Fator { get; set; }
    }
}
