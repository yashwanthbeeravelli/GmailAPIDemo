using Abp.Domain.Entities;
using GmailAPIDemo.GmailAPIRepositories;
using GmailAPIDemo.GmailEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.Domain.Repositories;
using System.Data.Entity;
using Google.Apis.Auth.OAuth2;
using System.IO;
using Google.Apis.Gmail.v1;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Gmail.v1.Data;
using System.Net.Mail;
using MimeKit;
using Abp.Application.Services;

namespace GmailAPIDemo.EntityFramework.Repositories
{
    public class MailRepository<TEntity> : GmailAPIDemoRepositoryBase<Mail>, IMailRepository<TEntity> where TEntity : class
    {
        private GmailService gmailService = new GmailService();

        public MailRepository(IDbContextProvider<GmailAPIDemoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<Mail> GetAllMails()
        {
            IQueryable<Mail> allMails = Context.Mail.AsQueryable();
            allMails = allMails.Where(x => x.Id >= 1);
            return allMails.ToList();
        }

        public async Task<GmailService> GetGmailService()
        {
            return await SetGmailService();
        }

        public async Task<GmailService> SetGmailService()
        {
            try
            {
                UserCredential credential;
                using (var stream = new FileStream("C:\\Users\\Nair.ACER\\Documents\\Visual Studio 2015\\Projects\\GmailClientDemo\\GmailClientDemo.Web\\JSON\\client_secret_1043375833636-npt5ih8uo00sh5p19t2s5eettmpdm4ph.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
                {
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        // This OAuth 2.0 access scope allows for read-only access to the authenticated 
                        // user's account, but not other types of account access.
                        new[] { GmailService.Scope.GmailReadonly,
                            GmailService.Scope.MailGoogleCom,
                            GmailService.Scope.GmailModify,
                            GmailService.Scope.GmailSend
                        },
                        "yash bee",
                        CancellationToken.None,
                        new FileDataStore(this.GetType().ToString())
                    );
                }

                GmailService gService = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = this.GetType().ToString()
                });
                return gService;
            }
            catch (Exception ex)
            {
                return gmailService;
            }
        }

        public async Task<ListMessagesResponse> GetMailsFromGmail(string label, int count, string userId)
        {
            try
            {
                GmailService gmailService = await GetGmailService();
                //label="INBOX"; userId = yash.bee@gmail.com
                var emailListRequest = gmailService.Users.Messages.List(userId);
                emailListRequest.LabelIds = label;
                emailListRequest.IncludeSpamTrash = false;
                //emailListRequest.Q = "is:unread"; //this was added because I only wanted undread email's...
                //get our emails
                var emailListResponse = await emailListRequest.ExecuteAsync();
                return emailListResponse;
            }
            catch(Exception ex)
            {
                ListMessagesResponse list = new ListMessagesResponse();
                return list;
            }
            
        }

        public async Task InsertMails(ListMessagesResponse emailListResponse, List<Mail> MailsInDB)
        {
            try
            {
                GmailService gmailService = await GetGmailService();
                foreach (var email in emailListResponse.Messages)
                {
                    var emailInfoRequest = gmailService.Users.Messages.Get("yash.bee@gmail.com", email.Id);
                    //make another request for that email id...
                    var emailInfoResponse = await emailInfoRequest.ExecuteAsync();
                    if (emailInfoResponse != null)
                    {
                        if(MailExistsinDB(emailInfoResponse, MailsInDB)==false)
                        {
                            await InsertMailIntoDB(emailInfoResponse);
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        public string GetSenderInfo(Message emailInfoResponse)
        {
            string SenderInfo = "";
            try
            {
                foreach (var MailPayloadHeader in emailInfoResponse.Payload.Headers)
                {
                    if (MailPayloadHeader.Name == "From")
                    {
                        SenderInfo = MailPayloadHeader.Value.ToString();
                        break;
                    }
                }
                if (string.IsNullOrEmpty(SenderInfo))
                    return "Sender Info Not Found";
                else
                    return SenderInfo;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string GetSubject(Message emailInfoResponse)
        {
            string Subject = "";
            try
            {
                foreach (var MailPayloadHeader in emailInfoResponse.Payload.Headers)
                {
                    if (MailPayloadHeader.Name == "Subject")
                    {
                        Subject = MailPayloadHeader.Value.ToString();
                        break;
                    }
                }
                if (string.IsNullOrEmpty(Subject))
                    return "Subject Not Found";
                else
                    return Subject;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string GetSenderEmailAddress(Message emailInfoResponse)
        {
            string SenderEmailID = "";
            try
            {
                foreach (var MailPayloadHeader in emailInfoResponse.Payload.Headers)
                {
                    if (MailPayloadHeader.Name == "From")
                    {
                        SenderEmailID = MailPayloadHeader.Value.ToString();
                        break;
                    }
                }
                if (string.IsNullOrEmpty(SenderEmailID))
                    return "Sender Email Address Not Found";
                else
                    return SenderEmailID;

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string GetMailData(Message emailInfoResponse)
        {
            string EmailData = "";
            try
            {
                foreach (var MailPayLoadHeader in emailInfoResponse.Raw)
                {
                    EmailData = MailPayLoadHeader.ToString();
                }
                if (string.IsNullOrEmpty(EmailData))
                    return "Email Data Not Retrieved";
                else
                    return EmailData;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }


        }

        public string GetMailDate(Message emailInfoResponse)
        {
            string EmailDate = "";
            try
            {
                foreach (var MailPayloadHeader in emailInfoResponse.Payload.Headers)
                {
                    if (MailPayloadHeader.Name == "Date")
                    {
                        EmailDate = MailPayloadHeader.Value.ToString();
                        break;
                    }
                }
                if (string.IsNullOrEmpty(EmailDate))
                    return "Sender Email Address Not Found";
                else
                    return EmailDate;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public async void SendEmailToGmail(string subject, string From, string data)
        {
            GmailService gmailService = await GetGmailService();
            MailMessage mail = new MailMessage();
            mail.Subject = subject;
            mail.Body = data;
            mail.From = new MailAddress(From);
            mail.IsBodyHtml = true;
            mail.To.Add(new MailAddress("yash.bee@gmail.com"));
            //mail.CC.Add(new MailAddress(CC));
            #region comments
            //foreach (string add in vendorEmailList.Split(','))
            //{
            //    if (string.IsNullOrEmpty(add))
            //        continue;

            //    mail.To.Add(new MailAddress(add));
            //}

            //foreach (string add in userEmailList.Split(','))
            //{
            //    if (string.IsNullOrEmpty(add))
            //        continue;

            //    mail.CC.Add(new MailAddress(add));
            //}

            //foreach (string path in attachments)
            //{
            //    //var bytes = File.ReadAllBytes(path);
            //    //string mimeType = MimeMapping.GetMimeMapping(path);
            //    Attachment attachment = new Attachment(path);//bytes, mimeType, Path.GetFileName(path), true);
            //    mail.Attachments.Add(attachment);
            //}
            #endregion
            MimeKit.MimeMessage mimeMessage = MimeMessage.CreateFromMailMessage(mail);
            Message message = new Message();
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(mimeMessage.ToString());
            message.Raw = Convert.ToBase64String(inputBytes);
            var result = gmailService.Users.Messages.Send(message, From).Execute();
        }

        public async Task<Message> GetMessage(String userId, String messageId)
        {
            try
            {
                GmailService gmailService = await GetGmailService();
                return gmailService.Users.Messages.Get(userId, messageId).Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }

            return null;
        }

        public async void DeleteMessage(String userId, String messageId)
        {
            try
            {
                GmailService gmailService = await GetGmailService();
                gmailService.Users.Messages.Delete(userId, messageId).Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }

        public bool MailExistsinDB(Message msg, List<Mail> MailsInDB)
        {
            var Exists = MailsInDB.Find(x => x.Etag == msg.ETag || x.ThreadId == msg.ThreadId);
            if (Exists!=null)
            {
                return true;
            }
            else return false;
        }

        public async Task InsertMailIntoDB(Message msg)
        {
            try
            {
                GmailService gmailService = await GetGmailService();
                var emailInfoRequest = gmailService.Users.Messages.Get("yash.bee@gmail.com", msg.Id);
                var emailInfoResponse = await emailInfoRequest.ExecuteAsync();
                Mail newMail = new Mail()
                {
                    MailID = emailInfoResponse.Id,
                    HistoryId = emailInfoResponse.HistoryId,
                    Etag = emailInfoResponse.ETag,
                    InternalDate = emailInfoResponse.InternalDate,
                    Raw = emailInfoResponse.Raw,
                    SizeEstimate = emailInfoResponse.SizeEstimate,
                    Snippet = emailInfoResponse.Snippet,
                    ThreadId = emailInfoResponse.ThreadId,
                    SendBy = GetSenderInfo(emailInfoResponse),
                    Subject = GetSubject(emailInfoResponse),
                    SenderEmailAddress = GetSenderEmailAddress(emailInfoResponse),
                    Data = GetMailData(emailInfoResponse),
                    Date = GetMailDate(emailInfoResponse)
                };
                    Insert(newMail);
            }
            catch (Exception ex)
            {

            }
        }

        public async void SynchronizeMails(ListMessagesResponse emailListResponse, List<Mail> MailsInDB)
        {
            try
            {
                GmailService gmailService = await GetGmailService(); 
                foreach (Message msg in emailListResponse.Messages)
                {
                    var emailInfoRequest = gmailService.Users.Messages.Get("yash.bee@gmail.com", msg.Id);
                    //make another request for that email id...
                    var emailInfoResponse = await emailInfoRequest.ExecuteAsync();
                    if(emailInfoResponse!=null)
                    {
                        if (MailExistsinDB(emailInfoResponse, MailsInDB) == false)
                        {
                            await InsertMailIntoDB(emailInfoResponse);
                        }
                    }           
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
