using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VolvoWebApp.Enums;

namespace VolvoWebApp.Dtos
{
    public class VehicleUpdateDTO : BaseUpdateDTO
    {
        [Required]
        [DisplayName("Color")]
        public string Color { get; set; }
    }
}
