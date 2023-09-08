using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies; // Import the namespace for CookieAuthenticationDefaults
using MIS.Controllers;

namespace MIS
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // Add DbContext and other services...
            services.AddDbContext<ApplicationDb>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Other services...
        }
    }
    }
