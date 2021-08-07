using System.Collections.Generic;
namespace RPG.Domain.Entities
{
    public class Character : Status
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public List<Weapon> Weapons { get; set; }

        public List<Move> Moves { get; set; }
    }
}