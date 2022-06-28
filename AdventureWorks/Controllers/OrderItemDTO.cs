namespace AdventureWorks.Controllers
{
    public class OrderItemDTO
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalDue { get; set; }
    }
}