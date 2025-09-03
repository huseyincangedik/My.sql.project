using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My.sql.project.Contexts;

namespace My.sql.project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductForecast: ControllerBase
    {
        private readonly ILogger<ProductForecast> _logger;
        private readonly ProductDbContext _context;

        public ProductForecast(ILogger<ProductForecast> logger, ProductDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Tüm ürünleri listeleyen metot
        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products");
                return StatusCode(500, "Internal server error");
            }
        }

   

        // Sipariþleri listeleyen metot
        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            try
            {
                var orders = await _context.Orders
                    .ToListAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting orders");
                return StatusCode(500, "Internal server error");
            }
        }

        // Ürün bilgileriyle birlikte sipariþleri listeleyen metot
        [HttpGet("orders-with-products")]
        public async Task<ActionResult<IEnumerable<object>>> GetOrdersWithProducts()
        {
            try
            {
                var result = await _context.Orders
                    .Include(o => o.Product)
                    .Select(o => new
                    {
                        OrderId = o.OrderId,
                        ProductId = o.ProductId,
                        ProductName = o.Product.Name,
                        Quantity = o.Quantity,
                        OrderDate = o.OrderDate.ToString("yyyy-MM-dd"),
                        UnitPrice = o.Product.Price,
                        TotalPrice = o.Quantity * o.Product.Price
                    })
                    .ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting orders with products");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}