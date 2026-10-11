using Microsoft.AspNetCore.Mvc;
using UserManagementApi.DTOs;
using UserManagementApi.Models;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UsersController(IUserService users) : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyCollection<User>> GetAll() => Ok(users.GetAll());

    [HttpGet("{id:guid}")]
    public ActionResult<User> GetById(Guid id)
    {
        var user = users.GetById(id);
        return user is null ? NotFound(new { message = "User not found." }) : Ok(user);
    }

    [HttpPost]
    public ActionResult<User> Create([FromBody] UserRequest request)
    {
        var user = users.Create(request);
        if (user is null) return Conflict(new { message = "Email is already in use." });
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] UserRequest request)
    {
        return users.Update(id, request) switch
        {
            UpdateResult.NotFound => NotFound(new { message = "User not found." }),
            UpdateResult.EmailTaken => Conflict(new { message = "Email is already in use." }),
            _ => NoContent()
        };
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id) => users.Delete(id)
        ? NoContent()
        : NotFound(new { message = "User not found." });
}
