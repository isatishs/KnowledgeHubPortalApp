using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeHubPortal.Domain.DTOs
{
    public class BaseDTO<T>
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public List<T> Data { get; set; }=new List<T>();    

    }
}
