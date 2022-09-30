using System;
using System.ComponentModel.DataAnnotations;

namespace MemoProject.Data
{
    public class Audit
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public int AuditEventId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string Value { get; set; }
        public string UserAgent { get; set; }

    }
    
}
