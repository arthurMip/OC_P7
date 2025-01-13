using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Repositories;
using Serilog;

namespace P7CreateRestApi.Controllers;

[Authorize(Roles = "Admin,User")]
[ApiController]
[Route("api/[controller]")]
public class BidListController : ControllerBase
{
    private readonly BidListRepository _bidListRepository;

    public BidListController(BidListRepository bidListRepository)
    {
        _bidListRepository = bidListRepository;
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetBidList(int id)
    {
        Log.Information("GetBidList for {Id}", id);

        var bidList = await _bidListRepository.GetBidListByIdAsync(id);
        return bidList == null ? NotFound() : Ok(bidList);
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddBidList([FromBody] BidList bidList)
    {
        Log.Information("AddBidList for {BidList}", bidList);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _bidListRepository.CreateBidListAsync(bidList);

        return Ok(bidList);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateBidList(int id, [FromBody] BidList bidList)
    {
        Log.Information("UpdateBidList for {Id}", id);

        bool exists = await _bidListRepository.BidListExistsAsync(id);

        if (!exists)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bidList.BidListId = id;
        await _bidListRepository.UpdateBidListAsync(bidList);

        return Ok(bidList);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteBidList(int id)
    {
        Log.Information("DeleteBidList for {Id}", id);

        bool deleted = await _bidListRepository.DeleteBidListAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}