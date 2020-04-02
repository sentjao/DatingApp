using DatingApi.NET.Data;
using Microsoft.AspNetCore.Mvc;

namespace DatingApi.NET.Controllers
{
    [Route("seeddata")]
    public class SeedDataController : ControllerBase
    {
        private readonly ISeedData seedData;
        public SeedDataController(ISeedData seedData)
        {
            this.seedData = seedData;

        }

        [HttpGet]
        public void Seed()
        {
            seedData.Seed();
        }
    }
}