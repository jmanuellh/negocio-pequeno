using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace negocio_pequeño.Models.Product
{
    public class Product
    {
        public int Id {get;set;} = 0;
        [StringLength(50)]
        public string Nombre {get;set;} = null;
        [Column(TypeName = "decimal(5, 2)")]
        public decimal PrecioCompra {get;set;} = 0;
        [Column(TypeName = "decimal(4, 2)")]
        public decimal PrecioVenta {get;set;} = 0;
    }    
}
