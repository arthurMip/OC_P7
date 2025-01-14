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
public class RatingController : ControllerBase
{
    private readonly RatingRepository _ratingRepository;

    public RatingController(RatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetRating(int id)
    {
        var userId = User.GetUserId();
        var rating = await _ratingRepository.GetRatingByIdAsync(id);
        if (rating is null)
        {
            Log.Warning("GetRating for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }
        else
        {
            Log.Information("GetRating for {Id} by user: {User} ok", id, userId);
            return Ok(rating);
        }
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddRating([FromBody] Rating rating)
    {
        var userId = User.GetUserId();
        if (!ModelState.IsValid)
        {
            Log.Warning("AddRating by user: {User} bad request", userId);
            return BadRequest(ModelState);
        }

        await _ratingRepository.CreateRatingAsync(rating);
        Log.Information("AddRating by user: {User} ok", userId);
        return Ok(rating);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateRating(int id, [FromBody] Rating rating)
    {
        var userId = User.GetUserId();
        bool exists = await _ratingRepository.RatingExistsAsync(id);
        if (!exists)
        {
            Log.Warning("UpdateRating for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            Log.Warning("UpdateRating for {Id} by user: {User} bad request", id, userId);
            return BadRequest(ModelState);
        }

        rating.Id = id;
        bool updated = await _ratingRepository.UpdateRatingAsync(rating);

        if (!updated)
        {
            Log.Warning("UpdateRating for {Id} by user: {User} bad request", id, userId);
            return BadRequest();
        }

        Log.Information("UpdateRating for {Id} by user: {User} ok", id, userId);
        return Ok(rating);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteRating(int id)
    {
        var userId = User.GetUserId();
        bool deleted = await _ratingRepository.DeleteRatingAsync(id);
        if (deleted)
        {
            Log.Information("DeleteRating for {Id} by user: {User} no content", id, userId);
            return NoContent();
        }
        else
        {
            Log.Warning("DeleteRating for {Id} by user: {User} not found", id, userId);
            return NotFound();
        }
    }
}