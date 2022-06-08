
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MagniboardBackend.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MagniboardDbConnection>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MagniboardDbConnection") ?? throw new InvalidOperationException("Connection string 'MagniboardDbConnection' not found.")));

// Add services to the container.
var connString = builder.Configuration.GetConnectionString("MagniboardDbConnection");
builder.Services.AddDbContext<MagniboardDbConnection>(options => options.UseSqlServer(connString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
