﻿using System.ComponentModel.DataAnnotations;

namespace Project.MVC.Models
{
    public class VehicleMakeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Abbreviation field is required.")]
        public string Abrv { get; set; }
    }
}
