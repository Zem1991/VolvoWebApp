using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VolvoWebApp.Enums;

namespace VolvoWebApp.Dtos
{
    public class VehicleReadDTO : BaseReadDTO
    {
        [Required]
        [DisplayName("Chassis Series")]
        public string ChassisSeries { get; set; }
        [Required]
        [DisplayName("Chassis Number")]
        public uint ChassisNumber { get; set; }
        [Required]
        [DisplayName("Type")]
        public VehicleType Type { get; set; }
        [Required]
        [DisplayName("Color")]
        public string Color { get; set; }
    }
}
