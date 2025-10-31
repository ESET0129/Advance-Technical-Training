using LibraryManagementAPI.Model;
using LibraryManagementAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return Ok(books);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }



        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdBook = await _bookRepository.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBook), new { id = createdBook.BookId }, createdBook);
        }




        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        //{
        //    if (id != book.BookId)
        //        return BadRequest("Book ID mismatch");

        //    var updatedBook = await _bookRepository.UpdateBookAsync(book);
        //    if (updatedBook == null)
        //        return NotFound();

        //    return Ok(updatedBook);
        //}




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var deleted = await _bookRepository.DeleteBookAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }




    }
}
