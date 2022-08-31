using MemoProject.Models.Memos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoProject.Models.DataTable
{
    public class DataTableModel
    {
        public List<MemoViewModel> MemoList { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
    }
}
