using Server.Data;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// CONFIGURE FOR SQLITE
builder.Services.AddDbContext<AppDBContext>(options =>
    // Point to appsettings configuration
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); // THIS IS THE WAY
// Tested from https://docs.microsoft.com/en-us/ef/core/dbcontext-configuration/

//----
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// New - Routting and controllers :D
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Added static files
app.UseStaticFiles();



app.Run();

