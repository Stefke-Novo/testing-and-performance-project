using Microsoft.EntityFrameworkCore;
using ServerApp;
using ServerApp.Services;
using ServerApp.Services.OsobaServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(name: "TestingDatabase"));
});
builder.Services.AddScoped<OsobaService>();
builder.Services.AddScoped<MestoService>();
/*builder.Services.AddScoped<GetAllService>();*/
builder.Services.AddCors(options => 
{
    options.AddPolicy(name: "GeneralPolicy", policy => 
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("GeneralPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
