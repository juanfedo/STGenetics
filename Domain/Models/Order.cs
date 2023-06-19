namespace Domain.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int TotalAmount { get; set; } = 0;
        public int TotalPrice { get; set; } = 0;
    }
}
