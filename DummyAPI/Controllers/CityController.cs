using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DummyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;

        public CityController(ILogger<CityController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<City> Get()
        {
            return new List<City>()
            {
                new City()
                {
                    Name = "Seatle",
                    Population = 1,
                    State = "Washington"
                },
                new City()
                {
                    Name = "Kansas City",
                    Population = 2,
                    State = "Kansas"
                },
                new City()
                {
                    Name = "Atlanta",
                    Population = 3,
                    State = "Georgia"
                }
            };
        }
    }
}
