using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ChefsAndDishes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace ChefsAndDishes.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    //////////////////////////////////?Views Section?////////////////////////////////////////////
    public IActionResult Index()
    {
        List<Chef> chefs = _context.Chefs.Include(c => c.Dishes).ToList();
        return View("Index", chefs);
    }

    [HttpGet("chefs/new")]
    public IActionResult ChefForm()
    {
        return View("ChefForm");
    }

    [HttpGet("dish")]
    public IActionResult DishView()
    {
        List<Dish> AllDishes = _context.Dishes.Include(c => c.Creator).ToList();

        return View("DishesView", AllDishes);
    }

    [HttpGet("dishes/new")]
    public IActionResult DishForm()
    {
        List<Chef> chefs = _context.Chefs.ToList();
        ChefsAndDish formdata = new ChefsAndDish();
        formdata.chefs = chefs;
        return View("DishForm", formdata);
    }
    [HttpGet("dishes/{id}/edit")]
    public IActionResult EditDish(int id)
    {
        Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == id);
        if (dish == null)
        {
            return View("Index");
        }
        List<Chef> chefs = _context.Chefs.ToList();
        ChefsAndDish formdata = new ChefsAndDish { chefs = chefs, dish = dish };
        return View("UpdateDish", formdata);
    }
    //////////////////////////////?End Views Section?//////////////////////////////
    //////////////////////////////!Create Section!/////////////////////////////////
    [HttpPost("createDish")]
    public IActionResult CreateDish(ChefsAndDish chefsDish)
    {
        if (ModelState.IsValid)
        {
            _context.Dishes.Add(chefsDish.dish);
            _context.SaveChanges();
            return RedirectToAction("DishView");
        }

        List<Chef> chefs = _context.Chefs.ToList();
        ChefsAndDish formdata = new ChefsAndDish();
        formdata.chefs = chefs;
        formdata.dish = chefsDish.dish;
        return View("DishForm", formdata);
    }

    [HttpPost("createChef")]
    public IActionResult CreateChef(Chef user)
    {
        if (ModelState.IsValid)
        {
            _context.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            return View("ChefForm");
        }
    }



    //////////////////////!Update Section!/////////////////////////////////

    [HttpPost("dishes/update")]
    public IActionResult UpdateDish(ChefsAndDish chefsDish)
    {
        if (ModelState.IsValid)
        {
            Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == chefsDish.dish.DishId);
            if (dish != null)
            {
                Chef? creatorChef = _context.Chefs.FirstOrDefault(c => c.ChefId == chefsDish.dish.ChefId);
                dish.DishName = chefsDish.dish.DishName;
                dish.Calories = chefsDish.dish.Calories;
                dish.Tastiness = chefsDish.dish.Tastiness;
                dish.Creator = creatorChef;
                _context.SaveChanges();
                return RedirectToAction("DishView");
            }
            else
            {
                return View("Index");
            }
        }
        else
        {
            List<Chef> chefs = _context.Chefs.ToList();
            ChefsAndDish formdata = new ChefsAndDish { chefs = chefs, dish = chefsDish.dish };
            return View("UpdateDish", formdata);
        }
    }

    //////////////////////////?Delete Section?/////////////////////////////////
    [HttpPost("dishes/delete")]
    public IActionResult DeleteDish(int id)
    {
        Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == id);
        if (dish == null)
        {
            return View("Index");
        }

        _context.Dishes.Remove(dish);
        _context.SaveChanges();
        return RedirectToAction("DishView");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
