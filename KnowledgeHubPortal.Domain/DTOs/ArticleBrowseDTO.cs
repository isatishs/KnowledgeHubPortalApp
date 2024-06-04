using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeHubPortal.Domain.DTOs
{
    public class ArticleBrowseDTO
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public string PostedBy { get; set; }
        public int CategoryID { get; set; }
    }
}
