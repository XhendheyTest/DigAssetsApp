using DigitalAssetsApp.Application.DTOS;
using DigitalAssetsApp.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace DigitalAssetsApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionsController(ITransactionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionDto dto)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(
                    ModelState.Values.SelectMany(v => v.Errors)
                        .Select(e => new FluentValidation.Results.ValidationFailure("", e.ErrorMessage)));

            var tx = await _service.CreateTransactionAsync(
                dto.FromAddress,
                dto.ToAddress,
                dto.Amount);

            return Ok(tx);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }
    
    }
}
