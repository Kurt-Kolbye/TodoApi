using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using TodoApi.Data;
using TodoApi.Services;

namespace TodoApi
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
            // Added to support cross-origin requests and the ContentType header
            // Swap out localhost with other environments later
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                    builder => builder.WithOrigins("https://localhost:44346", "http://127.0.0.1:5500")
                    .WithHeaders(HeaderNames.ContentType, "application/json")
                    .AllowAnyMethod());
            });

            // Add dependencies including DbContext. QueryTrackingBehavior is disabled to prevent issues with double tracking entities unexpectedly
            services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<ILabelService, LabelService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Added to support cross-origin requests
            app.UseCors("AllowMyOrigin");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
