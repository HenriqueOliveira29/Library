﻿using HandlebarsDotNet;
using Microsoft.Extensions.Configuration;
using Supermarket.Data.Interfaces.Services;
using Supermarket.Data.Models.Helper;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace Library.DAL.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _username = string.Empty;
        private readonly string _password = string.Empty;
        private readonly string _smtpHost = string.Empty;
        private readonly int _port = 0;
        private readonly bool _enableSsl = false;
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _username = configuration["Email:Username"];
            _password = configuration["Email:Password"];
            _smtpHost = configuration["Email:Host"];
            _port = Convert.ToInt32(configuration["Email:Port"]);
            _enableSsl = Convert.ToBoolean(configuration["Email:Ssl"]);

            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_smtpHost) || _port == 0)
            {
                throw new ArgumentException("EmailUsername, EmailPassword, EmailHost, EmailPort têm que estar preenchidos");
            }
        }
        public async Task<MessageHelper> SendEmail(string emailTo, string templateName, object obj, string subject, string emailCC = null, string emailBCC = null, List<EmailFileDTO> files = null)
        {
            MessageHelper response = new();
            MailMessage mailMessage = new();
            SmtpClient smtp = new();

            try
            {
                List<string> emailToList = new();
                List<string> emailCCList = new();
                List<string> emailBCClist = new();

                var handlebars = Handlebars.Create(new HandlebarsConfiguration() { FileSystem = new DiskFileSystem() });
                var path = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\Templates\\{templateName}.hbs";

                handlebars.RegisterHelper("breaklines", (writer, context, parameters) =>
                {
                    var param = parameters.FirstOrDefault();
                    if (param != null && param.ToString() != null)
                    {
                        writer.WriteSafeString(param.ToString()!.Replace("\n", "<br>"));
                    }
                });

                var template = handlebars.CompileView(path);
                var templateHTML = template(obj);

                mailMessage = new()
                {
                    From = new MailAddress(_username),
                    Subject = subject,
                    Body = templateHTML,
                    IsBodyHtml = true
                };
                bool embedFilesInEmail = false;

                if (string.IsNullOrEmpty(_configuration["Email:FilesEmbedInEmail"]) == false) {
                    bool.TryParse(_configuration["Email:FilesEmbedInEmail"], out embedFilesInEmail);
                }
                if (embedFilesInEmail == false && files != null && files.Count > 0) {
                    foreach (var file in files) {
                        Attachment data = new Attachment(file.Path);
                        mailMessage.Attachments.Add(data);
                    }
                }

                emailToList = !string.IsNullOrEmpty(emailTo) ? emailTo.Split(';').ToList() : emailToList;
                emailCCList = !string.IsNullOrEmpty(emailCC) ? emailBCC.Split(';').ToList() : emailCCList;
                emailBCClist = !string.IsNullOrEmpty(emailBCC) ? emailBCC.Split(';').ToList() : emailBCClist;

                foreach (var email in emailToList) { mailMessage.To.Add(new MailAddress(email)); }
                foreach (var email in emailCCList) { mailMessage.CC.Add(new MailAddress(email)); }
                foreach (var email in emailBCClist) { mailMessage.Bcc.Add(new MailAddress(email)); }

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                smtp.Host = _smtpHost;
                smtp.EnableSsl = _enableSsl;
                smtp.UseDefaultCredentials = false;
                smtp.Port = _port;
                smtp.Credentials = new NetworkCredential
                {
                    UserName = _username,
                    Password = _password,
                };
                await smtp.SendMailAsync(mailMessage);

                mailMessage.Dispose();
                mailMessage = null;
                smtp.Dispose();
                smtp = null;

                response.Sucess = true;

            }
            catch (Exception ex) {
                mailMessage.Dispose();
                smtp.Dispose();
                response.Sucess = false;
                response.Message = "Ocorreu um erro ao enviar";
            }

            return response;
        }

    }
}
