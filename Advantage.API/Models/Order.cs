#nullable disable
namespace Advantage.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public decimal Total { get; set; }
        public DateTimeOffset Placed { get; set; }
        public DateTimeOffset? Completed { get; set; }
    }
}