using MemoProject.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace MemoProject.Models.Memos
{
    public class MemoViewModel
    {
        public MemoViewModel(Memo memo, string dateFormat, string timeFormat)
        {
            Id = memo.Id;
            CreatedAt = $"{memo.CreatedAt.ToString(dateFormat+" "+timeFormat)}";
            Title = memo.Title;
            Note = memo.Note;
            Tags = memo.Tag.ToString();
            UserId = memo.UserId;
            StatusId = memo.StatusId;
        }
        public MemoViewModel()
        {

        }
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public string Note { get; set; }

        public int StatusId { get; set; }

        public string CreatedAt { get; set; }

        public string UserId { get; set; }

        public string Tags { get; set; }
    }
}
