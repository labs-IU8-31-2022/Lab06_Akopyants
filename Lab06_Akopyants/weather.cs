using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
namespace Lab06_Akopyants
{
    class weather
    {
        public weather() // определить широту и долготу
        {
            latitude = generate_latitude();
            longitude = generate_longitude();
        }

        public string generate_latitude()
        {
            string latitude = "";
            Random random = new Random();
            int first = -90;
            int last = 90;
            latitude = Convert.ToString(random.Next(first, last));
            return latitude;
        }

        public string generate_longitude()
        {
            string longitude = "";
            Random random = new Random();
            int first = -180;
            int last = 180;
            longitude = Convert.ToString(random.Next(first, last));
            return longitude;
        }
        public async Task generate_req_res() // запрос и ответ от сервера 
        {
            Console.Write($"Generate request");
            print_dots();
            HttpClient http_client = new HttpClient();
            string request = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={API_key}"; // запрос
            using HttpResponseMessage serv_response = await http_client.GetAsync(request);

            Console.Write("Waiting for response");
            print_dots();
            string response = await serv_response.Content.ReadAsStringAsync(); //  ответ
            var deserializeObject = JsonConvert.DeserializeObject<dynamic>(response)!;
            Country = deserializeObject.sys.country;
            Name = deserializeObject.name;
            Temp = deserializeObject.main.temp - 273;
            Description = deserializeObject.weather[0].description;
        }


        public void printInfo()
        {
            Thread.Sleep(100);
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"Country: {Country}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Temp: {Temp}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            Thread.Sleep(100);
        }


        private void print_dots()
        {
            for(int i = 0; i < 15; ++i)
            {
               // Thread.Sleep(20);
                Console.Write($".");
                
            }
            Console.WriteLine();
        }


        public int CompareTo(weather other)
        {
            if (Temp.CompareTo(other.Temp) != 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        private const string API_key = "b92af92b0160a995c820f7d6e5155d4a";
        private string latitude;
        private string longitude;
        private IComparable<weather> comparable;

        public string? Country;
        public string? Name;
        public string? Temp;
        public string? Description;
    }
}
