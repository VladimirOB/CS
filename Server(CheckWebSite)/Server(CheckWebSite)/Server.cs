using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Server_CheckWebSite_
{
    delegate int Subscriber(string current);

    class Server
    {
        //переменная-списка адресов функций (event)
        protected event Subscriber subscribers;
        System.Timers.Timer timer;
        HashSet<string> cars;
        int countOfCars;
        public Server()
        {
            cars = new HashSet<string>();
            Client1 c1 = new Client1();
            Client2 c2 = new Client2();
            ClientWeb c3 = new ClientWeb();
            Add(c1.ShowInfo);
            Add(c2.Log);
            Add(c3.SendMail);
            FirstCheck();
        }

        void FirstCheck()
        {
            using (WebClient client = new WebClient())
            {
                string htmlCodeFirst = client.DownloadString("https://www.olx.ua/d/transport/legkovye-avtomobili/donetsk/?currency=USD&search%5Border%5D=created_at:desc");
                MatchCollection m = Regex.Matches(htmlCodeFirst, "count\">Мы нашли ");
                MatchCollection carMatch = Regex.Matches(htmlCodeFirst, "css-1pvd0aj-Text eu5v0x0\">"); // css-1q7gvpp-Text eu5v0x0"> цена
                char[]? temp = new char[10];
                for (int i = m[0].Index + 17, k = 0; htmlCodeFirst[i] != ' '; k++, i++)
                {
                    temp[k] = htmlCodeFirst[i];
                }
                string res = new string(temp);
                Int32.TryParse(res, out countOfCars);
                Console.WriteLine($"Найдено объявлений: {countOfCars}");

                StringBuilder sb = new StringBuilder();
                foreach (Match match in carMatch)
                {
                    if (htmlCodeFirst[match.Index - 103] != 'П') // проверка на "ТОП" категорию
                    {
                        int i;
                        for (i = match.Index + 26; htmlCodeFirst[i] != '<';i++)
                        {
                            sb.Append(htmlCodeFirst[i]);
                        }
                        //if (htmlCodeFirst[i+63] == '>')
                        //{
                        //    sb.Append(" Цена: ");
                        //    for (i +=64; htmlCodeFirst[i] != '$'; i++)
                        //    {
                        //        sb.Append(htmlCodeFirst[i]);
                        //    }
                        //    sb.Append("$");
                        //}
                        string buffer = new string(sb.ToString());
                        if (!cars.Contains(buffer))
                        {
                            cars.Add(buffer);
                        }
                        sb.Clear();
                    }
                }

                StringBuilder sbHtmlName = new StringBuilder("https://www.olx.ua/d/transport/legkovye-avtomobili/donetsk/?currency=USD&search%5Border%5D=created_at%3Adesc&page=2");
                string htmlCodeNext;
                int page = 3;
                for (int j = 0; j < 19; j++)
                {
                    htmlCodeNext = client.DownloadString(sbHtmlName.ToString());
                    MatchCollection carMatchNext = Regex.Matches(htmlCodeNext, "css-1pvd0aj-Text eu5v0x0\">"); // 2 страница и т.д.
                    foreach (Match match in carMatchNext)
                    {
                        if (htmlCodeNext[match.Index - 103] != 'П') // проверка на "ТОП" категорию
                        {
                            int i;
                            for ( i = match.Index + 26; htmlCodeNext[i] != '<'; i++)
                            {
                                sb.Append(htmlCodeNext[i]);
                            }
                            string buffer = new string(sb.ToString());
                            if (!cars.Contains(buffer))
                            {
                                cars.Add(buffer);
                            }
                            sb.Clear();
                        }
                    }
                    if (page < 11)
                        sbHtmlName.Remove(sbHtmlName.Length-1, 1);
                    else
                        sbHtmlName.Remove(sbHtmlName.Length-2, 2);
                    sbHtmlName.Append(page++.ToString());
                    Console.WriteLine(sbHtmlName);
                }
                if (cars.Count < countOfCars)
                {
                    Console.WriteLine($"Cars Count = {cars.Count}");
                    FirstCheck();
                }
                Console.WriteLine($"Cars Count = {cars.Count}");
                SaveAllCars();
            }
        }

        void SaveAllCars()
        {
            StreamWriter sw = new StreamWriter("V:/temp/AllCars.txt");
            int cntForSave = 1;
            foreach (var item in cars)
            {
                sw.WriteLine(cntForSave++ + ") " + item);
            }
            sw.Close();
        }

        public void StartServer()
        {
            timer = new System.Timers.Timer(600000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            StartEvent();
        }

        //метод, подписывающий клиентские классы на сообщ.
        public void Add(Subscriber ev)
        {
            subscribers += ev;
        }
        public void StartEvent()
        {
            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString("https://www.olx.ua/d/transport/legkovye-avtomobili/donetsk/?currency=USD&search%5Border%5D=created_at:desc");
                MatchCollection m = Regex.Matches(htmlCode, "count\">Мы нашли ");
                MatchCollection carMatch = Regex.Matches(htmlCode, "css-1pvd0aj-Text eu5v0x0\">"); // css-1q7gvpp-Text eu5v0x0"> цена
                char[]? temp = new char[10];
                for (int i = m[0].Index + 17, k = 0; htmlCode[i] != ' '; k++, i++)
                {
                    temp[k] = htmlCode[i];
                }
                string res = new string(temp);
                Int32.TryParse(res, out countOfCars);
                Console.WriteLine($"Найдено объявлений: {countOfCars}");
                StringBuilder sb = new StringBuilder();
                foreach (Match match in carMatch)
                {
                    if (htmlCode[match.Index - 103] != 'П')
                    {
                        for (int i = match.Index + 26, k = 0; htmlCode[i] != '<'; k++, i++)
                        {
                            sb.Append(htmlCode[i]);
                        }

                        string buffer = new string(sb.ToString());
                        if (!cars.Contains(buffer))
                        {
                            cars.Add(buffer);
                            subscribers.Invoke(buffer);
                        }
                        sb.Clear();
                    }
                }

            }
        }
    }

    class Client1
    {
        // обработчик события
        public int ShowInfo(string carName)
        {
            Console.WriteLine("Появилось новое авто!");
            Console.WriteLine($"Информация: {carName}");
            return 0;
        }
    }
    class Client2
    {
        public int Log(string carName)
        {
            using (StreamWriter sw = File.AppendText("V:/temp/db.txt"))
            {
                string time = DateTime.Now.ToString();
                sw.WriteLine(time + " " + carName);
            }
            return 0;
        }
    }
    class ClientWeb
    {
        public int SendMail(string carName)
        {
            string login = "@yandex.ru";
            string password = "";
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress(login, "Vladimir");
            // кому отправляем
            MailAddress to = new MailAddress("@gmail.com");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Автомобиль!";
            // текст письма
            m.Body = "Новое авто: " + carName;
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.yandex.ru";
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(login, password);
            smtp.EnableSsl = true;
            smtp.Send(m);
            Console.WriteLine("Птичка полетела!");
            return 0;
        }
    }
}
