using System;
using System.Collections.Generic;

#nullable disable

namespace MemoProject.Data
{
    public partial class Memo
    {
        public Memo()
        {
            Tag = new HashSet<Tag>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public int StatusId { get; set; }
        public string UserId { get; set; }


        public virtual ICollection<Tag> Tag { get; set; }
    }
}
