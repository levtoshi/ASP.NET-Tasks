using HomeWork_06._02._2026.Models;
using HomeWork_06._02._2026.Services.BookService;
using HomeWork_06._02._2026.Services.ValidDescriptionService;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork_06._02._2026.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            return Ok(await _bookService.GetBooks());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook([FromRoute] int id)
        {
            Book book;

            try
            {
                book = await _bookService.GetBook(id);
            }
            catch (ArgumentException ex)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            Book createdBook;

            try
            {
                createdBook = await _bookService.AddBook(book);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetBook), new { Id = createdBook.Id }, createdBook);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] Book book)
        {
            Book updatedBook;

            try
            {
                updatedBook = await _bookService.UpdateBook(book);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            try
            {
                await _bookService.DeleteBook(id);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}