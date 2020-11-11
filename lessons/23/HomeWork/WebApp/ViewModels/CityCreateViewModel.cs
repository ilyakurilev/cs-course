using System;
using System.ComponentModel.DataAnnotations;
using WebApp.ViewModels.Attributes;

namespace WebApp.ViewModels
{
    public class CityCreateViewModel
    {
		[Required(ErrorMessage = "The title is empty")]
		[StringLength(128)]
		[Duplicate(nameof(Description), ErrorMessage = "The value duplicated with field Description")]
		public string Title { get; set; }

		[Required]
		[StringLength(512)]
		public string Description { get; set; }

		[Range(1, 100_000_000)]
		public int Population { get; set; }
	}
}
