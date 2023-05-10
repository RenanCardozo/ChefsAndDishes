#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChefsAndDishes.Models;

public class ChefsAndDish
{
    public Dish dish { get; set; }

    public List<Chef>? chefs { get; set; }
}