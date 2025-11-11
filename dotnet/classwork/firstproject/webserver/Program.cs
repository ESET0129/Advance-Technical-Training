
using Microsoft.EntityFrameworkCore;
using webserver.Models;

namespace webserver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<BlazorTutorialContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDBConnection")));

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7024/api/") });
            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowMVC",
                    policy =>
                    {
                        policy.WithOrigins("*") // your MVC app URL
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowMVC");
            app.UseHttpsRedirection();
            

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
