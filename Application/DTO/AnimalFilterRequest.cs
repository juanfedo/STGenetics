using System.ComponentModel.DataAnnotations;
using static Application.Enums.FilterEnums;

namespace Application.DTO
{
    public class AnimalFilterRequest
    {
        [Required]
        public AnimalFilter Filter { get; set; }
        [Required]
        public string? Value { get; set; }
    }
}
