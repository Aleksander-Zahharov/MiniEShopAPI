using MiniEShopAPI.Models;
using MiniEShopAPI.Repositories;
using System.Collections.Generic;

namespace MiniEShopAPI.Services
{
    /**
     * ProductService implements business logic for managing products.
     */
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository; // Repository for accessing product data

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository; // Injects the product repository
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAll(); // Retrieves all products from the repository
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetById(id); // Retrieves a product by ID from the repository
        }

        public void CreateProduct(Product product)
        {
            _productRepository.Add(product); // Adds a new product to the repository
        }

        public bool UpdateProduct(int id, Product product)
        {
            return _productRepository.Update(id, product); // Updates an existing product by ID
        }

        public bool DeleteProduct(int id)
        {
            return _productRepository.Delete(id); // Deletes a product by ID
        }
    }
}