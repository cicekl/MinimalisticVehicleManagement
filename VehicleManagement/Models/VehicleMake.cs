using System.ComponentModel.DataAnnotations;

namespace Project.Service.Models
{
    public class VehicleMake
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Abrv { get; set; }
 
        public List<VehicleModel>? Models { get; set; }

    }
}
