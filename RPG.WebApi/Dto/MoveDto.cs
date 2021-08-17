using System.ComponentModel.DataAnnotations;

namespace RPG.WebApi.Dto
{
    public class MoveDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="O Nome é obrigatório")]
        [StringLength(100, ErrorMessage ="O campo Nome não pode exceder 100 caracteres")]
        public string Name { get; set; }

        public int Usage { get; set; }

        public int Type { get; set; }

        public string Category { get; set; }

        public int Accurace { get; set; }

        public int Damage { get; set; }
    }
}