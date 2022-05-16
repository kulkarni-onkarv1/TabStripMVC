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
            var resultToken = random.Next(1,1000000).ToString("D6");

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

        public static void OnTransactionStatusChange(object source, EventArgs eventArgs, String MailID)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("kulkarni.onkar40@gmail.com");
            mail.To.Add(MailID);
            mail.Subject = "Change in status of payment request";
            mail.Body = "Dear User,Your Request Status Has Been Changed!Please Log In To See The Change!";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("kulkarni.onkar40@gmail.com", "Mechanics@123");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

    }
}
