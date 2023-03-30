using System;
using System.Collections.Generic;

namespace devTicket.Models
{
    public partial class Evento
    {
        public Evento()
        {
            Entrada = new HashSet<Entrada>();
        }

        public int Id { get; set; }
        public int Disponible { get; set; }
        public string Descripcion { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public bool Active { get; set; }
        public int IdTipoEvento { get; set; }
        public int IdEscenario { get; set; }

        public virtual Escenario? IdEscenarioNavigation { get; set; }
        public virtual TipoEvento? IdTipoEventoNavigation { get; set; }
        public virtual ICollection<Entrada>? Entrada { get; set; }
    }
}
