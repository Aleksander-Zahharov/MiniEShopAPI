using Microsoft.AspNetCore.Mvc;
using MiniEShopAPI.Data;
using MiniEShopAPI.Models;
using System.Linq;

/**
 * ProductController handles API endpoints related to products.
 */
namespace MiniEShopAPI.Controllers
{
    [ApiController] // Marks this class as an API controller
    [Route("api/[controller]")] // Defines the base route for all endpoints in this controller
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context; // Database context for accessing products

        public ProductController(ApplicationDbContext context)
        {
            _context = context; // Injects the database context
        }

        [HttpGet("test-db-connection")] // Defines a GET endpoint to test database connection
        public IActionResult TestDatabaseConnection()
        {
            try
            {
                var products = _context.Products.ToList(); // Retrieves all products from the database
                return Ok(new { success = true, count = products.Count }); // Returns success and product count
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message }); // Returns error message
            }
        }

        [HttpGet("check-database")] // Defines a GET endpoint to check database status
        public IActionResult CheckDatabase()
        {
            Console.WriteLine("CheckDatabase endpoint was called."); // Logs the call to the console
            try
            {
                var products = _context.Products.ToList(); // Retrieves all products from the database
                return Ok(new { success = true, data = products }); // Returns success and product data
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); // Logs the error to the console
                return StatusCode(500, new { success = false, message = ex.Message }); // Returns error message
            }
        }

        [HttpGet] // Defines a GET endpoint at /api/products
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList(); // Retrieves all products from the database
            return Ok(products); // Returns the list of products
        }

        [HttpPost] // Defines a POST endpoint at /api/products
        public IActionResult AddProduct(Product product)
        {
            _context.Products.Add(product); // Adds the new product to the database
            _context.SaveChanges(); // Saves changes to the database
            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product); // Returns the created product
        }
    }
}