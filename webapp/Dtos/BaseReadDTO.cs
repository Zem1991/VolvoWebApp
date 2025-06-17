using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VolvoWebApp.Dtos
{
    public class BaseReadDTO
    {
        [Required]
        [DisplayName("Id")]
        public string Id { get; set; }
        [Editable(false)]
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
        [Editable(false)]
        [DisplayName("Last Update")]
        public DateTime LastUpdate { get; set; }
    }
}
