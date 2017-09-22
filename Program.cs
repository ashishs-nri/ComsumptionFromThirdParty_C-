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
            while(true)
            {
                Console.WriteLine("Enter operation code");
                Console.WriteLine("1. Now Playing");
                Console.WriteLine("2. Exit");
                string key = Console.ReadLine();
                if(key.Equals("1"))
                {
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
                    new Task(()=> { ApiCall(url); }).Start();
                    Console.ReadLine();
                }
                else if(key.Equals("2"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Enter proper code");
                }
            }
        }

        static async void ApiCall(string url)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
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
                    var jsonObj = JsonConvert.DeserializeObject<ResultModel>(responseBody);
                    foreach (var obj in jsonObj.results)
                    {
                        Console.WriteLine("Movie Id:{0} \nMovie Name: {1} \n{3} \nUpvotes: {2}\n \n", obj.id, obj.title,obj.vote_count, obj.overview);
                    }
                }
            }
        }
    }
        
}
