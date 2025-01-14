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
        var userId = User.GetUserId();
        var curvePoint = await _curvePointRepository.GetCurvePointByIdAsync(id);
        if (curvePoint is null)
        {
            Log.Warning("GetBidList for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }
        else
        {
            Log.Information("GetBidList for {Id} by user: {User} ok", id, userId);
            return Ok(curvePoint);
        }
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddCurvePoint([FromBody] CurvePoint curvePoint)
    {
        var userId = User.GetUserId();
        if (!ModelState.IsValid)
        {
            Log.Warning("AddCurvePoint by user: {User} bad request", userId);
            return BadRequest(ModelState);
        }

        await _curvePointRepository.CreateCurvePointAsync(curvePoint);
        Log.Information("AddCurvePoint by user: {User} ok", userId);
        return Ok(curvePoint);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateCurvePoint(int id, [FromBody] CurvePoint curvePoint)
    {
        var userId = User.GetUserId();
        bool exists = await _curvePointRepository.CurvePointExistsAsync(id);
        if (!exists)
        {
            Log.Warning("UpdateBidList for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            Log.Warning("UpdateBidList for {Id} by user: {User} bad request", id, userId);
            return BadRequest(ModelState);
        }

        curvePoint.Id = id;
        bool updated = await _curvePointRepository.UpdateCurvePointAsync(curvePoint);
        if (!updated)
        {
            Log.Warning("UpdateBidList for {Id} by user: {User} bad request", id, userId);
            return BadRequest();
        }

        Log.Information("UpdateBidList for {Id} by user: {User} ok", id, userId);
        return Ok(curvePoint);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteCurvePoint(int id)
    {
        var userId = User.GetUserId();
        bool deleted = await _curvePointRepository.DeleteCurvePointAsync(id);
        if (deleted)
        {
            Log.Information("UpdateBidList for {Id} by user: {User} no content", id, userId);
            return NoContent();
        }
        else
        {
            Log.Warning("UpdateBidList for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }
    }
}