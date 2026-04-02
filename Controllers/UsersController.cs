using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.DTOs;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _service;

    public UsersController(IUserService service, ILogger<UsersController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // GET /users
    [HttpGet]
    public IActionResult GetAll()
    {
        _logger.LogInformation("Retrieving all users");

        var users = _service.GetAll()
            .Select(u => new UserReadDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            });

        if (users.Any())
        {
            _logger.LogInformation("{Count} users retrieved", users.Count());
        }
        else
        {
            _logger.LogInformation("No users found");
        }

        return Ok(users);
    }

    // GET /users/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        _logger.LogInformation("Retrieving user {Id}", id);
        var user = _service.GetById(id);

        if (user is null)
        {
            _logger.LogWarning("User {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("User {Id} retrieved successfully", id);

        return Ok(new UserReadDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        });
    }

    // POST /users
    [HttpPost]
    public IActionResult Create(UserCreateDto dto)
    {
        _logger.LogInformation("Creating user {Email}", dto.Email);

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for creating user {Email}", dto.Email);
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Model state valid for creating user {Email}", dto.Email);

        try
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            var created = _service.Add(user);
            _logger.LogInformation("User {Email} created successfully with Id {Id}", dto.Email, created.Id);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, new UserReadDto
            {
                Id = created.Id,
                FirstName = created.FirstName,
                LastName = created.LastName,
                Email = created.Email
            });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error creating user");
            return BadRequest(new { error = ex.Message });
        }
    }

    // PUT /users/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, UserUpdateDto dto)
    {
        _logger.LogInformation("Updating user {Id}", id);

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for updating user {Id}", id);
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Model state valid for updating user {Id}", id);

        try
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            var updated = _service.Update(id, user);
            if (!updated)
            {
                _logger.LogWarning("User {Id} not found for update", id);
                return NotFound();
            }

            _logger.LogInformation("User {Id} updated successfully", id);
            return Ok(new UserReadDto
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Error updating user {Id}", id);
            return BadRequest(new { error = ex.Message });
        }
    }

    // DELETE /users/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Deleting user {Id}", id);

        var deleted = _service.Delete(id);

        if (!deleted)
        {
            _logger.LogWarning("User {Id} not found for deletion", id);
            return NotFound();
        }

        _logger.LogInformation("User {Id} deleted successfully", id);
        return NoContent();
    }
}