using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GmailAPIDemo.GmailEntities;
using GmailAPIDemo.GmailAPIRepositories;
using Abp.Domain.Repositories;
using AutoMapper;
using GmailAPIDemo.GmailAPI.Dto;
using System.Collections;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Google.Apis.Gmail.v1.Data;

namespace GmailAPIDemo.GmailAPI
{
    public class MailAppService : ApplicationService, IMailAppService
    {
        //private readonly IRepository<Mail, long> _userRepository;
        private readonly IRepository<Mail> _mailRepository;
        private readonly IMailRepository<Mail> _iMailRepository;

        public MailAppService()
        {
        }

        public MailAppService(IRepository<Mail> mailRepository, IMailRepository<Mail> iMailRepository)
        {
            _mailRepository = mailRepository;
            _iMailRepository = iMailRepository;
        }

        public List<Mail> GetAllList()
        {
            return _iMailRepository.GetAllMails();
        }

        public void Insert(List<Mail> newMails)
        {
           
        }

        public void Insert(Mail newMail)
        {
            try
            {
                var oldMails = GetAllList();
                if (oldMails != null)
                {
                    var findIfExists = oldMails.Find(x => x.Etag == newMail.Etag);
                    if (findIfExists == null)
                    {
                        _mailRepository.Insert(newMail);
                    }
                }
            }
            catch(Exception ex)
            {

            }           
        }

        public ListResultDto<MailListDTO> GetMails()
        {
            var mails = GetAllList();
            return new ListResultDto<MailListDTO>(
                mails.MapTo<List<MailListDTO>>()
                );   
        }

        public void CreateUser(CreateMailInput input)
        {
            var mail = input.MapTo<Mail>();
            //user.TenantId = AbpSession.TenantId;
            //user.Password = new PasswordHasher().HashPassword(input.Password);
            //user.IsEmailConfirmed = true;

            //CheckErrors(await UserManager.CreateAsync(user));
        }

        public async Task<ListMessagesResponse> GetMailsFromGmail()
        {
            return await _iMailRepository.GetMailsFromGmail("INBOX", 100, "yash.bee@gmail.com");
        }

        public void SendEmail(CreateMailInput mail)
        {
            _iMailRepository.SendEmailToGmail(mail.Subject, mail.SendBy, mail.Data);
        }

        public async Task InsertMailsIntoDB()
        {
            await _iMailRepository.InsertMails(await GetMailsFromGmail(), GetAllList());
        }

        public async Task SyncMails()
        {
            try
            {
                ListMessagesResponse emailListResponse = await GetMailsFromGmail();
                _iMailRepository.SynchronizeMails(emailListResponse, GetAllList());
            }
            catch(Exception ex)
            {

            }
        }

        public async Task<int> calculateNewMails()
        {
            int x = 0;
            ListMessagesResponse MailsInGmail = await GetMailsFromGmail();
            ListResultDto<MailListDTO> MailsInDB = GetMails();
            x = (MailsInGmail.Messages.Count()) - (MailsInDB.Items.Count());
            return x;
        }

        public async void DeleteMail(MailListDTO mail)
        {
            _mailRepository.Delete(x => x.Id == mail.Id);
            _iMailRepository.SynchronizeMails(await GetMailsFromGmail(), GetAllList());
        }

        public string GetMail(string ThreadId)
        {
            return _mailRepository.FirstOrDefault(x => x.ThreadId == ThreadId).Data;
        }
    }
}
