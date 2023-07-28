using Advantage.API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = "User ID=postgres;Password=root;Host=localhost;Port=5432;Database=Advantage.API.Dev;Pooling=true;";
builder.Services.AddDbContext<Advantage.API.Models.ApiContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddTransient<DataSeed>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Aqui você chama o SeedData usando o contexto do banco de dados e passando os argumentos
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dataSeed = serviceProvider.GetRequiredService<DataSeed>();
    dataSeed.SeedData(20, 1000);
}


app.MapControllers();

app.MapGet("/", () => "Hello world");

app.Run();

