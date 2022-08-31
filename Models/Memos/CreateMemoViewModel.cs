using System;
using System.ComponentModel.DataAnnotations;

namespace MemoProject.Models.Memos
{
    public class CreateMemoViewModel
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public string Note { get; set; }
        [Required]
        public string Tags { get; set; }
    }
}
