
using Library.DAL.Repositories;
using Library.DAL.Services;
using Microsoft.EntityFrameworkCore;
using Supermarket.Data;
using Supermarket.Data.Interfaces.Repository;
using Supermarket.Data.Interfaces.Services;

namespace Supermarket.API
{
    public class Startup 
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;

            //Para ser possível atualizar as settings em runtime
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();

                builder.WithOrigins("http://localhost:3000", "https://localhost:3000")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.HttpOnly = true;
                options.Cookie.MaxAge = TimeSpan.FromDays(7);
                options.IdleTimeout = TimeSpan.FromDays(7);
                options.IOTimeout = TimeSpan.FromDays(7);
            });

            services.AddDbContext<ApplicationDBContext>(options =>
           options.UseSqlServer(
               Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Supermarket.API")));
            

            services.AddControllers();
            services.AddSignalR();

            services.AddHttpContextAccessor();
            services.AddSwaggerGen();


            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();

            services.AddTransient<IBookService, BooksServices>();
            services.AddTransient<IAuthorService, AuthorService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, ApplicationDBContext applicationDbContext)
        {

            applicationDbContext.Database.Migrate();

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("MyPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseRouting();

            
        }
    }
}
