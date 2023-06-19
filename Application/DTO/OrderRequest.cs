using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class OrderRequest
    {
        [Required]
        public List<OrderDetailRequest>? Details { get; set; }
    }
}
