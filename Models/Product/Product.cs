using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace negocio_pequeño.Models.Product
{
    public class Product
    {
        public int Id {get;set;}
        [StringLength(50)]
        public string Nombre {get;set;}
        [Column(TypeName = "decimal(4, 2)")]
        public decimal PrecioVenta {get;set;}
    }    
}
