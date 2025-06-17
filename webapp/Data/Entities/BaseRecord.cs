using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VolvoWebApp.Data.Entities
{
    public class BaseRecord
    {
        [Required]
        [DisplayName("Id")]
        public string Id { get; set; } = $"{Guid.NewGuid()}";
        [Editable(false)]
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Editable(false)]
        [DisplayName("Last Update")]
        public DateTime LastUpdate { get; set; } = DateTime.Now;

        public void WriteUpdate()
        {
            LastUpdate = DateTime.Now;
        }
    }
}
