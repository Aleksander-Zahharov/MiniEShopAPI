using MiniEShopAPI.Models;
using System.Collections.Generic;

namespace MiniEShopAPI.Services
{
    /**
     * IProductService defines the contract for product-related business logic.
     */
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts(); // Retrieves all products
        Product GetProductById(int id); // Retrieves a product by its ID
        void CreateProduct(Product product); // Creates a new product
        bool UpdateProduct(int id, Product product); // Updates an existing product by ID
        bool DeleteProduct(int id); // Deletes a product by ID
    }
}