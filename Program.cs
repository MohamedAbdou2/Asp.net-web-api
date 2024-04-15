
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ITIEntity>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("c1"));
            });
            builder.Services.AddCors(CorsOptions => {

                CorsOptions.AddPolicy("MyPolicy", CorsPolicyBuilder =>
                {

                    CorsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

                });
            
            
            });
            //custom service
            builder.Services.AddScoped<IEmployeeRepo,EmployeeRepo>();
            builder.Services.AddScoped<IProductRepo, ProductRepo>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            //settings for cors policy
            app.UseCors("MyPolicy");  //policy block or open
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
