using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace Server.Controllers
{
    // Default placeholder for all routes = www.namewebsite/api/posts
    // Specifies this controller will handle HTTPS requests/responses & Inheritate Controller()
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly AppDBContext _appDBContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        // Constructor = Add AppDBContext (import + asign)
        // Add IWebHostEnvironment [For Delete]
        public PostsController(AppDBContext appDBContext, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _appDBContext = appDBContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
        #region Crud Methods
        // Create GET Request return all posts from db.
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // List<Post> posts = await _appDBContext.Posts.ToListAsync();
            List<Post> posts = await _appDBContext.Posts
                .Include(post => post.Category)
                .ToListAsync();
            // Return list of posts in json.
            return Ok(posts);
            //async (AppDBContext db) => await db.Posts.ToListAsync());
        }

        // Get a single post by id
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPost(int id)
        {
            Post post = await GetPostByPostID(id);
            return Ok(post);
        }

        // Create a post
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostDTO postToCreateDTO)
        {
            try
            {
                if (postToCreateDTO == null) // Form Empty
                {
                    return BadRequest(ModelState);
                }
                if (ModelState.IsValid == false) 
                {
                    return BadRequest(ModelState);
                }
                // MAP
                Post postToCreate = _mapper.Map<Post>(postToCreateDTO);

                if (postToCreate.IsPublished == true)
                {
                    postToCreate.PublishDate = DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm");
                }

                //----  ----//
                await _appDBContext.Posts.AddAsync(postToCreate);
                bool changesPersistToDatabase = await PersistChangeToDatabase();
                if (changesPersistToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong with out side. Please contact the developer");
                }
                else
                {
                    return Created("Create", postToCreate);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Wooops! Something went wrong. Please contact the developer with Error message: {e.Message}.");
            }
        }
        // Update a post
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PostDTO postToUpdateDTO)
        {
            try
            {
                if (id < 1 || postToUpdateDTO == null || id != postToUpdateDTO.PostID ) 
                {
                    return BadRequest(ModelState);
                }
                // Check if a post exists with the passed ID.
                Post oldPost = await _appDBContext.Posts.FindAsync(id);
            
                if (oldPost == null)
                    return NotFound();


                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }
                // MAP
                Post postToUpdate = _mapper.Map<Post>(postToUpdateDTO);

                if (postToUpdate.IsPublished == true)
                {
                    if (oldPost.IsPublished == false)
                    {
                        postToUpdate.PublishDate = DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm");
                    }
                    else
                    {
                        postToUpdate.PublishDate = oldPost.PublishDate;
                    }
                }
                else
                {
                    postToUpdate.PublishDate = string.Empty;
                }

                _appDBContext.Entry(oldPost).State = EntityState.Detached;
                _appDBContext.Posts.Update(postToUpdate);

                bool changesPersistToDatabase = await PersistChangeToDatabase();
                if (changesPersistToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong with out side. Please contact the developer");
                }
                else
                {
                    return Created("Create", postToUpdate);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Wooops! Something went wrong. Please contact the developer with Error message: {e.Message}.");
            }
        }
        // Delete a post
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest(ModelState);
                }
                // Check if a post exists with the passed ID.
                bool exits = await _appDBContext.Posts.AnyAsync(post => post.PostID == id);
                if (exits == false)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }
                // Get post
                Post postToDelete = await GetPostByPostID(id);
                // If image uploaded delete also
                if (postToDelete.PostImage != "uploads/placeholder.jpg")
                {
                    string fileName = postToDelete.PostImage.Split('/').Last();
                    System.IO.File.Delete($"{_webHostEnvironment.ContentRootPath}\\wwwroot\\uploads\\{fileName}");
                }
                _appDBContext.Posts.Remove(postToDelete);
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
        private async Task<Post> GetPostByPostID(int postID)
        {

            Post postToGet = await _appDBContext.Posts
                    .Include(post => post.Category)
                    .FirstAsync(post => post.PostID == postID);
            return postToGet;
        }

        #endregion

    }
}
