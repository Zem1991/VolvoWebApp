using Microsoft.EntityFrameworkCore;
using VolvoWebApp.Enums;

namespace VolvoWebApp.Models
{
    public class Vehicle
    {
        public string Id { get; set; }
        public string ChassisSeries { get; set; }
        public uint ChassisNumber { get; set; }
        public string ChassisId => $"{ChassisSeries}/{ChassisNumber}";
        public VehicleType Type { get; set; }
        public uint NumberOfPassengers => Type.NumberOfPassengers();
        public string? Color { get; set; }

        public Vehicle()
        {
            
        }
    }
}
