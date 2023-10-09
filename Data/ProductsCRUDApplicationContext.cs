using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductsCRUDApplication.Models;

namespace ProductsCRUDApplication.Data
{
    public class ProductsCRUDApplicationContext : DbContext
    {
        public ProductsCRUDApplicationContext (DbContextOptions<ProductsCRUDApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<ProductsCRUDApplication.Models.Product> Product { get; set; } = default!;
        
    }
}
