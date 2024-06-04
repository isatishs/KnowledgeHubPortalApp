using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeHubPortal.Domain.DTOs
{
    public class ArticleApproveDTO
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public string CategoryName { get; set; }
        public int ArticalID { get; set; }
    }
}
