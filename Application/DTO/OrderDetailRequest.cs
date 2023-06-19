using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class OrderDetailRequest
    {
        [Required]
        public int AnimalId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
