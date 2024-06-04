using KnowledgeHubPortal.Domain.Entities;

namespace KnowledgeHubPortal.Domain.Repositories
{
    public interface IArticleRepository
    {
        #region Syncronous
        void SubmitArticle(Article atricle);
        List<Article>? GetAllArticlesForApprove(int categoryId = 0);
        List<Article>? GetAllArticlesForBrowse(int categoryId = 0);
        void ApproveArticle(int[] articleIds);
        void RejectArticle(int[] articleIds);
        #endregion

        #region Asyncronous
        Task SubmitArticleAsync(Article atricle);
        Task<List<Article>>? GetAllArticlesForApproveAsync(int categoryId = 0);
        Task<List<Article>>? GetAllArticlesForBrowseAsync(int categoryId = 0);
        Task ApproveArticleAsync(int[] articleIds);
        Task RejectArticleAsync(int[] articleIds);
        #endregion


    }

}
