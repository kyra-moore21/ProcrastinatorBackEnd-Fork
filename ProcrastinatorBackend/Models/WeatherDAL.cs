using Newtonsoft.Json;
using System.Net;

namespace ProcrastinatorBackend.Models
{
    public class WeatherDAL
    {
        public static WeatherModel GetWeather(string city) //Adjust
        {
            //adjust
            //setup
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={Secret.apikey}&units=imperial";
            //hide API key from github at end 
            //request leave alone
            HttpWebRequest request = WebRequest.CreateHttp(url);

            //get response leave alone
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //JSON
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string JSON = reader.ReadToEnd();

            //adjust 
            //Convert to C#
            //Install Newtonsoft.Json nugget package
            WeatherModel result = JsonConvert.DeserializeObject<WeatherModel>(JSON);
            return result;
        }
    }
}
