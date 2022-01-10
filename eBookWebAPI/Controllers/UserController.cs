using Contracts;
using Entities.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eUserWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IRepositoryManager repoManager;

        public UserController(ILogger<UserController> logger, IRepositoryManager repoManager)
        {
            this.logger = logger;
            this.repoManager = repoManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Users = await repoManager.User.GetAll();
            return Ok(Users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var item = await repoManager.User.GetById(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User User)
        {
            if (ModelState.IsValid)
            {
                User.Id = Guid.NewGuid();
                await repoManager.User.Create(User);
                await repoManager.SaveChangesAsync();
                return CreatedAtAction("GetItem", new { User.Id }, User);
            }
            return new JsonResult("Smthing went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, User User)
        {
            if (id != User.Id)
                return BadRequest();
            await repoManager.User.Update(User);
            await repoManager.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var obj = await repoManager.User.GetById(id);
            if (obj == null)
                return BadRequest();
            await repoManager.User.Delete(id);
            await repoManager.SaveChangesAsync();
            return Ok(obj);
        }
    }
}
