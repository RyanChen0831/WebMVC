using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string Northwind=builder.Configuration.GetConnectionString("Northwind");
            builder.Services.AddDbContext<NorthwindContext>(opt => opt.UseSqlServer(Northwind));

            builder.Services.AddControllers();
            //CORS
            string MyCorsPolicy = "AllowLocal";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                name: MyCorsPolicy,
                options => options.WithOrigins("*").WithHeaders("*").WithMethods("*"));
            });
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

            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}