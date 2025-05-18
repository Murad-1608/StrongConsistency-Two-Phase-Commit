using Coordinator.Models.Contexts;
using Coordinator.Services;
using Coordinator.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TwoPhaseCommitContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddHttpClient("OrderApi", client => client.BaseAddress = new Uri("https://localhost:7183/"));
builder.Services.AddHttpClient("StockApi", client => client.BaseAddress = new Uri("https://localhost:7171/"));
builder.Services.AddHttpClient("PaymentApi", client => client.BaseAddress = new Uri("https://localhost:7071/"));

builder.Services.AddSingleton<ITransactionService, TransactionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/create-order-transaction", async (ITransactionService transactionService) =>
{
    var transactionId = await transactionService.CreateTransactionAsync();
    await transactionService.PrepareServiceAsync(transactionId);
    bool transactionState = await transactionService.CheckReadyServiceAsync(transactionId);
    if (transactionState)
    {
        await transactionService.CommitAsync(transactionId);
        transactionState = await transactionService.CheckTransactionStateServicesAsync(transactionId);
    }

    if (!transactionState)
        await transactionService.RollbackAsync(transactionId);
});

app.Run();
