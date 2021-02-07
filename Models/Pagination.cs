namespace negocio_pequeño.Models
{
    public class Pagination
    {
        public int Page {get;set;}
        public int ItemsPerPage {get;set;}
        public Product.Product Product {get;set;}
    }
}