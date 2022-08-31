using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoProject.Models.Setting
{
    public class SettingViewModel
    {
        public long Id { get; set; }
        public string Zone  { get; set; }
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
        public string Culture { get; set; }
        
    }    
}
