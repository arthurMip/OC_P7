using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Repositories;

namespace Dot.Net.WebApi.Controllers;

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
        var ruleName = await _ruleNameRepository.GetRuleNameByIdAsync(id);
        return ruleName == null ? NotFound() : Ok(ruleName);
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddRuleName([FromBody] RuleName ruleName)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _ruleNameRepository.CreateRuleNameAsync(ruleName);

        return Ok(ruleName);
    }

    [HttpPost]
    [Route("update/{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateRuleName(int id, [FromBody] RuleName ruleName)
    {
        bool exists = await _ruleNameRepository.RuleNameExistsAsync(id);

        if (!exists)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        ruleName.Id = id;
        bool updated = await _ruleNameRepository.UpdateRuleNameAsync(ruleName);

        if (!updated)
        {
            return NotFound();
        }

        return Ok(ruleName);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteRuleName(int id)
    {
        bool deleted = await _ruleNameRepository.DeleteRuleNameAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}