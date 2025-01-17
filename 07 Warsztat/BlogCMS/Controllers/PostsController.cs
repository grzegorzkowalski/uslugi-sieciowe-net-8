using BlogCMS.Models;
using BlogCMS.Repositories;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IRepository<Post> _postRepository;

    public PostsController(IRepository<Post> postRepository)
    {
        _postRepository = postRepository;
    }

    // GET: api/posts
    [HttpGet]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _postRepository.GetAllAsync();
        return Ok(posts);
    }

    // GET: api/posts/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(int id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    // POST: api/posts
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] Post post)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newPostId = await _postRepository.AddAsync(post);
        return CreatedAtAction(nameof(GetPostById), new { id = newPostId }, post);
    }

    // PUT: api/posts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] Post post)
    {
        if (id != post.Id)
        {
            return BadRequest();
        }

        var result = await _postRepository.UpdateAsync(post);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/posts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost([FromBody] Post post)
    {
        var result = await _postRepository.DeleteAsync(post);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}