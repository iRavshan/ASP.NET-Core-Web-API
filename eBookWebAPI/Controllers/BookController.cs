using Contracts;
using Entities.Models.Book;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBookWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> logger;
        private readonly IRepositoryManager repoManager;

        public BookController(ILogger<BookController> logger, IRepositoryManager repoManager)
        {
            this.logger = logger;
            this.repoManager = repoManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await repoManager.Book.GetAll();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var item = await repoManager.Book.GetById(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                book.Id = Guid.NewGuid();
                await repoManager.Book.Create(book);
                await repoManager.SaveChangesAsync();
                return CreatedAtAction("GetItem", new { book.Id }, book);
            }
            return new JsonResult("Smthing went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Book book)
        {
            if (id != book.Id)
                return BadRequest();
            await repoManager.Book.Update(book);
            await repoManager.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var obj = await repoManager.Book.GetById(id);
            if (obj == null)
                return BadRequest();
            await repoManager.Book.Delete(id);
            await repoManager.SaveChangesAsync();
            return Ok(obj);
        }
    }
}
