using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GmailAPIDemo.GmailAPI.Dto;
using GmailAPIDemo.GmailEntities;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmailAPIDemo.GmailAPI
{
    public interface IMailAppService : IApplicationService
    {
        void Insert(Mail mail);
        void Insert(List<Mail> newMails);
        ListResultDto<MailListDTO> GetMails();
        List<Mail> GetAllList();
        Task<ListMessagesResponse> GetMailsFromGmail();
        void SendEmail(CreateMailInput mail);
        Task InsertMailsIntoDB();
        Task<int> calculateNewMails();
        Task SyncMails();
        string GetMail(string ThreadId);
        //List<MailListDTO> GetMails();
    }
}
