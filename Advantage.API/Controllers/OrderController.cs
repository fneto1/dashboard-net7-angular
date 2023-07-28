#nullable disable

using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Advantage.API.Controllers
{

    [ApiController]
    [Route("api/[controller]/")]
    public class OrderController : Controller
    {
        private readonly ApiContext _ctx;

        public OrderController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            var data = _ctx.Orders.Include(o => o.Customer).OrderByDescending(c => c.Placed);

            var page = new PaginatedResponse<Order>(data, pageIndex, pageSize);

            var totalCount = data.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var response = new
            {
                Page = page,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        [HttpGet("ByState")]
        public IActionResult ByState()
        {
            var orders = _ctx.Orders.Include(o => o.Customer).ToList();

            var groupedResult = orders.GroupBy(o => o.Customer.State).ToList()
            .Select(grp => new
            {

                State = grp.Key,
                Total = grp.Sum(x => x.Total)
            })
            .OrderByDescending(res => res.Total).ToList();

            return Ok(groupedResult);
        }

        [HttpGet("ByCostumer/{n}")]
        public IActionResult ByCostumer(int n)
        {
            var orders = _ctx.Orders.Include(o => o.Customer).ToList();

            var groupedResult = orders.GroupBy(o => o.Customer.Id).ToList()
            .Select(grp => new
            {
                Costumer = _ctx.Customers.Find(grp.Key).Name,
                Total = grp.Sum(x => x.Total)
            })
            .Take(n)
            .OrderByDescending(res => res.Total).ToList();

            return Ok(groupedResult);
        }

        [HttpGet("GetOrder/{id}", Name = "GerOrder")]
        public IActionResult GetOrder(int id)
        {
            var order = _ctx.Orders.Include(o => o.Customer).First(o => o.Id == id);

            return Ok(order);
        }
    }


}