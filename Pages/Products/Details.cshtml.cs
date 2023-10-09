using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProductsCRUDApplication.Data;
using ProductsCRUDApplication.Models;

namespace ProductsCRUDApplication.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ProductsCRUDApplication.Data.ProductsCRUDApplicationContext _context;

        public DetailsModel(ProductsCRUDApplication.Data.ProductsCRUDApplicationContext context)
        {
            _context = context;
        }

      public Product Product { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FirstOrDefaultAsync(m => m.IdProd == id);
            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }
    }
}
