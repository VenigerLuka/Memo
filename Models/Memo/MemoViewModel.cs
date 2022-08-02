using System;
using System.ComponentModel.DataAnnotations;

namespace MemoProject.Models.Memo
{
    public class MemoViewModel
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public string Note { get; set; }

        public int StatusId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }

        public string Tags { get; set; }
    }
}
