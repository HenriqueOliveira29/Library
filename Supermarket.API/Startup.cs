
using Library.DAL.Repositories;
using Library.DAL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Supermarket.Data;
using Supermarket.Data.Entities;
using Supermarket.Data.Interfaces.Repository;
using Supermarket.Data.Interfaces.Services;
using System.Security.Claims;
using System.Text;

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

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:Key"])),
                    ValidateLifetime = false,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Issuer"],
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidateIssuerSigningKey = true
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.FromResult<object>(null);
                    },
                    OnTokenValidated = context =>
                    {
                        var _userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();

                        var userID = context.Principal.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;

                        if (userID is null)
                        {
                            context.Response.StatusCode = 401;
                            context.Fail("invalid Data");
                            return Task.CompletedTask;
                        }
                        var user = _userManager.Users.Include(u => u.UserRoles).ThenInclude(t => t.Role).Single(o => o.Id == userID);

                        if (user is null) {
                            context.Response.StatusCode = 401;
                            context.Fail("Invalid Data");
                            return Task.CompletedTask;
                        }

                        var jwtUserRoles = context.Principal.Claims.Where(x => x.Type == ClaimTypes.Role).ToList();
                        if (jwtUserRoles.Count == 0) {
                            context.Response.StatusCode = 401;
                            context.Fail("Invalid Data");
                            return Task.CompletedTask;
                        }

                        var userRoles = user.UserRoles.Select(x => x.Role.Name).ToList();
                        if (userRoles.Count == 0) {
                            context.Response.StatusCode = 401;
                            context.Fail("Invalid Data");
                            return Task.CompletedTask;
                        }

                        foreach (var jwtUserRole in jwtUserRoles) {
                            var doesUserHaveRole = userRoles.Contains(jwtUserRole.Value);
                            if (doesUserHaveRole == false) {
                                context.Response.StatusCode = 401;
                                context.Fail("Invalid Data");
                                return Task.CompletedTask;
                            }
                        }
                        return Task.CompletedTask;

                    }
                };
            });

            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IBookService, BooksServices>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IAuthService, AuthService>();

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
            app.UseSession();

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseCors("MyPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseRouting();

            
        }
    }
}
