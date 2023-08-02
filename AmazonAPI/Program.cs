using Amazon.Application.Contracts;
using Amazon.Application.Services;
using Amazon.Context;
using Amazon.Domain;
using Amazon.Infrastructure;
using Amazon.Infrastrucure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace AmazonAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetIsOriginAllowed(origin => origin == "https://member5-8.smarterasp.net/cp/filemanager.asp?d=h:"); ;
                });
            });

            builder.Services.AddControllers().AddJsonOptions(op =>
            {
                op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("SecretKey").Value);
            builder.Services.AddAuthentication().AddJwtBearer(options =>
            {
                options.SaveToken = true;
                TokenValidationParameters token = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
            }).AddEntityFrameworkStores<ApplicationContext>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            
            builder.Services.AddScoped<IProductReposatory,ProductReposatory>();
            builder.Services.AddScoped<IProductServices, ProductSrvices>();
            builder.Services.AddScoped<ICategoryReposatory, CategoryReposatory>();
            builder.Services.AddScoped<IcategoryServices,CategoryService>();
            builder.Services.AddScoped<ISubCategoryReposatory,SubCategoryReposatory>();
            builder.Services.AddScoped<ISubcategoryServices,SubCategoryService>();
            builder.Services.AddScoped<IOrderItemReposatory, OrderItemReposatory>();
            builder.Services.AddScoped<IOrderItemService,OrderItemServices>();
            builder.Services.AddScoped<IImageReposatory,ImageReposatory>();
            builder.Services.AddScoped<IOrderReposatory, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IRatingRepository, RatingRepository>();
            builder.Services.AddScoped<IRatingService, RatingService>();
            builder.Services.AddScoped<IcountryReposatory, CountryReposatory>();
            builder.Services.AddScoped<ICountryServices, CountryServices>();
            builder.Services.AddScoped<ICityReposatory, CityReposatory>();
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<IShippingAddressRepository, ShippingAddressReposatory>();
            builder.Services.AddScoped<IShippingAddressServices, ShippingAddressServices>();
            builder.Services.AddScoped<IUserReposatory, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseCors();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

           /* app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features;
                   
                var response = new { error =$"This value is already taken" };
                await context.Response.WriteAsJsonAsync(response);
            }));*/

            app.MapControllers();

            app.Run();
        }
    }
}