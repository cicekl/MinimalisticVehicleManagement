using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Service.Models
{
    public class VehicleModelViewModel
    {
        public int Id { get; set; }

       // [Display(Name = "Make")]
        public int MakeId { get; set; }

       // [Display(Name = "Name")]
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

       // [Display(Name = "Abbreviation")]
        [Required(ErrorMessage = "The Abbreviation field is required.")]
        public string Abrv { get; set; }

        public VehicleMake? Make { get; set; }


    }
}
