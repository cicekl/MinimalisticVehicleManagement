using Project.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace Project.MVC.Models
{
    public class VehicleModelViewModel
    {
        public int Id { get; set; }

        public int MakeId { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Abbreviation field is required.")]
        public string Abrv { get; set; }

        public VehicleMake? Make { get; set; }


    }
}
