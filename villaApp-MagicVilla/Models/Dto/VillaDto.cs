using System.ComponentModel.DataAnnotations;

namespace villaApp_MagicVilla.Models.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]

        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
