using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPG.WebApi.Dto
{
    public class RickDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="O Nome é obrigatório")]
        [StringLength(100, ErrorMessage ="O campo Nome não pode exceder 100 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage ="O Descrição é obrigatório")]
        [StringLength(255, ErrorMessage ="O campo Descrição não pode exceder 255 caracteres")]
        public string Description { get; set; }

        public string Image { get; set; }
        
        public int QI { get; set; }

        public MortyDto Morty { get; set; }
        
        public List<DimensionDto> Dimensions { get; set; }
    }
}