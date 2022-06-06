using HolidaysUkApi.Models;
using HolidaysUkApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HolidaysUkApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HolidaysController : Controller
    {
        private readonly ILogger<HolidaysController> _logger;
        private readonly IUkHolidayService _service;

        public HolidaysController(ILogger<HolidaysController> logger, IUkHolidayService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ItemReturn), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetHolidays([FromBody] FilterParams filterParams)
        {
            var ret = await _service.GetHolidayUk(filterParams);
            return Ok(ret);
        }
    }
}
