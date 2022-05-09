using Microsoft.Extensions.Configuration;
using System;

namespace BMS.Bll.Helper
{
    public class Mailing
    {
        //private static string mailAccount = "bsshizmettakip@gmail.com";
        //private static string mailPassword = "Bss.2019";
        //private static string mailTitle = "BSS Yazılım Hizmet Takip Sistemi";
        //private static string pop3Host = "smtp.gmail.com";
        //private static int pop3Port = 587;
        private string mailAccount = "";
        private string mailPassword = "";
        private string mailTitle = "";
        private string pop3Host = "";
        private int pop3Port;

        public Mailing(IConfiguration configuration)
        {
            mailAccount = configuration["MailInfo:Account"];
            mailPassword = configuration["MailInfo:Password"];
            mailTitle = configuration["MailInfo:Title"];
            pop3Host = configuration["MailInfo:Pop3Host"];
            pop3Port = int.Parse(configuration["MailInfo:Pop3Port"]);
        }

       
        
        public bool Send(string mailAddresses, string mailContent)
        {
            try
            {
                System.Net.NetworkCredential cred = new System.Net.NetworkCredential(mailAccount, mailPassword);

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.From = new System.Net.Mail.MailAddress(mailAccount, mailTitle);
                mail.To.Add(mailAddresses);
                mail.Subject = "İş Yönetim Sistemi üzerinden bir mesaj aldınız.";
                mail.IsBodyHtml = true;
                mail.Body = mailContent;
                mail.Attachments.Clear();


                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(pop3Host, pop3Port);
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = cred;
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}