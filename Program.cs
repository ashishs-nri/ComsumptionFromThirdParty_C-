using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.IO;

namespace RetrieveData
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = null;
            try
            {
                Console.WriteLine("Enter the Page No between 1-45");
                int pageNo = int.Parse(Console.ReadLine());
                url = "http://api.themoviedb.org/3/movie/now_playing?api_key=e649c1ec4f43c9f8ea307ec5aec0e891&page=" + pageNo;
            }
            catch
            {
                Console.WriteLine("Wrong Input");
            }
            //var json = new WebClient().DownloadString(url);
            new Task(()=> { ApiCall(url); }).Start();
           // T.Start();

           // var serializer = new JavaScriptSerializer();
            //var data = serializer.Deserialize<JSONModel>(json);
            Console.ReadLine();
        }

        static async void ApiCall(string url)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);//+"&page="+page);
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch
                {
                    Console.WriteLine("Some Error Has occured");
                    return;
                }
                using (HttpContent content = response.Content)
                {

                    string responseBody = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(responseBody);
                    var jsonObj = JsonConvert.DeserializeObject<ResultModel>(responseBody);
                    foreach (var obj in jsonObj.results)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}", obj.id, obj.original_title, obj.title,obj.vote_count);
                    }

                }
                   Console.ReadLine();
            }
        }
    }
        
}
