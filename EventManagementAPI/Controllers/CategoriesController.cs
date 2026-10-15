using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventManagementAPI.Data;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly EventsDbContext _context;
        public CategoriesController(EventsDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> GetAllCategories()
        {
            var FindAllCategories = await _context.Categories.ToListAsync();

            if(FindAllCategories == null)
            {
                return NotFound("No Categories Available");
            }

            return Ok(FindAllCategories);
        }
        [HttpPost]
        public async Task<ActionResult<Categories>> CreateCategory(Categories category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateCategory), new { CategoryID = category.CategoriesID }, category);
        }
        [HttpPut("{CategoriesID}")]
        public async Task<ActionResult<Categories>> UpdateCategory(int CategoriesID, Categories categories)
        {
            var findCategory = await _context.Categories.FindAsync(CategoriesID);
            
            if(findCategory == null)
            {
                return NotFound();
            }

            findCategory.Age = categories.Age;
            findCategory.categoryType = categories.categoryType;
            findCategory.MaxKM = categories.MaxKM;
            findCategory.MinKM = categories.MinKM;

            await _context.SaveChangesAsync();

            return Ok(findCategory);
        }

    }
}
