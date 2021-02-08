namespace negocio_pequeño.Models
{
    public class Pagination
    {
        public int Page {get;set;} = 1;
        public int ItemsPerPage {get;set;} = 10000;
        public string[] sortBy {get;set;} = {"Id"};
        public bool[] sortDesc {get;set;} = {false};
        public Product.Product Product {get;set;} = new Product.Product();
    }
}
