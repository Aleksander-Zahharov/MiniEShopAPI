using MiniEShopAPI.Data;
using MiniEShopAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace MiniEShopAPI.Repositories
{
    /**
     * ProductRepository implements data access methods for products.
     */
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context; // Database context for accessing data

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context; // Injects the database context
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList(); // Retrieves all products from the database
        }

        public Product GetById(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id); // Finds a product by ID
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found."); // Throws an exception if not found
            }
            return product; // Returns the found product
        }

        public void Add(Product product)
        {
            _context.Products.Add(product); // Adds a new product to the database
            _context.SaveChanges(); // Saves changes to the database
        }

        public bool Update(int id, Product product)
        {
            var existingProduct = _context.Products.Find(id); // Finds the product by ID
            if (existingProduct == null) return false; // Returns false if the product is not found

            existingProduct.Name = product.Name; // Updates the product name
            existingProduct.Description = product.Description; // Updates the product description
            existingProduct.Price = product.Price; // Updates the product price

            _context.SaveChanges(); // Saves changes to the database
            return true; // Returns true if the update was successful
        }

        public bool Delete(int id)
        {
            var product = _context.Products.Find(id); // Finds the product by ID
            if (product == null) return false; // Returns false if the product is not found

            _context.Products.Remove(product); // Removes the product from the database
            _context.SaveChanges(); // Saves changes to the database
            return true; // Returns true if the deletion was successful
        }
    }
}