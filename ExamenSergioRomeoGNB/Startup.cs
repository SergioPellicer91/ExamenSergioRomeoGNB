using ExamenSergioRomeoGNB.AppConfig;
using ExamenSergioRomeoGNB.Models;
using ExamenSergioRomeoGNB.Repositories;
using ExamenSergioRomeoGNB.ServiceRequests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExamenSergioRomeoGNB
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
            //App configuration via appsettings.json
            services.Configure<UrlConfig>(Configuration.GetSection("WebServiceUrls"));
            services.Configure<DbConfig>(Configuration.GetSection("LocalDb"));

            //Logging Injection
            services.AddSingleton(NLog.LogManager.LoadConfiguration("NLog.Config"));


            //Dependency injection: Context
            services.AddDbContext<GnbContext>(options => options.UseSqlServer(Configuration["LocalDb:DbConnection"]));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Dependency injection: Repositories
            services.AddScoped<IRepository<Rate>, RatesRepository>();
            services.AddScoped<IRepository<Transaction>, TransactionsRepository>();

            //Dependency injection: Services
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IRateService, RateService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<GnbContext>();
                context.Database.Migrate();
            }


            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
