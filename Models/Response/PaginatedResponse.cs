using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoProject.Models.Response
{
    public class PaginatedResponse
    {
        public string SearchValue { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
    }
}
