using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class CityUpdateViewModel
    {
        [Required]
        [StringLength(512)]
        public string Description { get; set; }

        [Range(1, 100_000_000)]
        public int Population { get; set; }
    }
}
