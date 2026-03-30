using DigitalAssetsApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DigitalAssetsApp.API.Controllers
{
    [ApiController]
    [Route("api/wallet")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet("{address}")]
        public async Task<IActionResult> GetBalance(string address)
        {
            var balance = await _walletService.GetBalanceAsync(address);
            return Ok(new { address, balance });
        }
    }
}
