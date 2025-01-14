using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Extensions;
using Serilog;

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
        var userId = User.GetUserId();
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            Log.Warning("GetUser for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }
        else
        {
            Log.Information("GetUser for {Id} by user: {User} ok", id, userId);
            return Ok(user);
        }
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        var userId = User.GetUserId();
        if (!ModelState.IsValid)
        {
            Log.Warning("AddUser by user: {User} bad request", userId);
            return BadRequest(ModelState);
        }

        var existingUser = await _userManager.FindByNameAsync(user.Username);
        if (existingUser is not null)
        {
            Log.Warning("AddUser by user: {User} bad request, user already exists", userId);
            return BadRequest();
        }

        var newUser = new IdentityUser(user.Username);
        var role = await _roleManager.FindByNameAsync(user.Role);
        if (role is null)
        {
            Log.Warning("AddUser by user: {User} bad request, role not found", userId);
            return BadRequest();
        }

        var result = await _userManager.CreateAsync(newUser, user.Password);
        if (!result.Succeeded)
        {
            Log.Warning("AddUser by user: {User} bad request, errors", userId);
            return BadRequest(result.Errors);
        }

        await _userManager.AddToRoleAsync(newUser, role.Name);
        Log.Information("AddUser by user: {User} ok", userId);
        return Ok();
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
    {
        var userId = User.GetUserId();
        var existingUser = await _userManager.FindByIdAsync(id);
        if (existingUser is null)
        {
            Log.Warning("UpdateUser for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            Log.Warning("UpdateUser for {Id} by user: {User} bad request", id, userId);
            return BadRequest(ModelState);
        }

        if (!string.IsNullOrEmpty(user.Role))
        {
            var role = await _roleManager.FindByNameAsync(user.Role);
            if (role is null)
            {
                Log.Warning("UpdateUser for {Id} by user: {User} bad request, role not found", id, userId);
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
        Log.Information("UpdateUser for {Id} by user: {User} ok", id, userId);
        return Ok(user);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var userId = User.GetUserId();
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            Log.Warning("DeleteUser for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }
        
        await _userManager.DeleteAsync(user);
        Log.Information("DeleteUser for {Id} by user: {User} no content", id, userId);
        return NoContent();
    }
}