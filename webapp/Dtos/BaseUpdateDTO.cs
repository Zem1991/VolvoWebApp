using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VolvoWebApp.Dtos
{
    public class BaseUpdateDTO
    {
        [Required]
        [DisplayName("Id")]
        public string Id { get; set; }
    }
}
