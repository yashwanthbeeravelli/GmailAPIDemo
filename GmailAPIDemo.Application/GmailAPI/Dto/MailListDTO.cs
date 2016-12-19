using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GmailAPIDemo.GmailEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmailAPIDemo.GmailAPI.Dto
{
    [AutoMap(typeof(Mail))]
    public class MailListDTO:EntityDto
    {
        public string Snippet { get; set; }
        public bool IsRead { get; set; }
        public string SendBy { get; set; }
        public string Subject { get; set; }
        public string SenderEmailAddress { get; set; }
        public string Data { get; set; }
        public string ThreadId { get; set; }
    }
}
