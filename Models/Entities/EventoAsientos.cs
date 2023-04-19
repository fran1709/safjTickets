using devTicket.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace devTicket.Models
{
    public class EventoAsientos : DetalleEvento
    {
        [Display(Name = "Asientos")]
        public List<AsientoPrecio> Asientos { get; set; }
    }
}
