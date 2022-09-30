using System;

namespace MemoProject.Models.Audit
{
    public class AuditViewModel
    {
        public AuditViewModel(long id, int auditEventId, string value, string userAgent, string createdBy,DateTime createdAt)
        {
            Id = id;
            AuditEventId = auditEventId;
            Value = value;
            UserAgent = userAgent;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }

        public long Id { get; set; }
        public int AuditEventId { get; set; }
        public string Value { get; set; }
        public string UserAgent { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

    }
}
