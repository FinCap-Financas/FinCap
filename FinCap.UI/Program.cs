using FinCap.Application.Services;
using FinCap.Data.Context;
using FinCap.Data.Repositories;
using FinCap.Domain.Business;
using FinCap.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinCap.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<FinCapDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("FinCapDB"));
                options.EnableSensitiveDataLogging();
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            });

            builder.Services.AddScoped<ServiceFactory>();
            builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            builder.Services.AddScoped<BusinessFactory>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseExceptionHandler("/Error");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
