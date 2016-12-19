using Abp.AutoMapper;
using GmailAPIDemo.GmailEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmailAPIDemo.GmailAPI.Dto
{
    [AutoMap(typeof(Mail))]
    public class CreateMailInput
    {
        [Required]
        public string SendBy { get; set; }

        public string SenderEmailAddress { get; set; }

        [Required]
        public string Subject { get; set; }

        public string Data { get; set; }
    }
}
