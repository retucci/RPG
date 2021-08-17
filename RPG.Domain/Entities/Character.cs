using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPG.Domain.Entities
{
    public class Character : Status
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required(AllowEmptyStrings = false)]
        public string Nickname { get; set; }

        [Column(TypeName = "varchar(1)")]
        [Required(AllowEmptyStrings = false)]
        public string Gender { get; set; }

        public int MainType { get; set; }

        public int? SecondaryType { get; set; }

        public string Image { get; set; }

        public List<Weapon> Weapons { get; set; }

        public List<Move> Moves { get; set; }
    }
}