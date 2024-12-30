using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;


var builder = WebApplication.CreateBuilder(args);

// Connection to Database
builder.Services.AddDbContext<ToDoAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoAPIContext") ?? throw new InvalidOperationException("Connection string 'ToDoAPIContext' not found.")));

// Allow to connect from outside
builder.Services.AddCors(options =>
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3001")
              .WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    })
);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowReactApp");
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
