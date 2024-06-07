using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ewybory_dotnet.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using ewybory_dotnet.Services;

namespace ewybory_dotnet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add configuration data
            var connectionString = builder.Configuration.GetConnectionString("System");
            
            // Add context class to resources
            builder.Services.AddDbContext<eElectionContext>(x => x.UseSqlServer(connectionString));
            builder.Services.AddDbContext<AuthorizationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("System")));


            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AuthorizationDbContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthorization();



            builder.Services.AddRazorPages();
            builder.Services.AddHttpClient();
            builder.Services.AddTransient<ReCAPTCHAService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors(
                options => options
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod()
                );
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();
            app.Run();
        }
    }
}
