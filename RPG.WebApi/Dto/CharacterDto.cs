using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPG.WebApi.Dto
{
    public class CharacterDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="O Nome é obrigatório")]
        [StringLength(100, ErrorMessage ="O campo Nome não pode exceder 100 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage ="O Apelido é obrigatório")]
        [StringLength(100, ErrorMessage ="O campo Apelido não pode exceder 100 caracteres")]
        public string NickName { get; set; }

        [Required(ErrorMessage ="O Gênero é obrigatório")]
        [StringLength(1, ErrorMessage ="O campo Gênero não pode exceder 1 caracteres")]
        public string Gender { get; set; }

        [Required(ErrorMessage ="O Tipo é obrigatório")]
        public int MainType { get; set; }
        
        public int? SecondaryType { get; set; }

        public string Image { get; set; }

        public int Level { get; set; }

        public int ExperiencePoints { get; set; }

        public int TotalExperiencePoints { get; set; }

        public int HitPoints { get; set; }

        public int TotalHitPoints { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public int SpecialAttack { get; set; }

        public int SpecialDefense { get; set; }

        public int Speed { get; set; }

        public List<WeaponDto> Weapons { get; set; }

        public List<MoveDto> Moves { get; set; }
    }
}