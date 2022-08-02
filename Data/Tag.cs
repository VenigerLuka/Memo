#nullable disable

namespace MemoProject.Data
{
    public partial class Tag
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long MemoId { get; set; }

        public virtual Memo Memo { get; set; }
    }
}
