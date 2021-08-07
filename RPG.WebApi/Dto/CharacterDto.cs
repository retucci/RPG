using System.Collections.Generic;
namespace RPG.WebApi.Dto
{
    public class CharacterDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public int Level { get; set; }

        public int ExperiencePoints { get; set; }

        public int HitPoints { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public int SpecialAttack { get; set; }

        public int SpecialDefense { get; set; }

        public int Speed { get; set; }

        public List<WeaponDto> Weapons { get; set; }

        public List<MoveDto> Moves { get; set; }
    }
}