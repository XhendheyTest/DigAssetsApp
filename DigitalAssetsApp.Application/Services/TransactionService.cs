using DigitalAssetsApp.Application.Interfaces;
using DigitalAssetsApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigitalAssetsApp.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly IWalletService _walletService;
    private readonly IBlockchainService _blockchain;
    private readonly IAppDbContext _context;

    public TransactionService(
        IWalletService walletService,
        IBlockchainService blockchain,
        IAppDbContext context)
    {
        _walletService = walletService;
        _blockchain = blockchain;
        _context = context;
    }

    public async Task<Transaction> CreateTransactionAsync(string from, string to, decimal amount)
    {
        if (!await _walletService.HasSufficientBalance(from, amount))
            throw new Exception("Insufficient balance");

        await _walletService.DeductBalance(from, amount);

        var tx = new Transaction
        {
            Id = Guid.NewGuid(),
            FromAddress = from,
            ToAddress = to,
            Amount = amount,
            TxHash = _blockchain.GenerateTransactionHash(),
            CreatedAt = DateTime.UtcNow
        };

        _context.Transactions.Add(tx);
        await _context.SaveChangesAsync(CancellationToken.None);

        return tx;
    }

    public async Task<List<Transaction>> GetAllAsync()
    {
        return await _context.Transactions
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }
}