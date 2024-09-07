using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantList.Data;
using RestaurantList.Models;

namespace RestaurantList.Controllers
{
    public class RestaurantListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantListController(ApplicationDbContext context)
        {
            _context = context;
        }


        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Restaurants.ToListAsync());
        //}


        public async Task<IActionResult> Index(string searchString)
        {
            var restaurants = from r in _context.Restaurants
                              select r;
            if (!string.IsNullOrEmpty(searchString))
            {
                restaurants = restaurants.Where(r => r.Name.Contains(searchString));
                return View(await restaurants.ToListAsync());
            }
            return View(await restaurants.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var restaurant = await _context.Restaurants.Include(rd => rd.RestaurantDishes)
            .ThenInclude(d => d.Dish)
            .FirstOrDefaultAsync(x => x.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

    }
}
