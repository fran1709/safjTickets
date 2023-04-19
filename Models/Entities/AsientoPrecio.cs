using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace devTicket.Models.Entities
{
    public class AsientoPrecio:Asiento
    {
        public Decimal Precio { get; set; }
    }
}
