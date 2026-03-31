using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalAssetsApp.Application.DTOS;
using DigitalAssetsApp.Application.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace DigitalAssetsApp.Testsp.Validators
{
  

    public class CreateTransactionValidatorTests
    {
        private readonly CreateTransactionValidator _validator;

        public CreateTransactionValidatorTests()
        {
            _validator = new CreateTransactionValidator();
        }

        // FromAddress
        [Fact]
        public void Should_Have_Error_When_FromAddress_Is_Empty()
        {
            var model = new CreateTransactionDto
            {
                FromAddress = "",
                ToAddress = "wallet2",
                Amount = 100
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.FromAddress);
        }

        // ToAddress 
        [Fact]
        public void Should_Have_Error_When_ToAddress_Is_Empty()
        {
            var model = new CreateTransactionDto
            {
                FromAddress = "wallet1",
                ToAddress = "",
                Amount = 100
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.ToAddress);
        }

        // Amount
        [Fact]
        public void Should_Have_Error_When_Amount_Is_Less_Than_Zero()
        {
            var model = new CreateTransactionDto
            {
                FromAddress = "wallet1",
                ToAddress = "wallet2",
                Amount = -10
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Amount);
        }

        // wallet
        [Fact]
        public void Should_Have_Error_When_From_And_To_Are_Same()
        {
            var model = new CreateTransactionDto
            {
                FromAddress = "wallet1",
                ToAddress = "wallet1",
                Amount = 100
            };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x);
        }

        // Case Validate
        [Fact]
        public void Should_Not_Have_Error_When_Data_Is_Valid()
        {
            var model = new CreateTransactionDto
            {
                FromAddress = "wallet1",
                ToAddress = "wallet2",
                Amount = 100
            };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
