using CryptoProj.Domain.Models;
using CryptoProj.Domain.Services.Analytics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoProj.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/analytics")]
    public class AnalyticsController : ControllerBase
    {
        private readonly AnalyticsService _analyticsService;

        public AnalyticsController(AnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _analyticsService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AnalyticsItem analyticsItem)
        {
            return CreatedAtAction(nameof(Get), new { id = analyticsItem.Id }, await _analyticsService.Add(analyticsItem));
        }

        [HttpPut]
        public async Task<IActionResult> Update(AnalyticsItem analyticsItem)
        {
            return Ok(await _analyticsService.Update(analyticsItem));
        }
    }
}
