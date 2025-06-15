using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VolvoWebApp.Enums;

namespace VolvoWebApp.Dtos
{
    public class VehicleReadDTO
    {
        [Required]
        [DisplayName("Id")]
        public string Id { get; set; } = $"{Guid.NewGuid()}";
        [Editable(false)]
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Editable(false)]
        [DisplayName("Last Update")]
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        [Required]
        [DisplayName("Chassis Series")]
        public string ChassisSeries { get; set; } = string.Empty;
        [Required]
        [DisplayName("Chassis Number")]
        public uint ChassisNumber { get; set; }
        [Required]
        [DisplayName("Chassis Id")]
        public string ChassisId => $"{ChassisSeries}/{ChassisNumber}";
        [Required]
        [DisplayName("Type")]
        public VehicleType Type { get; set; }
        [Required]
        [DisplayName("Number of Passengers")]
        public uint NumberOfPassengers => Type.NumberOfPassengers();
        [Required]
        [DisplayName("Color")]
        public string Color { get; set; } = string.Empty;
    }
}
