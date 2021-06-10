using System;
using System.Collections.Generic;

#nullable disable

namespace SistemaEventos.Domain.Entity
{
    public partial class CategoriaEvento
    {
        public CategoriaEvento()
        {
            Eventos = new HashSet<Evento>();
        }

        public int IdCategoriaEvento { get; set; }
        public string NomeCategoria { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; }
    }
}
