using Microsoft.AspNetCore.Mvc;
using Menu.Data;
using Menu.Models;
using Microsoft.EntityFrameworkCore;

namespace Menu.Controllers
{
    public class Menu : Controller
    {
        private readonly MenuContext _context;
        public Menu(MenuContext context) 
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var dishes = from d in _context.Dishes
                       select d;
            if(!string.IsNullOrEmpty(searchString))
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                dishes = dishes.Where(d => d.Name.Contains(searchString));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                return View(await dishes.ToListAsync());

            }
            return View( await dishes.ToListAsync());
        }

        public async Task<IActionResult> Details (int? id)
        {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            var dish = await _context.Dishes
                .Include(di => di.DishIngredients)
                .ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync(x => x.Id == id);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

            if (dish == null) 
            {
                return NotFound();
            }
            return View(dish);
        }
    }
}
