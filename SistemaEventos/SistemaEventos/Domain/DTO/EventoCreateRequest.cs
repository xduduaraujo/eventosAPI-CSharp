using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEventos.Domain.DTO
{
    public class EventoCreateRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Preencha o nome corretamente.")]
        [MaxLength(250, ErrorMessage = "O nome deve possuir no máximo 250 caracteres.")]
        public string Nome { get; set; }
        [Required]
        public DateTime DataHoraInicio { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataHoraFim { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Preencha o local corretamente.")]
        [MaxLength(250, ErrorMessage = "O local deve possuir no máximo 250 caracteres.")]
        public string Local { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Preencha a descrição corretamente.")]
        [MaxLength(1000, ErrorMessage = "A descrição deve possuir no máximo 1000 caracteres.")]
        public string Descricao { get; set; }
        [Required]
        public int? LimiteVagas { get; set; }
        [Required]
        public int? Categoria { get; set; }
    }
}
