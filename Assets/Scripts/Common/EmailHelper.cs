using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security;

namespace Assets.Scripts.Common
{
    public static class EmailSender
    {
        public static void Send(string subject, string body)
        {
            const string server = "smtp.gmail.com";
            const int port = 587;
            const string emailFrom = "brgamestest@gmail.com";
            const string emailTo = "blackrainbowgames@gmail.com";
            //const string password = "r9ifo(sVjAKFrGI";

            var password = new SecureString();

            foreach (var c in new[] { 'r', '9', 'i', 'f', 'o', '(', 's', 'V', 'j', 'A', 'K', 'F', 'r', 'G', 'I' })
            {
                password.AppendChar(c);
            }

            var mail = new MailMessage
            {
                From = new MailAddress(emailFrom),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            mail.To.Add(emailTo);

            var smtpServer = new SmtpClient(server)
            {
                Port = port,
                // ReSharper disable RedundantCast
                Credentials = new NetworkCredential(emailFrom, SecureStringToString(password)) as ICredentialsByHost,
                // ReSharper restore RedundantCast
                EnableSsl = true
            };

            ServicePointManager.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;

            smtpServer.Send(mail);
        }

        private static string SecureStringToString(SecureString value)
        {
            var bstr = Marshal.SecureStringToBSTR(value);

            try
            {
                return Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                Marshal.FreeBSTR(bstr);
            }
        }
    }
}