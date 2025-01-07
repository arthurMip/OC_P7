using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers;

[Authorize(Roles = "Admin,User")]
[ApiController]
[Route("api/[controller]")]
public class CurvePointController : ControllerBase
{
    private readonly CurvePointRepository _curvePointRepository;

    public CurvePointController(CurvePointRepository curvePointRepository)
    {
        _curvePointRepository = curvePointRepository;
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCurvePoint(int id)
    {
        var curvePoint = await _curvePointRepository.GetCurvePointByIdAsync(id);
        return curvePoint == null ? NotFound() : Ok(curvePoint);
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddCurvePoint([FromBody] CurvePoint curvePoint)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _curvePointRepository.CreateCurvePointAsync(curvePoint);

        return Ok(curvePoint);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateCurvePoint(int id, [FromBody] CurvePoint curvePoint)
    {
        bool exists = await _curvePointRepository.CurvePointExistsAsync(id);

        if (!exists)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        curvePoint.Id = id;
        bool updated = await _curvePointRepository.UpdateCurvePointAsync(curvePoint);

        if (!updated)
        {
            return NotFound();
        }

        return Ok(curvePoint);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteCurvePoint(int id)
    {
        bool deleted = await _curvePointRepository.DeleteCurvePointAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}