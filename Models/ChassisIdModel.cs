namespace VolvoWebApp.Models
{
    public class ChassisIdModel
    {
        public string? Series { get; set; }
        public uint Number { get; set; }

        public override string ToString()
        {
            return $"{Series}/{Number}";
        }
    }
}
