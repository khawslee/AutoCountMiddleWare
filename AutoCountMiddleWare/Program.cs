using AutoCountMiddleWare.Model;
using AutoCountMiddleWare.Services;
using AutoCountMiddleWare.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions();
builder.Services.Configure<AutoCountSettings>(
    builder.Configuration.GetSection("AutoCountSettings"));

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<ICreditorService, CreditorService>();

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
