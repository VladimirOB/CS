using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;

namespace Facade_Antivirus_
{
    partial class MainApp
    {
        static string CheckWebSiteUSD(string path)
        {
            using (WebClient client = new WebClient()) 
            {
                //client.DownloadFile(path, @"V:\test.html");
                string htmlCode = client.DownloadString(path);
                MatchCollection m = Regex.Matches(htmlCode, "USD");
                char[]? USD = new char[10];
                foreach (Match match in m)
                {
                    for (int i = match.Index + 66, k = 0; i < match.Index + 72; i++, k++)
                    {
                        USD[k] = htmlCode[i];
                    }
                    string usd = new string(USD);
                    Console.WriteLine($"Курс доллара: {usd}");
                    return usd;
                }
                //Console.Read();
                return null;
            }
        }

        static void SendMail()
        {
            string USD = CheckWebSiteUSD("https://cbr.ru");
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress(login, "Vladimir");
            // кому отправляем
            MailAddress to = new MailAddress("@gmail.com");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Центробанк!";
            // текст письма
            m.Body = "Курс доллара: " + USD;
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.yandex.ru";
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(login, password);
            smtp.EnableSsl = true;
            // логин и пароль

            try
            {

                smtp.Send(m);
            }
            catch (SmtpException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Птичка полетела!");
        }

        static void Main(string[] args)
        {
            //CheckWebSiteUSD("https://cbr.ru");
            //SendMail();


            Antivirus antivirus = new Antivirus();
            AntivirusFacade facade = AntivirusFacade.Instance();
            facade.ScanAll(antivirus, "V:/temp");
        }
    }




























    partial class MainApp
    {
        public static string login = "@yandex.ru";
        public static string password = "";
    }
}