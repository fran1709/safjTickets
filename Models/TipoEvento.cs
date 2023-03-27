﻿using System;
using System.Collections.Generic;

namespace devTicket.Models
{
    public partial class TipoEvento
    {
        public TipoEvento()
        {
            Eventos = new HashSet<Evento>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public int Active { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; }
    }
}
