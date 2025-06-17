using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VolvoWebApp.Models
{
    public class BaseRecordModel
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
