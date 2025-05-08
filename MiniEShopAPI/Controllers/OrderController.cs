/**
 * OrderController handles API endpoints related to orders.
 */
using Microsoft.AspNetCore.Mvc;
using MiniEShopAPI.Data;
using MiniEShopAPI.Models;
using System.Linq;

namespace MiniEShopAPI.Controllers
{
    [ApiController] // Marks this class as an API controller
    [Route("api/[controller]")] // Defines the base route for all endpoints in this controller
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context; // Database context for accessing orders

        public OrderController(ApplicationDbContext context)
        {
            _context = context; // Injects the database context
        }

        [HttpGet] // Defines a GET endpoint at /api/orders
        public IActionResult GetOrders()
        {
            var orders = _context.Orders.ToList(); // Retrieves all orders from the database
            return Ok(orders); // Returns the list of orders
        }

        [HttpGet("{id}")] // Defines a GET endpoint at /api/orders/{id}
        public IActionResult GetOrderById(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id); // Finds an order by ID
            if (order == null)
            {
                return NotFound(); // Returns 404 if the order is not found
            }
            return Ok(order); // Returns the found order
        }

        [HttpPost] // Defines a POST endpoint at /api/orders
        public IActionResult CreateOrder(Order order)
        {
            _context.Orders.Add(order); // Adds the new order to the database
            _context.SaveChanges(); // Saves changes to the database
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order); // Returns the created order
        }

        [HttpDelete("{id}")] // Defines a DELETE endpoint at /api/orders/{id}
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id); // Finds an order by ID
            if (order == null)
            {
                return NotFound(); // Returns 404 if the order is not found
            }

            _context.Orders.Remove(order); // Removes the order from the database
            _context.SaveChanges(); // Saves changes to the database
            return NoContent(); // Returns 204 No Content
        }
    }
}