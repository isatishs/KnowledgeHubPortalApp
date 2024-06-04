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
    public class ArticleRepository : IArticleRepository
    {
        private readonly KnowledgeHubPortalDbContext db = null;//new KnowledgeHubPortalDbContext();
        public ArticleRepository(KnowledgeHubPortalDbContext db)
        {
            this.db = db;
        }

        #region Syncronous
        public void ApproveArticle(int[] articleIds)
        {
            ////Option 1.
            //var articles = db.Articles;
            //foreach (int id in articleIds)
            //{
            //    foreach (Article article in articles)
            //    {
            //        if(article.ArticleID == id)
            //        {
            //            article.IsApproved = true;
            //        }
            //    }
            //}

            ////Option 2.
            foreach (int id in articleIds)
            {
                var articleToApprove = db.Articles.Find(id);
                if (articleToApprove != null)
                {
                    articleToApprove.IsApproved = true;
                }
            }

            db.SaveChanges();
        }

        public List<Article> GetAllArticlesForApprove(int categoryId = 0)
        {
            ////Option 1.
            if (categoryId == 0)
            {
                return db.Articles.Include(a=>a.Category).Where(a => a.IsApproved == false).ToList();
            }
            else
            {
                var articles = db.Articles.Include(a => a.Category).Where(a => a.IsApproved == false && a.Category.CategoryId == categoryId);

                return articles.ToList();
            }

            ////Option 2.
            //var result = from a in db.Articles.Include(a=>a.Category)
            //             where a.IsApproved == false
            //             select a;
            //if (categoryId != 0)
            //{
            //    var result1 = from a in db.Articles.Include(a => a.Category)
                            
            //                 where a.IsApproved == false && a.CategoryId == categoryId
            //                 select a;
            //}
            //return result1.ToList();

        }

        public List<Article> GetAllArticlesForBrowse(int categoryId = 0)
        {
            ////Option 1.
            if (categoryId == 0)
            {
                return db.Articles.Include(a => a.Category).Where(a => a.IsApproved == true).ToList();
            }
            else
            {
                var articles = db.Articles.Include(a => a.Category).Where(a => a.IsApproved == true && a.CategoryId == categoryId);

                return articles.ToList();
            }
            ////Option 2.
            ///
            //var result = from a in db.Articles.Include(a => a.Category)
            //             where a.IsApproved == true
            //             select a;
            //if (categoryId != 0)
            //{
            //    result = from a in db.Articles.Include(a => a.Category) where a.IsApproved == true && a.CategoryId == categoryId select a;
            //}
            //return result.ToList();
        }

        public void RejectArticle(int[] articleIds)
        {
            ////Option 1.
            //foreach(int id in articleIds)
            //{
            //    var articleToDelete = (from a in db.Articles
            //                           where a.ArticleID == id
            //                           select a).FirstOrDefault();
            //    db.Remove(articleToDelete);
            //}

            ////Option 2.
            foreach (int id in articleIds)
            {
                var articleToDel = db.Articles.Find(id);
                if (articleToDel != null)
                {
                    db.Articles.Remove(articleToDel);
                }
            }
            db.SaveChanges();
        }

        public void SubmitArticle(Article atricle)
        {
            var category = ((from c in db.Categories
                                  where c.CategoryId == atricle.CategoryId
                                  select c).FirstOrDefault());
      
            atricle.Category = category;
            db.Articles.Add(atricle);
            db.SaveChanges();
        }

        #endregion

        #region Asyncronous
        public async Task ApproveArticleAsync(int[] articleIds)
        {

            foreach (int id in articleIds)
            {
                var articleToApprove = await db.Articles.FindAsync(id);
                if (articleToApprove != null)
                {
                    articleToApprove.IsApproved = false;
                }
            }

            await db.SaveChangesAsync();
        }

        public async Task<List<Article>> GetAllArticlesForApproveAsync(int categoryId = 0)
        {

            var result = from a in db.Articles
                         where a.IsApproved == false
                         select a;
            if (categoryId != 0)
            {
                result = from a in db.Articles where a.CategoryId == categoryId select a;
            }
            return await result.ToListAsync();

        }

        public async Task<List<Article>> GetAllArticlesForBrowseAsync(int categoryId = 0)
        {

            var result = from a in db.Articles
                         where a.IsApproved == true
                         select a;
            if (categoryId != 0)
            {
                result = from a in db.Articles where a.CategoryId == categoryId select a;
            }
            return await result.ToListAsync();
        }

        public async Task RejectArticleAsync(int[] articleIds)
        {

            foreach (int id in articleIds)
            {
                var articleToDel = await db.Articles.FindAsync(id);
                if (articleToDel != null)
                {
                    db.Articles.Remove(articleToDel);
                }
            }
            await db.SaveChangesAsync();
        }

        public async Task SubmitArticleAsync(Article atricle)
        {
            db.Articles.Add(atricle);
            await db.SaveChangesAsync();
        }

        #endregion




    }
}
