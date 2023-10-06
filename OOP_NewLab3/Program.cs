using System.Xml;

namespace OOP_NewLab3
{
    class Log
    {
        public static List<Debug> LogContents = new();
        public static void Add(Debug debugLog)
        {
            LogContents.Add(debugLog);
        }
        public static void Show()
        {
            foreach (Debug i in LogContents)
            {
                i.PrintToConsole();
            }
        }
    }
    class RealEstate
    {
        public static List<Realty> RealtyList = new();
        public static void Add(Realty realty)
        {
            RealtyList.Add(realty);
        }
        public static void Show()
        {
            foreach (Realty i in RealtyList)
            {
                i.PrintToConsole();
            }
        }
    }
    class Debug
    {
        public string Date { get; private set; }
        public string Result { get; private set; }
        public string IPFrom { get; private set; }
        public string Method { get; private set; }
        public string URLTo { get; private set; }
        public int Response { get; private set; }
        public Debug(string date,
                   string result,
                   string sourceIp,
                   string method,
                   string dest_url,
                   int resp)
        {
            Date = date;
            Result = result;
            IPFrom = sourceIp;
            Method = method;
            URLTo = dest_url;
            Response = resp;
        }
        public void PrintToConsole()
        {
            Console.WriteLine("Дата/час: {0}", Date);
            Console.WriteLine("Результат: {0}", Result);
            Console.WriteLine("Запит від IP: {0}", IPFrom);
            Console.WriteLine("Метод: {0}", Method);
            Console.WriteLine("Посилання: {0}", URLTo);
            Console.WriteLine("Відповідь сервера: {0}", Response);
        }
    }
    class Realty
    {
        public string Country { get; private set; }
        public string Region { get; private set; }
        public string District { get; private set; }
        public string LocalityName { get; private set; }
        public string? SublocalityName { get; private set; }
        public string? NonAdminSublocality { get; private set; }
        public string? Address { get; private set; }
        public string Direction { get; private set; }
        public int? Distance { get; private set; }
        public Realty(string country,
                      string region,
                      string district,
                      string localityName,
                      string sublocalityName,
                      string nonAdminSublocal,
                      string address,
                      string direction,
                      int distance)
        {
            Country = country;
            Region = region;
            District = district;
            LocalityName = localityName;
            SublocalityName = sublocalityName;
            NonAdminSublocality = nonAdminSublocal;
            Address = address;
            Direction = direction;
            Distance = distance;
        }
        public void PrintToConsole()
        {
            Console.WriteLine("Країна: {0}", Country);
            Console.WriteLine("Регіон: {0}", Region);
            Console.WriteLine("Міжгромадський район: {0}", District);
            Console.WriteLine("Місцевість: {0}", LocalityName);
            Console.WriteLine("Район місцевості: {0}", SublocalityName);
            Console.WriteLine("Народна назва району: {0}",
                                      NonAdminSublocality);
            Console.WriteLine("Адреса: {0}", Address);
            Console.WriteLine("Напрямок: {0}", Direction);
            Console.WriteLine("Відстань: {0}", Distance);

        }
    }
    static class XMLProcess
    {
        public static void CreateXML(string location)
        {
            XmlDocument xml = new();
            try 
            {
                xml.Load(location);
                string root = xml.DocumentElement.Name;
                switch (root)
                {
                    case "log":
                        foreach (XmlNode node in xml.DocumentElement)
                        {
                            string date = node.Attributes[0].InnerText;
                            string result = node.Attributes[1].InnerText;
                            string sourceIp = node["ip-from"].InnerText;
                            string method = node["method"].InnerText;
                            string dest_url = node["url-to"].InnerText;
                            int resp = 
                                Convert.ToInt32(node["response"].InnerText);
                            Log.Add(new Debug(date,
                                              result,
                                              sourceIp,
                                              method,
                                              dest_url,
                                              resp));

                        }
                        Log.Show();
                        break;
                    case "realty":
                        foreach (XmlNode node in xml.DocumentElement)
                        {
                            string country = node["country"].InnerText;
                            string region = node["region"].InnerText;
                            string district = node["district"].InnerText;
                            string localityName = 
                                node["locality-name"].InnerText;
                            string sublocalityName =
                                node["sub-locality-name"] is not null ?
                                node["sub-locality-name"].InnerText :
                                "Не визначено";
                            string nonAdminSublocal =
                                node["non-admin-sub-locality"] is not null ?
                                node["non-admin-sub-locality"].InnerText :
                                "Не визначено";
                            string address = node["address"] is not null ?
                                node["address"].InnerText : "Не визначено";
                            string direction = node["direction"].InnerText;
                            int distance =
                                Convert.ToInt32(node["distance"] is not null ?
                                                node["distance"].InnerText :
                                                "0");
                            RealEstate.Add(new Realty(country,
                                                      region,
                                                      district,
                                                      localityName,
                                                      sublocalityName,
                                                      nonAdminSublocal,
                                                      address,
                                                      direction,
                                                      distance));

                        }
                        RealEstate.Show();
                        break;
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Рядок шляху не може бути порожнім.\n" +
                                  "Повторіть спробу.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("За цим шляхом підтримуваний файл " +
                                  "не знайдено. Повторіть спробу.");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine("Введіть шлях до файлу XML.");
            Console.WriteLine("Або перетягніть файл у консоль.");
            string path = Console.ReadLine() ?? "";
            XMLProcess.CreateXML(path);
        }
    }
}
