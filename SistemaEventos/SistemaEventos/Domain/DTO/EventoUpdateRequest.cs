using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEventos.Domain.DTO
{
    public class EventoUpdateRequest
    {
        [Required]
        public int? EventoStatus { get; set; }
    }
}
