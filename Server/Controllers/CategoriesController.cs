﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Constructor = Add AppDBContext (import + asign)
        // Add IWebHostEnvironment [For Delete]
        public CategoriesController(AppDBContext appDBContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDBContext = appDBContext;
            _webHostEnvironment = webHostEnvironment;
        }
        #region Crud Methods
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
        // Get with Posts
        [HttpGet("withposts")]
        public async Task<IActionResult> GetWithPosts()
        {
            List<Category> categories = await _appDBContext.Categories
                .Include(category => category.Posts).ToListAsync();
            return Ok(categories);
        }

        // Get a single category by id
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategory(int id)
        {
            Category category = await GetCategoryByCategoryID(id, false);
            return Ok(category);
        }

        // Get a single category by id with posts
        [HttpGet("withposts/{id}")]
        public async Task<IActionResult> GetCategoryWithPosts(int id)
        {
            Category category = await GetCategoryByCategoryID(id, true);
            return Ok(category);
        }

        // Create a category
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category categoryToCreate)
        {
            try
            {
                if (categoryToCreate == null) // Form Empty
                {
                    return BadRequest(ModelState);
                }
                if (ModelState.IsValid == false) 
                {
                    return BadRequest(ModelState);
                }
                await _appDBContext.Categories.AddAsync(categoryToCreate);
                bool changesPersistToDatabase = await PersistChangeToDatabase();
                if (changesPersistToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong with out side. Please contact the developer");
                }
                else
                {
                    return Created("Create", categoryToCreate);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Wooops! Something went wrong. Please contact the developer with Error message: {e.Message}.");
            }
        }
        // Update a category
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category categoryToUpdate)
        {
            try
            {
                if (id < 1 || categoryToUpdate == null || id != categoryToUpdate.CategoryID ) 
                {
                    return BadRequest(ModelState);
                }
                // Check if a category exists with the passed ID.
                bool exits = await _appDBContext.Categories.AnyAsync(category => category.CategoryID == id);
                if (exits == false)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                _appDBContext.Categories.Update(categoryToUpdate);
                bool changesPersistToDatabase = await PersistChangeToDatabase();
                if (changesPersistToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong with out side. Please contact the developer");
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Wooops! Something went wrong. Please contact the developer with Error message: {e.Message}.");
            }
        }
        // Delete a category
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest(ModelState);
                }
                // Check if a category exists with the passed ID.
                bool exits = await _appDBContext.Categories.AnyAsync(category => category.CategoryID == id);
                if (exits == false)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }
                // Get category
                Category categoryToDelete = await GetCategoryByCategoryID(id, false);
                // If image uploaded delete also
                if (categoryToDelete.BlogImage != "uploads/placeholder.jpg")
                {
                    string fileName = categoryToDelete.BlogImage.Split('/').Last();
                    System.IO.File.Delete($"{_webHostEnvironment.ContentRootPath}\\wwwroot\\uploads\\{fileName}");
                }
                _appDBContext.Categories.Remove(categoryToDelete);
                // Persistance
                bool changesPersistToDatabase = await PersistChangeToDatabase();
                if (changesPersistToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong with out side. Please contact the developer");
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Wooops! Something went wrong. Please contact the developer with Error message: {e.Message}.");
            }
        }

        #endregion
        // ------------------------------------ //
        #region Utility Methods
        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> PersistChangeToDatabase()
        {
            int amountOfChanges = await _appDBContext.SaveChangesAsync();
            return amountOfChanges > 0;
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Category> GetCategoryByCategoryID(int categoryID, bool withPosts)
        {
            Category categoryToGet = null;
            if (withPosts == true)
            {
                categoryToGet = await _appDBContext.Categories
                    .Include(category => category.Posts)
                    .FirstAsync(category => category.CategoryID == categoryID);
            }
            else
            {
                categoryToGet = await _appDBContext.Categories
                    .FirstAsync(category => category.CategoryID == categoryID);
            }
            return categoryToGet;
        }

        #endregion

    }
}
