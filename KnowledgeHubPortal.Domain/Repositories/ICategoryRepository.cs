using KnowledgeHubPortal.Domain.Entities;

namespace KnowledgeHubPortal.Domain.Repositories
{
    public interface ICategoryRepository
    {
        #region Syncronous
        void Create(Category category);
        List<Category> GetAll();
        Category GetById(int id);
        void Update(Category category);
        bool SoftDelete(int id);
        #endregion

        #region Asyncronous
        Task CreateAsync(Category category);
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task UpdateAsync(Category category);
        Task<bool> SoftDeleteAsync(int id);
        #endregion
    }
}
