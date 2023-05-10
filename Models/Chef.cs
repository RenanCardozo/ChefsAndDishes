#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChefsAndDishes.Models;

public class Chef
{
    [Key]
    public int ChefId { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    [Display(Name = "Date of Birth")]
    [DataType(DataType.Date)]
    [MinAge(18)]
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<Dish> Dishes { get; set; } = new List<Dish>();
}
public class MinAge : ValidationAttribute
{
    private int _Limit;
    public MinAge(int Limit)
    { 
        this._Limit = Limit;
    }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime bday = DateTime.Parse(value.ToString());
        DateTime today = DateTime.Today;
        int age = today.Year - bday.Year;
        if (bday > today.AddYears(-age))
        {
            age--;
        }
        if (age < _Limit)
        {
            var result = new ValidationResult("Sorry, chefs must be at least 18 year old!");
            return result;
        }
        return null;
    }
}