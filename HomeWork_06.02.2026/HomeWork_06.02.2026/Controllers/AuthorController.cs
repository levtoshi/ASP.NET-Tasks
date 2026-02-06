using HomeWork_06._02._2026.Models;
using HomeWork_06._02._2026.Services.AuthorService;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork_06._02._2026.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            return Ok(await _authorService.GetAuthors());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor([FromRoute] int id)
        {
            Author author;

            try
            {
                author = await _authorService.GetAuthor(id);
            }
            catch (ArgumentException ex)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] Author author)
        {
            Author createdAuthor;

            try
            {
                createdAuthor = await _authorService.AddAuthor(author);
            }
            catch (ArgumentException ex)
            {
                return NotFound();
            }

            return Ok(createdAuthor);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAuthor([FromBody] Author author)
        {
            Author updatedAuthor;

            try
            {
                updatedAuthor = await _authorService.UpdateAuthor(author);
            }
            catch (ArgumentException ex)
            {
                return NotFound();
            }

            return Ok(updatedAuthor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            try
            {
                await _authorService.DeleteAuthor(id);
            }
            catch (ArgumentException ex)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}