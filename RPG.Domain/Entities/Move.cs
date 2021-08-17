using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPG.Domain.Entities
{
    public class Move  : Effect
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public int Type { get; set; }

        [Column(TypeName = "varchar(1)")]
        [Required(AllowEmptyStrings = false)]
        public string Category { get; set; }

        public int Usage { get; set; }

        public int Accurace { get; set; }
    }
}