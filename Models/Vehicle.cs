using VolvoWebApp.Enums;

namespace VolvoWebApp.Models
{
    public class Vehicle
    {
        public string Id { get; set; }
        public ChassisId ChassisId { get; set; }
        public VehicleType Type { get; set; }
        public uint NumberOfPassengers { get; set; }
        public string? Color { get; set; }
    }
}
