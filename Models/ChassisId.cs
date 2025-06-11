namespace VolvoWebApp.Models
{
    public class ChassisId
    {
        public string? Series { get; set; }
        public uint Number { get; set; }

        public override string ToString()
        {
            return $"{Series}/{Number}";
        }
    }
}
