
using KnowledgeHubPortal.Data;
using KnowledgeHubPortal.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeHubPortal.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //inject dbcontext
            builder.Services.AddDbContext<KnowledgeHubPortalDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // inject dbcontext using built in IoC
            string conStr = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppIdentityDBContext>(options =>
            {

                options.UseSqlServer(conStr);

            });
            // Step 1: Inject IdentityDbContext
            builder.Services.AddDbContext<AppIdentityDBContext>(options =>
            {
                options.UseSqlServer(conStr);
            });

            //Step 2: Configure API Endpoints for Identity
            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDBContext>();

            //Step 3: Add Authorization middleware into pipeline
            builder.Services.AddAuthorization();


            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowOrigins",
                        builder =>
                        {
                            //builder.WithOrigins("https://localhost:4200", "http://localhost:4200");
                            builder.AllowAnyHeader();
                            builder.AllowAnyMethod();
                            builder.AllowAnyOrigin();
                        }
                    );
            });

            var app = builder.Build();

            //Step 4: Map Identity API
            app.MapIdentityApi<IdentityUser>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowOrigins");
            app.UseAuthorization();

            

            app.MapControllers();

            app.Run();
        }
    }
}
