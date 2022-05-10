using System;
using System.Linq;
using System.Net.Mail;
using TabStripDemo.Models;

namespace TabStripDemo.Repositories
{
    public class MailService
    {       
        public static string ValidateMailID(RegisterUser user)
        {
            var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var resultToken = new string(
               Enumerable.Repeat(allChar, 8)
               .Select(token => token[random.Next(token.Length)]).ToArray());

            string authToken = resultToken.ToString();
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("kulkarni.onkar40@gmail.com");
                mail.To.Add(user.MailID);
                mail.Subject = "Do Not Reply";
                mail.Body = resultToken;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("kulkarni.onkar40@gmail.com", "Mechanics@123");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return resultToken;
                //MessageBox.Show("mail Send");               
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
    }
}
