using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsCRUDApplication.Models
{
    public class Product
    {
        [Key]
        public int IdProd { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String category { get; set; }
        public String Stock { get; set; }
        public String Image { get; set; }
        public float Price { get; set; }

       
    }
}
