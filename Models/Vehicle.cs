using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VolvoWebApp.Enums;

namespace VolvoWebApp.Models
{
    public class Vehicle
    {
        [Required]
        [DisplayName("Id")]
        [Editable(false)]
        public string Id { get; set; } = $"{Guid.NewGuid()}";
        [Required]
        [DisplayName("Chassis Series")]
        [Editable(false)]
        public string ChassisSeries { get; set; }
        [Required]
        [DisplayName("Chassis Number")]
        [Editable(false)]
        public uint ChassisNumber { get; set; }
        [Required]
        [DisplayName("Chassis Id")]
        [Editable(false)]
        public string ChassisId => $"{ChassisSeries}/{ChassisNumber}";
        [Required]
        [DisplayName("Type")]
        [Editable(false)]
        public VehicleType Type { get; set; }
        [Required]
        [DisplayName("Number of Passengers")]
        [Editable(false)]
        public uint NumberOfPassengers => Type.NumberOfPassengers();
        [Required]
        [DisplayName("Color")]
        [Editable(true)]
        public string Color { get; set; }
    }
}
