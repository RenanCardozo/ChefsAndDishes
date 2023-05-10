#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChefsAndDishes.Models;

public class Dish
{
    [Key]
    public int DishId { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Name of Dish")]
    public string DishName { get; set; }

    [Required(ErrorMessage = "Calories is required")]
    [Display(Name = "Calories")]
    public int? Calories { get; set; }

    [Required]
    [Range(1,5)]
    public int? Tastiness { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int ChefId { get; set; }

    public Chef? Creator { get; set; }
}