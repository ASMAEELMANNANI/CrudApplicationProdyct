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


namespace ProductsCRUDApplication.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProductsCRUDApplication.Data.ProductsCRUDApplicationContext _context;
        private readonly IWebHostEnvironment webHostEvenrinment;

        public IndexModel(ProductsCRUDApplication.Data.ProductsCRUDApplicationContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

       
        /// lié  la recherche http Get
        [BindProperty(SupportsGet =true)]
        public string Des { get; set; }



        /// search about 
        [BindProperty(SupportsGet = true)]
        public string Cat { get; set; }

        public List<string> Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public float Min_Price { get; set; }

        [BindProperty(SupportsGet = true)]
        public float Max_Price { get; set; }

       
        public async Task OnGetAsync()
        {
            ///Uplaoad Image 
            
            //Extract List
            Categories = await _context.Product.Select(p => p.category).Distinct().ToListAsync();
            var prods = from m in _context.Product  where m.Price >= Min_Price select m;

            if (!string.IsNullOrEmpty(Des))
            {
                prods = prods.Where(s => s.Name.Contains(Des));
            }

            if (!string.IsNullOrEmpty(Cat))
            {
                prods = prods.Where(s => s.category == Cat);
            }

            if (Max_Price>0)
            {
                prods = prods.Where(s => s.Price <= Max_Price);
            }


            Product = await prods.ToListAsync();
        }
    }
}
