using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Repositories;

namespace Dot.Net.WebApi.Controllers;

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
        var rating = await _ratingRepository.GetRatingByIdAsync(id);
        return rating == null ? NotFound() : Ok(rating);
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddRating([FromBody] Rating rating)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _ratingRepository.CreateRatingAsync(rating);

        return Ok(rating);
    }

    [HttpPost]
    [Route("update/{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateRating(int id, [FromBody] Rating rating)
    {
        bool exists = await _ratingRepository.RatingExistsAsync(id);

        if (!exists)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        rating.Id = id;
        bool updated = await _ratingRepository.UpdateRatingAsync(rating);

        if (!updated)
        {
            return NotFound();
        }

        return Ok(rating);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteRating(int id)
    {
        bool deleted = await _ratingRepository.DeleteRatingAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}