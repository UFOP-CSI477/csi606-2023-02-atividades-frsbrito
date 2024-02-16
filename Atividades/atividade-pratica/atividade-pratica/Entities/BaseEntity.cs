using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atividade_pratica.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime? Updated_at { get; set; }
        public DateTime? Deleted_at { get; set; }

    }
}
