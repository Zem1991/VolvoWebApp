using Microsoft.EntityFrameworkCore;

namespace VolvoWebApp.Models
{
    public class ChassisId
    {
        //public string Id => $"{Series}/{Number}";
        public string? Series { get; set; }
        public uint Number { get; set; }

        //public override string ToString()
        //{
        //    return $"{Series}/{Number}";
        //}
    }
}
