using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("users")]
public class UsersController(IUserService service) : ControllerBase
{
    private readonly IUserService _service = service;

    // GET /users
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    // GET /users/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _service.GetById(id);
        return user is not null ? Ok(user) : NotFound();
    }

    // POST /users
    [HttpPost]
    public IActionResult Create(User user)
    {
        var created = _service.Add(user);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT /users/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, User user)
    {
        var updated = _service.Update(id, user);
        return updated ? NoContent() : NotFound();
    }

    // DELETE /users/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _service.Delete(id);
        return deleted ? NoContent() : NotFound();
    }
}
