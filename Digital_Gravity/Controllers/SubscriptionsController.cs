using AutoMapper;
using Digital_Gravity.Models;
using Digital_Gravity.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Digital_Gravity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IRepository<Subscription> _subscriptionRepository;
        private readonly IMapper _mapper;

        public SubscriptionsController(IRepository<Subscription> subscriptionRepository, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        [HttpGet("GetSubscriptions")]
        [Authorize]
        public async Task<IActionResult> GetSubscriptions(
        [FromQuery] string? searchTerm,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); // Get user ID from the JWT token

            var result = await _subscriptionRepository.GetUserSubscriptionsAsync(userId, searchTerm, pageNumber, pageSize);

            return Ok(result);
        }
    }
}
