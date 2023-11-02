using DistributorStore.Data.ApplicationDbContext;
using DistributorStore.Data.Uow;
using DistributorStore.Operation.Services.OrderS;
using DistributorStore.Operation.Services.ProductS;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //Adding DbContext
        builder.Services.AddDbContext<DistributorStoreDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection")));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        //adding JWT token 
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
                ValidAudience = builder.Configuration["JwtConfig:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Secret"]))
            };
        });
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
    }
}