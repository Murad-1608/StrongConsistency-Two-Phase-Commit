var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/ready", () =>
{
    Console.WriteLine("Payment service is ready");
    return true;
});
app.MapGet("/commit", () =>
{
    Console.WriteLine("Payment service is commited");
    return true;
});
app.MapGet("/rollback", () =>
{
    Console.WriteLine("Payment service is rollbacked");
    return true;
});

app.Run();
