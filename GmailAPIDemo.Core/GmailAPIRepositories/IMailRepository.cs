using Abp.Application.Services;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using GmailAPIDemo.GmailEntities;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmailAPIDemo.GmailAPIRepositories
{
    public interface IMailRepository<TEntity> : IRepository, IApplicationService where TEntity : class
    {
        List<Mail> GetAllMails();
        Task<GmailService> GetGmailService();
        Task<GmailService> SetGmailService();
        Task<ListMessagesResponse> GetMailsFromGmail(string label, int count, string userId);
        Task InsertMails(ListMessagesResponse emailListResponse, List<Mail> MailsInDB);
        void SendEmailToGmail(string subject, string To, string Data);
        Task<Message> GetMessage(string userId, string messageId);
        void DeleteMessage(string userId, string messageId);
        Task InsertMailIntoDB(Message msg);
        bool MailExistsinDB(Message msg, List<Mail> MailsInDB);
        void SynchronizeMails(ListMessagesResponse emailListResponse, List<Mail> MailsInDB);
    }
}
