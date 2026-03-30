using DigitalAssetsApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace DigitalAssetsApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AssetsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Assets.ToList());
        }
    }
}
