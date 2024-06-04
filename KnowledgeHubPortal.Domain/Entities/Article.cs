
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeHubPortal.Domain.Entities
{
    public class Article
    {
        public int ArticleID { get; set; }
        [Required, MaxLength(200)]
        public string Title { get; set; }
        [Required, MaxLength(500)]
        public string Description { get; set; }
        [Required, MaxLength(100)]
        public string URL { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public bool IsApproved { get; set; }
        public string SubmitedBy { get; set; }
        public DateTime DateSubmited { get; set; }
        public DateTime DateApproved { get; set; }

    }
}
