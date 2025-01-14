using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Extensions;
using P7CreateRestApi.Repositories;
using Serilog;

namespace P7CreateRestApi.Controllers;

[Authorize(Roles = "Admin,User")]
[ApiController]
[Route("api/[controller]")]
public class RuleNameController : ControllerBase
{
    private readonly RuleNameRepository _ruleNameRepository;

    public RuleNameController(RuleNameRepository ruleNameRepository)
    {
        _ruleNameRepository = ruleNameRepository;
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetRuleName(int id)
    {
        var userId = User.GetUserId();
        var ruleName = await _ruleNameRepository.GetRuleNameByIdAsync(id);
        if (ruleName is null)
        {
            Log.Warning("GetRuleName for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }
        else
        {
            Log.Information("GetRuleName for {Id} by user: {User} ok", id, userId);
            return Ok(ruleName);
        }
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddRuleName([FromBody] RuleName ruleName)
    {
        var userId = User.GetUserId();
        if (!ModelState.IsValid)
        {
            Log.Warning("AddRuleName by user: {User} bad request", userId);
            return BadRequest(ModelState);
        }

        await _ruleNameRepository.CreateRuleNameAsync(ruleName);
        Log.Information("AddRuleName by user: {User} ok", userId);
        return Ok(ruleName);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateRuleName(int id, [FromBody] RuleName ruleName)
    {
        var userId = User.GetUserId();
        bool exists = await _ruleNameRepository.RuleNameExistsAsync(id);
        if (!exists)
        {
            Log.Warning("UpdateRuleName for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            Log.Warning("UpdateRuleName for {Id} by user: {User} bad request", id, userId);
            return BadRequest(ModelState);
        }

        ruleName.Id = id;
        bool updated = await _ruleNameRepository.UpdateRuleNameAsync(ruleName);

        if (!updated)
        {
            Log.Warning("UpdateRuleName for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }

        Log.Information("UpdateRuleName for {Id} by user: {User} ok", id, userId);
        return Ok(ruleName);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteRuleName(int id)
    {
        var userId = User.GetUserId();
        bool deleted = await _ruleNameRepository.DeleteRuleNameAsync(id);
        if (deleted)
        {
            Log.Information("DeleteRuleName for {Id} by user: {User} not content", id, userId);
            return NoContent();
        }
        else
        {
            Log.Warning("DeleteRuleName for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }
    }
}