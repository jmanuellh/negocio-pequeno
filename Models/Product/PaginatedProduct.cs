using System.Collections.Generic;

namespace negocio_pequeño.Models.Product
{
    public class PaginatedProduct
    {
        public List<Product> Product {get;set;}
        public int ServerItemsLength {get;set;}
    }
}