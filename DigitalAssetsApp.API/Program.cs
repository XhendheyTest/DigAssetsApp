using System;
using DigitalAssetsApp.API.Middleware;
using DigitalAssetsApp.Application.Interfaces;
using DigitalAssetsApp.Application.Services;
using DigitalAssetsApp.Application.Validators;
using DigitalAssetsApp.Infrastructure.Data;
using DigitalAssetsApp.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;




Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    
    .WriteTo.File(
        path: "logs/app-.txt",         
        rollingInterval: RollingInterval.Day, 
        retainedFileCountLimit: 7      
    )
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// validators register
builder.Services.AddValidatorsFromAssemblyContaining<CreateTransactionValidator>();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBlockchainService, BlockchainService>();

builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<IAppDbContext>(provider =>
    provider.GetRequiredService<AppDbContext>());



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Digital Assets API",
        Version = "v1",
        Description = "Technical Assessment - Web3 Simulation API"
    });
});


var app = builder.Build();


    

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await SeedData.InitializeAsync(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DigitalAssets API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

 
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();


app.MapControllers();



app.Run();




