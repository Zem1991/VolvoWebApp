using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VolvoWebApp.Enums;

namespace VolvoWebApp.Dtos
{
    public class VehicleUpdateDTO
    {
        [Required]
        [DisplayName("Id")]
        public string Id { get; set; } = $"{Guid.NewGuid()}";
        [Required]
        [DisplayName("Color")]
        public string Color { get; set; } = string.Empty;
    }
}
