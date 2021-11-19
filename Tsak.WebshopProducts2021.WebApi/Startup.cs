using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Tsak.WebshopProducts_2021_BE.Core.IServices;
using Tsak.WebshopProducts_2021_BE.Domain.IRepositories;
using Tsak.WebshopProducts_2021_BE.Domain.Services;
using Tsak.WebshopProducts2021.DataAccess;
using Tsak.WebshopProducts2021.DataAccess.Entities;
using Tsak.WebshopProducts2021.DataAccess.Repositories;
using Tsak.WebshopProducts2021.Security;
using Tsak.WebshopProducts2021.Security.Model;
using Tsak.WebshopProducts2021.Security.Services;

namespace Tsak.WebshopProducts2021.WebApi
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Tsak.WebshopProducts2021.WebApi", Version = "v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            
            services.AddDbContext<MainDBContext>(opt =>
            {
                opt.UseSqlite("Data Source=main.db");
            });

            services.AddDbContext<AuthDbContext>(opt =>
            {
                opt.UseSqlite("Data Source=auth.db");
            });
            
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthService, AuthService>();
            
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]
                };
            });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ProductsManager", 
                    policy => policy.RequireClaim("permissions", "ProductsManager"));
                options.AddPolicy("ProductsReader", 
                    policy => policy.RequireClaim("permissions", "ProductsReader"));
                options.AddPolicy("ProfileReader", 
                    policy => policy.RequireClaim("permissions", "ProfileReader"));
            });
            
            services.AddCors(opt =>
                opt.AddPolicy("dev-policy", policy =>
                        {
                            policy
                                .AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        })); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MainDBContext ctx, AuthDbContext authCtx)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tsak.WebshopProducts2021.WebApi v1"));
                app.UseCors("dev-policy");
                
                
                
                new DbSeeder(ctx).SeedDevelopment();

                authCtx.Database.EnsureDeleted();
                authCtx.Database.EnsureCreated();
                authCtx.LoginUsers.Add(new LoginUser
                {
                    UserName = "ljuul",
                    HashedPassword = "123456",
                    DbUserId = 1,
                });
                authCtx.LoginUsers.Add(new LoginUser
                {
                    UserName = "ljuul2",
                    HashedPassword = "123456",
                    DbUserId = 2,
                });
                authCtx.Permissions.AddRange(new Permission()
                {
                    Name = "CanWriteProducts"
                }, new Permission()
                {
                    Name = "CanReadProducts"
                });
                authCtx.SaveChanges();
                authCtx.UserPermissions.Add(new UserPermission { PermissionId = 1, UserId = 1 });
                authCtx.UserPermissions.Add(new UserPermission { PermissionId = 2, UserId = 1 });
                authCtx.UserPermissions.Add(new UserPermission { PermissionId = 2, UserId = 2 });
                authCtx.SaveChanges();
            }
            
            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}