using DevopsAssignment.Database;
using DevopsAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevopsAssignment.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MyDbContext _context;

        public ProductsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uniqueFileName = product.Name + Path.GetExtension(ImageFile.FileName);

                    // Copy the uploaded file to the specified path
                    using (var fileStream = new FileStream($"wwwroot/images/{uniqueFileName}", FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    // Set the ImageUrl property to the path of the uploaded image
                    product.ImageUrl = $"images/{uniqueFileName}";
                }

                // Save the new product to the database
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                // Redirect to the Home controller's Index action
                return RedirectToAction("Index", "Home");
            }

            // Return to the create view if there are validation errors
            return View(product);
        }

        [HttpPost]
        public IActionResult DeleteAll()
        {
            // Assuming you have a DbContext named _context
            var allProducts = _context.Products.ToList();

            if (allProducts.Count > 0)
            {
                _context.Products.RemoveRange(allProducts);
                _context.SaveChanges();
            }

            // Redirect back to the product list
            return RedirectToAction("Index", "Home");
        }
    }
}
