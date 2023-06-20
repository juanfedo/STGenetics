using Application.Validation;
using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class OrderRequest
    {
        [Required]
        [ValidateDuplicateAnimals]
        public List<OrderDetailRequest>? Details { get; set; }
    }
}
