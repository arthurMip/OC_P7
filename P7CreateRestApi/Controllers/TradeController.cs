using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Repositories;

namespace Dot.Net.WebApi.Controllers;

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
        var trade = await _tradeRepository.GetTradeByIdAsync(id);
        return trade == null ? NotFound() : Ok(trade);
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddTrade([FromBody] Trade trade)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _tradeRepository.CreateTradeAsync(trade);
        return Ok(trade);
    }

    [HttpPost]
    [Route("update/{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateTrade(int id, [FromBody] Trade trade)
    {
        bool exists = await _tradeRepository.TradeExistsAsync(id);

        if (!exists)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        trade.TradeId = id;
        bool updated = await _tradeRepository.UpdateTradeAsync(trade);

        if (!updated)
        {
            return NotFound();
        }

        return Ok(trade);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteTrade(int id)
    {
        bool deleted = await _tradeRepository.DeleteTradeAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}