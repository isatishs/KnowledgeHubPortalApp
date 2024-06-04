using KnowledgeHubPortal.Domain.Entities;
using KnowledgeHubPortal.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeHubPortal.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly KnowledgeHubPortalDbContext db = null;//new KnowledgeHubPortalDbContext();
        public CategoryRepository(KnowledgeHubPortalDbContext db)//DIP -Dependency Injection
        {
            this.db = db;
        }
        #region Syncronous
        public void Create(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public List<Category> GetAll()
        {
            return db.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return db.Categories.Find(id);
        }

        public bool SoftDelete(int id)
        {
            //get the category to softdelete
            var categoryToSoftDelete = db.Categories.Find(id);  
            if (categoryToSoftDelete != null)
            {
                categoryToSoftDelete.IsDeleated = true;
                db.SaveChanges();
                return true;
            }
            return false;
            
        }

        public void Update(Category category)
        {
            db.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
        #endregion

        #region AsyncSyncronous
        public async Task CreateAsync(Category category)
        {
            db.Categories.Add(category);
            await db.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await db.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await db.Categories.FindAsync(id);
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            //get the category to softdelete
            var categoryToSoftDelete = await db.Categories.FindAsync(id);
            if (categoryToSoftDelete != null)
            {
                categoryToSoftDelete.IsDeleated = true;
                await db.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task UpdateAsync(Category category)
        {
            db.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
        }
        #endregion

    }
}
