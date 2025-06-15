using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VolvoWebApp.Enums;

namespace VolvoWebApp.Models
{
    public class Vehicle : BaseRecord
    {
        //[Required]
        //[DisplayName("Id")]
        //public string Id { get; set; } = $"{Guid.NewGuid()}";
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
