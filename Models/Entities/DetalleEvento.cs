using System.ComponentModel.DataAnnotations;

namespace devTicket.Models
{
    public class DetalleEvento
    {
        [Display (Name="Id del Evento")]
        public int Id { get; set; }
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
        [Display(Name = "Tipo del Evento")]
        public string TipoEvento { get; set; }
        [Display(Name = "Fecha y Hora")]
        public DateTime Fecha { get; set; }
        [Display(Name = "Tipo del Evento")]
        public string TipoEscenario { get; set; }
        [Display(Name = "Escenario")]
        public string Escenario { get; set; }
        [Display(Name = "Localizacion")]
        public string Localizacion { get; set; }

    }
}
