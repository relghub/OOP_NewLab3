using System.Xml;
namespace ind
{
    class Cars
    {
        public static List<Shop> CarsList = new();
        public static void Add(Shop s)
        {
            CarsList.Add(s);
        }
        public static void Show()
        {
            foreach (Shop i in CarsList)
            {
                i.PrintToConsole();
            }
        }
    }
    class Shop
    {
        public string Id { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public int Price { get; private set; }
        public Shop(string id, string model, int year, int price)
        {
            Id = id;
            Model = model;
            Year = year;
            Price = price;
        }
        public void PrintToConsole()
        {
            Console.WriteLine("Автосалон: {0}", Id);
            Console.WriteLine("Марка: {0}", Model);
            Console.WriteLine("Рік: {0}", Year);
            Console.WriteLine("Ціна: {0}", Price);
        }
    }
    class Example
    {
        static void MainExample(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            XmlDocument xml = new();
            xml.Load(@"C:\Users\wideprism\Documents\example.xml");
            foreach (XmlNode node in xml.DocumentElement)
            {
                string id = node.Attributes[0].InnerText;
                string model = node["model"].InnerText;
                int year = int.Parse(node["year"].InnerText);
                int price = int.Parse(node["price"].InnerText);
                Cars.Add(new Shop(id, model, year, price));
            }
            Cars.Show();
        }
    }
}