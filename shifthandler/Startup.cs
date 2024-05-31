using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using shifthandler.Data;

namespace shifthandler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Retrieve the connection string from appsettings.json
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            // Check if the connection string is not null before calling UseMySQL
            if (connectionString != null)
            {
                // Add Entity Framework Core DbContext with MySQL provider
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseMySQL(connectionString));
            }
            else
            {
                // Handle the case where the connection string is null
                // You might log an error, provide a default connection string, or take another appropriate action.
            }

            // Configure authentication cookies
            services.AddAuthentication("MyCookieAuthenticationScheme")
                .AddCookie("MyCookieAuthenticationScheme", options =>
                {
                    options.Cookie.HttpOnly = false;
                    options.Cookie.SameSite = SameSiteMode.Lax; // Ensure SameSite=None for cross-site usage
                    options.Cookie.Name = "MyAppCookie";
                });

            // Configure session cookies
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = false;
                options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax; // Ensure SameSite=None for cross-site usage
            });

            // Add MVC services
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Use custom error page in production
                app.UseExceptionHandler("/Home/Error");

                // Use HSTS (HTTP Strict Transport Security)
                app.UseHsts();
            }

            // Redirect HTTP requests to HTTPS
            app.UseHttpsRedirection();

            // Serve static files (e.g., CSS, JavaScript)
            app.UseStaticFiles();

            // Enable session
            app.UseSession();

            // Enable routing
            app.UseRouting();

            // Enable authentication and authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Configure endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Shift}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                name: "invitations",
                pattern: "Invitations/{action=Index}/{id?}",
                defaults: new { controller = "Invitations" });

                endpoints.MapControllerRoute(
                name: "workers",
                pattern: "Workers/{action=Index}/{id?}",
                defaults: new { controller = "Workers" });

            });
        }
    }
}
