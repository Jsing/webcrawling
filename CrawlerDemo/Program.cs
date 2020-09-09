using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using HtmlAgilityPack;
using System.Text;
using System.Data.Odbc;
using System.Threading.Tasks;

namespace CrawlerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            startCrawlerasync();
            Console.ReadLine();
            
        }

        private static async Task startCrawlerasync()
        {
           
            //the url of the page we want to test
            var url = "https://www.automobile.tn/fr/neuf/bmw";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            // a list to add all the list of cars and the various prices 
            var cars = new List<Car>();
            var divs =
            htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Equals("versions-item")).ToList();
                       
            foreach(var div in divs)
            {

                var car = new Car
                {

                    Model = div.Descendants("h2").FirstOrDefault().InnerText,
                    Price = div.Descendants("div").FirstOrDefault().ChildNodes.Descendants("span").FirstOrDefault().InnerText
                };
                
                cars.Add(car);              
            }
            
            // 파일에 쓰기.

            Console.WriteLine("Successful....");
            Console.WriteLine("Press Enter to exit the program...");
            ConsoleKeyInfo keyinfor = Console.ReadKey(true);
            if(keyinfor.Key == ConsoleKey.Enter)
            {
                System.Environment.Exit(0);
            }

        }
       
    }
}
