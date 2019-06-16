using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ScrappingLeague
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var url = "https://euw1.api.riotgames.com/lol/platform/v3/champion-rotations";
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url);  // för att få fram html
              */
            GetHtml();
             //test();
            // Nu handlar det bara om att kunna få ut den specifika delen av html. 
            Console.Write("Enter a champions name from League of legends: ");
            var input = Console.ReadLine();

            var champName = "";
            if (input.ToLower() == input || input.ToUpper() == input)
                    champName = char.ToUpper(input[0]) + input.ToLower().Substring(1);
            System.Diagnostics.Process.Start("https://ddragon.leagueoflegends.com/cdn/9.11.1/img/champion/" + champName + ".png");

            Console.WriteLine(champName);
        }

        public static void GetHtml()
        {
            var urlPatch = "https://ddragon.leagueoflegends.com/realms/na.json";
            var httpClient = new HttpClient();
            var patchSource = httpClient.GetStringAsync(urlPatch).Result;
            var lastPatch = JsonConvert.DeserializeObject<ChampionsRoot>(patchSource);
            var latestPatch = lastPatch.V;

            var url = "http://ddragon.leagueoflegends.com/cdn/" + latestPatch + "/data/en_US/champion.json";
            var source = httpClient.GetStringAsync(url).Result;
            var root = JsonConvert.DeserializeObject<ChampionsRoot>(source);
            var names = root.Data.Select(kv => kv.Value.Name);
            var images = root.Data.Select(kv => kv.Value.Image.Full);
            var aPorAd = root.Data.Select(kv => kv.Value.Tags);
            Console.WriteLine(JObject.Parse(source));

            var champImages = new List<string>();
            var champNames = new List<string>(); 
            var champTags = new List<string>();

            foreach (var name in names)
            {
                Console.WriteLine(name);
                champNames.Add(name);
            }
            Console.WriteLine();
            foreach (var image in images)
            {
                Console.WriteLine(image);
                champImages.Add(image);
            }

            Console.WriteLine();
            foreach (var str in aPorAd)
            {
                var text = String.Join(", ", str);
                champTags.Add(text);
                Console.WriteLine(text);
            }
            Console.WriteLine();
            Console.WriteLine(champImages[131]);
            Console.WriteLine(champNames[131]);
            Console.WriteLine(champTags[131]);
            Console.WriteLine("Latest patch is: " + latestPatch);

            //foreach (var champImage in champImages)
            //    System.Diagnostics.Process.Start("https://ddragon.leagueoflegends.com/cdn/9.11.1/img/champion/" + champImage);
        }
    }
}
