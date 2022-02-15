using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace Server.Controllers
{
    // Default placeholder for all routes = www.namewebsite/api/categories
    // Specifies this controller will handle HTTPS requests/responses & Inheritate Controller()
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDBContext _appDBContext;

        // Constructor = Add AppDBContext (import + asign)
        public CategoriesController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        // Create GET Request return all categories from db.
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // List<Category> categories = await _appDBContext.Categories.ToListAsync();
            List<Category> categories = await _appDBContext.Categories.ToListAsync();
            // Return list of categories in json.
            return Ok(categories);

            //async (AppDBContext db) => await db.Categories.ToListAsync());
        }
    }
}
