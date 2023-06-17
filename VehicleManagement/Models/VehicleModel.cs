using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Service.Models
{
    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Make")]
        public int MakeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Abrv { get; set; }

        public VehicleMake? Make { get; set; }
    }
}
