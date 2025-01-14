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
public class TradeController : ControllerBase
{
    private readonly TradeRepository _tradeRepository;

    public TradeController(TradeRepository tradeRepository)
    {
        _tradeRepository = tradeRepository;
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetTrade(int id)
    {
        var userId = User.GetUserId();
        var trade = await _tradeRepository.GetTradeByIdAsync(id);
        if (trade is null)
        {
            Log.Warning("GetTrade for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }
        else
        {
            Log.Information("GetTrade for {Id} by user: {User} ok found", id, userId);
            return Ok(trade);
        }
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddTrade([FromBody] Trade trade)
    {
        var userId = User.GetUserId();
        if (!ModelState.IsValid)
        {
            Log.Warning("AddTrade by user: {User} bad request", userId);
            return BadRequest(ModelState);
        }

        await _tradeRepository.CreateTradeAsync(trade);
        Log.Information("AddTrade by user: {User} ok", userId);
        return Ok(trade);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateTrade(int id, [FromBody] Trade trade)
    {
        var userId = User.GetUserId();
        bool exists = await _tradeRepository.TradeExistsAsync(id);
        if (!exists)
        {
            Log.Warning("UpdateTrade for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            Log.Warning("UpdateTrade for {Id} by user: {User} bad request", id, userId);
            return BadRequest(ModelState);
        }

        trade.TradeId = id;
        bool updated = await _tradeRepository.UpdateTradeAsync(trade);
        if (!updated)
        {
            Log.Warning("UpdateTrade for {Id} by user: {User} bad request", id, userId);
            return BadRequest();
        }

        Log.Information("UpdateTrade for {Id} by user: {User} ok", id, userId);
        return Ok(trade);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteTrade(int id)
    {
        var userId = User.GetUserId();
        bool deleted = await _tradeRepository.DeleteTradeAsync(id);
        if (deleted)
        {
            Log.Information("DeleteTrade for {Id} by user: {User} no content", id, userId);
            return NoContent();
        }
        else
        {
            Log.Warning("DeleteTrade for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }
    }
}