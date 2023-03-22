using System;
using System.Collections.Generic;

namespace samTicket.Models
{
    public partial class Escenario
    {
        public Escenario()
        {
            TipoEscenarios = new HashSet<TipoEscenario>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Localizacion { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<TipoEscenario> TipoEscenarios { get; set; }
    }
}
