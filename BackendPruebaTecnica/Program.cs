using BackendPruebaTecnica.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BackendPruebaTecnica.Custom;
using BackendPruebaTecnica.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);
//Seleccionar la conexion deseada...
var connectionString = builder.Configuration.GetConnectionString("RemoteConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>( options => 
    options.UseMySql(connectionString,serverVersion));

builder.Services.AddSingleton<Utilities>();
builder.Services.AddAuthentication(config => {
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config => {
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        //Quitamos la opcion para que la url cuente como una verificacion
        ValidateIssuer = false,
        ValidateAudience = false,
        //Valida el tiempo de vida del token
        ValidateLifetime = true,
        //Evitar una desviacion del reloj al momento de expirar un token
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
    };
});

//Activamos cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolicy", app => app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    AppDbContext ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    ctx.Database.EnsureCreated();
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

app.UseHttpsRedirection();

app.UseCors("NewPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
