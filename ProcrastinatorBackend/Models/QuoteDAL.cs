using Newtonsoft.Json;
using System.Net;

namespace ProcrastinatorBackend.Models
{
    public class QuoteDAL
    {
        public static QuoteModel[] GetQuote()
        {
            string url = $"https://zenquotes.io/api/random";

            HttpWebRequest request = WebRequest.CreateHttp(url) ;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse() ; 

            StreamReader reader = new StreamReader(response.GetResponseStream()) ;
            string JSON = reader.ReadToEnd() ;

            QuoteModel[] result = JsonConvert.DeserializeObject<QuoteModel[]>(JSON) ;
            return result;
        }
    }
}
