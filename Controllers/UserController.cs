using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Interfaces;
using IdentityApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityApi.Controllers
{
    [Authorize]
    [ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // Get all users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    // Get user by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<UserModel>> GetUserById(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    // Add a new user
    [HttpPost]
    public async Task<ActionResult<UserModel>> AddUser([FromBody] UserModel userModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var addedUser = await _userService.AddUserAsync(userModel);
        return Ok(addedUser);
    }

    // Update user details
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UserModel userModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedUser = await _userService.UpdateUserAsync(id, userModel);
        if (updatedUser == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    // Delete a user
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var deletedUser = await _userService.DeleteUserAsync(id);
        if (deletedUser == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}
}