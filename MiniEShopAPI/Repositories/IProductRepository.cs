using MiniEShopAPI.Models;
using System.Collections.Generic;

namespace MiniEShopAPI.Repositories
{
    /**
     * IProductRepository defines the contract for product data access.
     */
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll(); // Retrieves all products
        Product GetById(int id); // Retrieves a product by its ID
        void Add(Product product); // Adds a new product to the repository
        bool Update(int id, Product product); // Updates an existing product by ID
        bool Delete(int id); // Deletes a product by ID
    }
}