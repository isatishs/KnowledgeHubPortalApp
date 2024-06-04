using KnowledgeHubPortal.Domain.DTOs;
using KnowledgeHubPortal.Domain.Entities;
using KnowledgeHubPortal.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeHubPortal.API.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleRepository iRepo;
        public ArticlesController(IArticleRepository iRepo)
        {
            this.iRepo = iRepo;
        }

        [HttpGet]
        [Route("browse/{cid:int}")]
        public IActionResult GetAllArticlesForApprove(int cid) { 
            var allArticles = iRepo.GetAllArticlesForApprove(cid);

            var allArticlesDto = from article in allArticles
                                 select new ArticleBrowseDTO { CategoryID = article.CategoryId, Description= article.Description, Title= article.Title, URL=article.URL, PostedBy=article.SubmitedBy };
            return Ok(allArticlesDto);
        }

        [HttpPost]
        [Route("article")]
        public IActionResult SubmitArticle(ArticleCreateDTO articleCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //automapper


            var article = new Article
            {
                Title = articleCreateDto.Title,
                URL = articleCreateDto.URL,
                CategoryId = articleCreateDto.CategoryID,
                SubmitedBy = User.Identity.Name ?? "me",
                DateSubmited= DateTime.Now,
                Description = articleCreateDto.Description,
               
            };

            iRepo.SubmitArticle(article);
            return Created($"api/article/{article.ArticleID}", article);

        }

        [HttpGet]
        [Route("review/{cid:int}")]
        [ProducesResponseType(200)]
        public IActionResult GetAllArticlesForReview(int cid)
        {
            var allArticles = iRepo.GetAllArticlesForApprove(cid);

            var allArticlesDto = from article in allArticles
                                 select new ArticleBrowseDTO { CategoryID = article.CategoryId, Description = article.Description, Title = article.Title, URL = article.URL, PostedBy = article.SubmitedBy };
            return Ok(allArticlesDto);
        }

        [HttpGet]
       // [Route("browse/{cid:int}")]

        public IActionResult GetAllArticlesForBrowse(int cid)
        {
            var allArticles = iRepo.GetAllArticlesForBrowse(cid);

            var allArticlesDto = from article in allArticles
                                 select new ArticleBrowseDTO { CategoryID = article.CategoryId, Description = article.Description, Title = article.Title, URL = article.URL, PostedBy = article.SubmitedBy,};
            return Ok(allArticlesDto);
        }

        [HttpPut]
        [Route("approve")]
        [ProducesResponseType(200)]
        public IActionResult ArticleToApprove(int[] articleIds) {
            iRepo.ApproveArticle(articleIds);
            return Ok();
        }

        [HttpDelete]
        [Route("reject")]
        [ProducesResponseType(200)]
        public IActionResult ArticleToReject(int[] articleIds)
        {
            iRepo.ApproveArticle(articleIds);
            return Ok();
        }
    }
}
