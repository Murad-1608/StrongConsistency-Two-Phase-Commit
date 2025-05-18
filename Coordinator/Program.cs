using Coordinator.Models.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TwoPhaseCommitContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
}); 

builder.Services.AddHttpClient("OrderApi", client => client.BaseAddress = new Uri("https://localhost:7183/"));
builder.Services.AddHttpClient("StockApi", client => client.BaseAddress = new Uri("https://localhost:7171/"));
builder.Services.AddHttpClient("PaymentApi", client => client.BaseAddress = new Uri("https://localhost:7071/"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
