using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductsCRUDApplication.Data;
using ProductsCRUDApplication.Models;


using System.IO;
using Microsoft.AspNetCore.Hosting;



namespace ProductsCRUDApplication.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ProductsCRUDApplication.Data.ProductsCRUDApplicationContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public CreateModel(ProductsCRUDApplication.Data.ProductsCRUDApplicationContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        
        [BindProperty]
        public Product Product { get; set; } = default!;


        // Property to represent the uploaded image file
        [BindProperty]
        public IFormFile ImageFile { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid /*|| _context.Product == null || Product == null*/)
            {
                return Page();
            }





            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Generate a unique filename using a timestamp
                var fileName = DateTime.Now.Ticks + Path.GetExtension(ImageFile.FileName);

                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Ensure the uploads folder exists
                Directory.CreateDirectory(uploadsFolder);

                // Save the file to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                // Save the file path in your database
                Product.Image = "/images/" + fileName; // Update the path as per your project structure
            }


            _context.Product.Add(Product);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}
