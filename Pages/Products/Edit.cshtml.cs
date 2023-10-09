using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductsCRUDApplication.Data;
using ProductsCRUDApplication.Models;

namespace ProductsCRUDApplication.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly ProductsCRUDApplication.Data.ProductsCRUDApplicationContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(ProductsCRUDApplication.Data.ProductsCRUDApplicationContext context, IWebHostEnvironment webHostEnvironment)
        { 
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }

        [BindProperty]
        public Product Product { get; set; } = default!;


        [BindProperty]
        public IFormFile NewImage { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product =  await _context.Product.FirstOrDefaultAsync(m => m.IdProd == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;


           
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (NewImage != null)
            {
               
                    // Generate a unique filename using a timestamp
                    var fileName = DateTime.Now.Ticks + Path.GetExtension(NewImage.FileName);

                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // Ensure the uploads folder exists
                    Directory.CreateDirectory(uploadsFolder);

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await NewImage.CopyToAsync(fileStream);
                    }

                    // Save the file path in your database
                    Product.Image = "/images/" + fileName; // Update the path as per your project structure
                
            }


            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.IdProd))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.IdProd == id)).GetValueOrDefault();
        }
    }
}
