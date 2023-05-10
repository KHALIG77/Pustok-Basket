using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokStart.Models
{
    public class Slide
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Desc { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public string BgColor { get; set; }
    }
}
