using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RPG.Domain.Entities;

namespace RPG.WebApi.Dto
{
    public class DimensionDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="O Código é obrigatório")]
        [StringLength(4, ErrorMessage ="O campo Código não pode exceder 4 caracteres")]
        public string Code  { get; set; }

        [Required(ErrorMessage ="É obrigatório informar se essa é a dimensão Original")]
        public int Original  { get; set; }

        [Required(ErrorMessage ="É obrigatório informar a data da viagem")]
        public DateTime TravelDate  { get; set; }

        public int RickId { get; set; }

        public RickDto Rick { get; set; }
    }
}