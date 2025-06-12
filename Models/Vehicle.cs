using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using VolvoWebApp.Enums;

namespace VolvoWebApp.Models
{
    public class Vehicle
    {
        [DisplayName("Id")]
        public string Id { get; set; }
        [DisplayName("Chassis Series")]
        public string ChassisSeries { get; set; }
        [DisplayName("Chassis Number")]
        public uint ChassisNumber { get; set; }
        [DisplayName("Chassis Id")]
        public string ChassisId => $"{ChassisSeries}/{ChassisNumber}";
        [DisplayName("Type")]
        public VehicleType Type { get; set; }
        [DisplayName("Number of Passengers")]
        public uint NumberOfPassengers => Type.NumberOfPassengers();
        [DisplayName("Color")]
        public string? Color { get; set; }

        public Vehicle()
        {
            
        }
    }
}
