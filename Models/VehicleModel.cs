using VolvoWebApp.Enums;

namespace VolvoWebApp.Models
{
    public class VehicleModel
    {
        public required ChassisIdModel ChassisId { get; set; }
        public VehicleType Type { get; set; }
        public uint NumberOfPassengers { get; set; }
        public string? Color { get; set; }
    }
}
