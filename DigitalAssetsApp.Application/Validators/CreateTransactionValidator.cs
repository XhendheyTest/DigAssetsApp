using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalAssetsApp.Application.DTOS;
using FluentValidation;

namespace DigitalAssetsApp.Application.Validators
{
    public class CreateTransactionValidator : AbstractValidator<CreateTransactionDto>
    {
        public CreateTransactionValidator()
        {
            RuleFor(x => x.FromAddress)
                .NotEmpty().WithMessage("FromAddress is required");

            RuleFor(x => x.ToAddress)
                .NotEmpty().WithMessage("ToAddress is required");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0");

            RuleFor(x => x)
                .Must(x => x.FromAddress != x.ToAddress)
                .WithMessage("Sender and receiver cannot be the same");
        }
    }
}
