#nullable disable

namespace MemoProject.Data
{
    public partial class Setting
    {
        public long Id { get; set; }
        public string Zone { get; set; }
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
        public string Culture { get; set; }
        public string UserId { get; set; }


    }
}
