using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingUser = await _userManager.FindByNameAsync(user.Username);
        if (existingUser is not null)
        {
            return BadRequest();
        }

        var newUser = new IdentityUser(user.Username);

        var role = await _roleManager.FindByNameAsync(user.Role);
        if (role is null)
        {
            return BadRequest();
        }

        var result = await _userManager.CreateAsync(newUser, user.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await _userManager.AddToRoleAsync(newUser, role.Name);

        return Ok();
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
    {
        var existingUser = await _userManager.FindByIdAsync(id);
        if (existingUser is null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!string.IsNullOrEmpty(user.Role))
        {
            var role = await _roleManager.FindByNameAsync(user.Role);
            if (role is null)
            {
                return BadRequest();
            }

            var existingRoles = await _userManager.GetRolesAsync(existingUser);
            await _userManager.RemoveFromRolesAsync(existingUser, existingRoles);
            await _userManager.AddToRoleAsync(existingUser, role.Name);
        }

        if (!string.IsNullOrEmpty(user.Username))
        {
            existingUser.UserName = user.Username;
        }

        if (!string.IsNullOrEmpty(user.Password))
        {
            existingUser.PasswordHash = _userManager.PasswordHasher.HashPassword(existingUser, user.Password);
        }

        await _userManager.UpdateAsync(existingUser);

        return Ok(user);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }
        
        await _userManager.DeleteAsync(user);

        return NoContent();
    }
}