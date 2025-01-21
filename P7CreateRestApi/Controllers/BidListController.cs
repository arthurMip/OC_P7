using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Extensions;
using P7CreateRestApi.Iterfaces;
using Serilog;

namespace P7CreateRestApi.Controllers;

[Authorize(Roles = "Admin,User")]
[ApiController]
[Route("api/[controller]")]
public class BidListController : ControllerBase
{
    private readonly IGenericRepository<BidList> _bidListRepository;

    public BidListController(IGenericRepository<BidList> bidListRepository)
    {
        _bidListRepository = bidListRepository;
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetBidList(int id)
    {
        var userId = User.GetUserId();
        var bidList = await _bidListRepository.GetByIdAsync(id);
        if (bidList is null)
        {
            Log.Warning("GetBidList for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }
        else
        {
            Log.Information("GetBidList for {Id} by user: {User} ok", id, userId);
            return Ok(bidList);
        }
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddBidList([FromBody] BidList bidList)
    {
        var userId = User.GetUserId();
        if (!ModelState.IsValid)
        {
            Log.Warning("AddBidList by user: {User} bad request", userId);
            return BadRequest(ModelState);
        }

        await _bidListRepository.CreateAsync(bidList);
        Log.Information("AddBidList by user: {User} ok", userId);
        return Ok(bidList);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateBidList(int id, [FromBody] BidList bidList)
    {
        var userId = User.GetUserId();
        bool exists = await _bidListRepository.ExistsAsync(id);
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

        bidList.BidListId = id;
        await _bidListRepository.UpdateAsync(bidList);

        Log.Information("UpdateBidList for {Id} by user: {User} ok", id, userId);
        return Ok(bidList);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteBidList(int id)
    {
        var userId = User.GetUserId();
        bool deleted = await _bidListRepository.DeleteAsync(id);
        if (deleted)
        {
            Log.Information("DeleteBidList for {Id} by user: {User} no content", id, userId);
            return NoContent();
        }
        else
        {
            Log.Warning("DeleteBidList for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }
    }
}