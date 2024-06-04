using KnowledgeHubPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeHubPortal.Data
{
    public class KnowledgeHubPortalDbContext : DbContext
    {
        //configure database and map
        public KnowledgeHubPortalDbContext(DbContextOptions<KnowledgeHubPortalDbContext> options) : base(options)
        {
            //web app - mvc.web api
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //non-web app
                optionsBuilder
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .UseLazyLoadingProxies()
                .UseSqlServer("server=INBMWN157056\\SQLEXPRESS;database=HarmanKHdB;Trusted_Connection=true;Trust Server Certificate=True;MultipleActiveResultSets=True");
            }
            
        }


        //configure tables and map
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
    }
}
