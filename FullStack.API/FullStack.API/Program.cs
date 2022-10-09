using FullStack.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// this builder is type of FullStackDbContext
// to inject dbcontext
builder.Services.AddDbContext<FullStackDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("FullStackConnectionString")));
// this indicate that we are using sql db


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

// to allow talk from one localhost to another localhost
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()) ;

app.Run();
