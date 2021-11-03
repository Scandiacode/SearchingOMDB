using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SearchingOMDB.Models
{
    public class MovieDAL
    {
       
        public string GetMovieTitleFromJson(string title)
        {
            string key = "68fb8e73";
            string url = @$"http://www.omdbapi.com/?apikey={key}&t={title}";
            HttpWebRequest request = WebRequest.CreateHttp(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();


            StreamReader reader = new StreamReader(response.GetResponseStream());
            string JSON = reader.ReadToEnd();
            return JSON;
        }

        public MovieModel ConvertJSONtoSingleTitleMovieModel(string Title)
        {
            string rawJSON = GetMovieTitleFromJson(Title);
            MovieModel movie = JsonConvert.DeserializeObject<MovieModel>(rawJSON);
            return movie;
        }
    }
}
