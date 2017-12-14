using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalogMVC.Services;

namespace ProductCatalogMVC
{
    public class Startup
    {
      

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IProductRepository>(new ProductRepository());
            services.AddMvc();
        }

       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseMvc(routes => routes.MapRoute(
                name: "default",
                template: "{controller=Product}/{action=Index}/{id?}"));
        }
    }
}
