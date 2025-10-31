using LibraryManagementAPI.Model;
using LibraryManagementAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound();

            return Ok(author);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdAuthor = await _authorRepository.AddAuthorAsync(author);
            return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthor.AuthorID }, createdAuthor);
        }


        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Author author)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        if (id != author.ID)
        //            return BadRequest("Author ID mismatch");
        //        var updatedAuthor = await _authorRepository.UpdateAuthorAsync(author);
        //        if (updatedAuthor == null)
        //            return NotFound();
        //        return Ok(updatedAuthor);
        //    }



            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var deleted = await _authorRepository.DeleteAuthorAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }


    }
}
