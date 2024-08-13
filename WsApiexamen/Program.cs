using WsApiexamen.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BdiExamenContext>(opcs => 
{
    opcs.UseSqlServer(builder.Configuration.GetConnectionString("CadenaConexion"));
}
);

builder.Services.AddCors(opcs => 
opcs.AddPolicy("Politica", app =>
{
    app.AllowAnyHeader().
    AllowAnyMethod().
    AllowAnyOrigin();
})
    
    );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Politica");

app.UseAuthorization();

app.MapControllers();

app.Run();
