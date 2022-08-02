using System;
using System.ComponentModel.DataAnnotations;

namespace MemoProject.Models.Memo
{
    public class CreateMemoViewModel
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public string Note { get; set; }

        public string Tags { get; set; }
    }
}
