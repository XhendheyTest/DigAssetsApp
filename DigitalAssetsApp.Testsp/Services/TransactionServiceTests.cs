using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalAssetsApp.Application.Interfaces;
using DigitalAssetsApp.Application.Services;
using DigitalAssetsApp.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DigitalAssetsApp.Testsp.Services
{
    public class TransactionServiceTests
    {
        private readonly Mock<IWalletService> _walletMock = new();
        private readonly Mock<IBlockchainService> _blockchainMock = new();
        private readonly Mock<IAppDbContext> _dbMock = new();

        private readonly TransactionService _service;
        private static DbSet<T> MockDbSet<T>(List<T> list) where T : class
        {
            var queryable = list.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(list.Add);

            return dbSet.Object;
        }
    
        public TransactionServiceTests()
        {
            _service = new TransactionService(
                _walletMock.Object,
                _blockchainMock.Object,
                _dbMock.Object);
        }
        #region Validation Tests
        
        [Fact]
        public async Task Should_Throw_Exception_When_Insufficient_Balance()
        {
            // Arrange
            _walletMock
                .Setup(x => x.HasSufficientBalance("wallet1", 100))
                .ReturnsAsync(false);

            // Act
            var act = async () =>
                await _service.CreateTransactionAsync("wallet1", "wallet2", 100);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Insufficient balance");
        }

        #endregion
        #region Success Tests
       
        [Fact]
        public async Task Should_Create_Transaction_When_Balance_Is_Sufficient()
        {
            // Arrange
            _walletMock
                .Setup(x => x.HasSufficientBalance("wallet1", 100))
                .ReturnsAsync(true);

            _blockchainMock
                .Setup(x => x.GenerateTransactionHash())
                .Returns("0xABC123");

            // Act
            var result = await _service.CreateTransactionAsync("wallet1", "wallet2", 100);

            // Assert
            result.Should().NotBeNull();
            result.Amount.Should().Be(100);
            result.TxHash.Should().Be("0xABC123");
        }
        #endregion
        #region Persistence Tests
       

        [Fact]
        public async Task Should_Save_Transaction_To_Database()
        {
            // Arrange
            _walletMock
                .Setup(x => x.HasSufficientBalance(It.IsAny<string>(), It.IsAny<decimal>()))
                .ReturnsAsync(true);

            _blockchainMock
                .Setup(x => x.GenerateTransactionHash())
                .Returns("0xHASH");

            var transactions = new List<Transaction>();

            _dbMock
                .Setup(x => x.Transactions)
                .Returns(MockDbSet(transactions));

            // Act
            await _service.CreateTransactionAsync("wallet1", "wallet2", 100);

            // Assert
            transactions.Should().HaveCount(1);
        }
        #endregion
    }
}


