using AdventureWorks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AdventureWorksContext _context;

        public OrdersController(AdventureWorksContext context)
        {
            _context = context;
        }


        [HttpGet("product-listprice-color-average")]
        public async Task<IEnumerable<ProductListPriceColorAverageDTO>> 
            GetProductListPriceByColorAverage() {
            var q = await _context.Products
                .GroupBy(g => g.Color)
                .Select(i => 
                        new ProductListPriceColorAverageDTO 
                        { 
                            Color = i.Key, AverageListPrice = i.Average(x => x.ListPrice) 
                        })
                .ToListAsync();

            return q;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderItemDTO>> GetOrders([FromQuery] long totoalDueOver)
        {
            var q = await _context.SalesOrderHeaders
                 .Where(s => s.TotalDue > totoalDueOver)
                 .Select(o => new
                 OrderItemDTO
                 {
                     CustomerId = o.CustomerId,
                     CustomerName = o.Customer.FirstName + " " + o.Customer.LastName,
                     TotalDue = o.TotalDue
                 }).OrderBy(x => x.TotalDue).ToListAsync();


            return q;
        }
    }
}
