using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VolvoWebApp.Data.Entities;
using VolvoWebApp.Enums;

namespace VolvoWebApp.Models
{
    public class VehicleModel : BaseRecordModel
    {
        [Required]
        [DisplayName("Chassis Series")]
        public string ChassisSeries { get; set; }
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
        public string Color { get; set; }
    }
}
