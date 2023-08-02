using Amazon.Application.Services;
using Amazon.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService ratingService;

        public RatingController(IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }
        [HttpPost]
        [Route("addRate")]
        public async Task<IActionResult> addRate(RatingDTO ratingDTO)
        {
            var rate= await ratingService.createRating(ratingDTO);
            return rate != null ? Ok(rate) : BadRequest();
        }
        [HttpGet]
        [Route("getAllReviews")]
        public async Task<IActionResult> getAllReviewsByProductId(int productId)
        {
            var rate = await ratingService.GetAllByProductIdAsync(productId);
            return rate != null ? Ok(rate) : BadRequest();
        }
        [HttpGet]
        [Route("calculateProductRate")]
        public async Task<IActionResult> calculateProductRate(int productId)
        {
            var rate = await ratingService.calculateProductRate(productId);
            return Ok(rate);
        }
    }
}
