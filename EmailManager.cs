using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EASendMail;

namespace WindowsService
{
    internal class EmailManager
    {
        public String sendEmail(string sender, string password, string destinatary, string subject, string text)
        {
            String aux = "";
            Boolean sended = false;
            try
            {
                SmtpMail email = new SmtpMail("TryIt");
                email.From = sender;
                email.To = destinatary;
                email.Subject = subject;
                email.TextBody = text;

                SmtpServer server = new SmtpServer("smtp.gmail.com");

                server.User = sender;
                server.Password = password;
                server.Port = 587;
                server.ConnectType = SmtpConnectType.ConnectSSLAuto;

                SmtpClient client = new SmtpClient();
                client.SendMail(server, email);
                sended = true;
            }
            catch (Exception ex)
            {
                aux = ex.Message;
            }
            return sended + aux;
        }
    }
}
