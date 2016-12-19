using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmailAPIDemo.GmailEntities
{
    [Table("Mail")]
    public partial class Mail :Entity
    {
        public string Etag { get; set; }
        public string MailID { get; set; }
        public string ThreadId { get; set; }
        [NotMapped]
        public string LabelId { get; set; }
        public string Raw { get; set; }
        public int? SizeEstimate { get; set; }
        public string Snippet { get; set; }
        public ulong? HistoryId { get; set; }
        public long? InternalDate { get; set; }
        public string Date { get; set; }
        public bool IsRead { get; set; }
        public string SendBy { get; set; }
        public string Subject { get; set; }
        public string SenderEmailAddress { get; set; }
        public string Data { get; set; }
    }
}
