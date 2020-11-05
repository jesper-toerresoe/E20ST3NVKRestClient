using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json; //Extension to System.Net.Http Simplified coding extensively
//using Newtonsoft.Json; //Replaced by teh following using
using System.Text.Json;
using DALRESTFulUtilCore.Logging;

namespace DALRESTFulUtilCore.HttpClientJSON
{

    /**
    * http://stackoverflow.com/questions/2546138/deserializing-json-data-to-c-sharp-using-json-net 
    * https://jsonclassgenerator.codeplex.com/downloads/get/631627
    * http://www.asp.net/web-api/overview/advanced/calling-a-web-api-from-a-net-client
    * https://stackoverflow.com/questions/18924996/logging-request-response-messages-when-using-httpclient
    * https://www.learmoreseekmore.com/2020/06/Httpclient-json-extension-methods.html   An important link about HTTP JSON extension
    **/



    public class APIGetJSON<T>
    {
        public T data;
        private string url;

        public APIGetJSON(string geturl)
        {
            url = geturl;
            data = getItems();
        }

        private  T getItems()
        {
            T result = default(T);
            HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // This allows for debugging possible JSON issues  //Newtonsoft
            //var settings = new JsonSerializerSettings
            //{
            //    Error = (sender, args) =>
            //    {
            //        if (System.Diagnostics.Debugger.IsAttached)
            //        {
            //            System.Diagnostics.Debugger.Break();
            //        }
            //    }
            //};


            using (HttpResponseMessage response = client.GetAsync(this.url).Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    string resp = response.Content.ReadAsStringAsync().Result;
                    //result = JsonConvert.DeserializeObject<T>(resp, settings);//Newtonsoft
                    result = JsonSerializer.Deserialize<T>(resp);
                }
            }
            return result;
        }
    }


    /// <summary>
    /// Uses the .NET deserializer instead of Newtonsoft JSON deserializer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class APIGetJSONAsync<T>
    {
        public T data;
        private string url;
        async Task RunConsumer()
        {
            data = await getItems();
        }

        public APIGetJSONAsync(string geturl)
        {
            url = geturl;
            RunConsumer().Wait();
        }

        private async Task<T> getItems()
        {
            //T result = default(T);
            HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(this.url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();

            //string data = await response.Content.ReadAsStringAsync();
            //result =  JsonSerializer.Deserialize<T>(data);
            //return result;
        }
    }

    /// <summary>
    /// Uses the System.Net.Http.Json extension
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class APIGetJSONSimpleAsync<T>
    {
        public T data;
        private string url;
        async Task RunConsumer()
        {
            data = await getItems();
        }

        public APIGetJSONSimpleAsync(string geturl)
        {
            url = geturl;
            RunConsumer().Wait();
        }

        private async Task<T> getItems()
        {
            //T result = default(T);
            HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            return await client.GetFromJsonAsync<T>(this.url);


        }
    }

    public class APIPostJSON<T>
    {
        public T data;
        private string url;
        private string request;

        public APIPostJSON(string geturl, string req, T item)
        {
            data = item;
            request = req;
            url = geturl;
            data = postItem(item);
        }

        private T postItem(T item)
        {
            T result = item;
            HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            client.BaseAddress = new Uri(url);
            //client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (HttpResponseMessage response = client.PostAsJsonAsync(request, item).Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    result = JsonSerializer.Deserialize<T>(response.Content.ReadAsStringAsync().Result);
                }
            }
            return result;
        }
    }
    /// <summary>
    /// Uses the .NET deserializer instead of Newtonsoft JSON deserializer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class APIPostJSONAsync<T>
    {
        public T data;
        private string url;
        private string request;

        async Task RunConsumer()
        {           
            data = await postItem(data); 
        }

        public APIPostJSONAsync(string geturl, string req, T item)
        {
            data = item;
            request = req;
            url = geturl;
            RunConsumer().Wait();
        }

        private async Task<T> postItem(T item)
        {
            T result = default(T);
            HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            client.BaseAddress = new Uri(url);
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsJsonAsync(request, item);
            response.EnsureSuccessStatusCode();
            result = await response.Content.ReadFromJsonAsync<T>(); //Get response back from server 

            //client.BaseAddress = new Uri(url);
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));      
            //using (HttpResponseMessage response = client.PostAsJsonAsync(request, item).Result)
            //{
            //    if (response.IsSuccessStatusCode)
            //    {
            //        //result = JsonSerializer.Deserialize<T>(response.Content.ReadAsStringAsync().Result);//For debug
            //        result = await response.Content.ReadFromJsonAsync<T>();
            //    }
            //}
            return result;

        }
    }
    public class APIPutJSONAsync<T>
    {
        public T data;
        private string url;
        private string request;

        async Task RunConsumer()
        {
            data = await putItem(data);
        }

        public APIPutJSONAsync(string puturl, string req, T item)
        {
            data = item;
            request = req;
            url = puturl;
            RunConsumer().Wait();
        }

        private async Task<T> putItem(T item)
        {
            HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
           
            client.BaseAddress = new Uri(url);
            var response = await client.PutAsJsonAsync(request, item);
            response.EnsureSuccessStatusCode();
            T result = default(T);
            //T result = await response.Content.ReadFromJsonAsync<T>();//No Content is returned

            //client.BaseAddress = new Uri(url);
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //using (HttpResponseMessage response = client.PutAsJsonAsync(request, item).Result)
            //{
            //    if (response.IsSuccessStatusCode)
            //    {
            //        result = JsonSerializer.Deserialize<T>(response.Content.ReadAsStringAsync().Result);
            //    }
            //}
            return result;
        }
    }

    public class APIDeleteJSONAsync<T>
    {
        public T data;
        private string url;
        private string request;
        
        public APIDeleteJSONAsync(string deleteurl, string req, T item)
        {
            data = item;
            request = req;
            url = deleteurl;
            data = deleteItem(item);
        }

        private T deleteItem(T item)
        {
            T result = default(T);
            result = item;
            HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (HttpResponseMessage response = client.DeleteAsync(request).Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    string resp = response.Content.ReadAsStringAsync().Result;
                    result = JsonSerializer.Deserialize<T>(resp);
                }
            }
            return result;
        }
    }

    public class APIDeleteJSONAsyncSimple<T>
    {
        public T data;
        private string url;
        private string request;

        async Task RunConsumer()
        {
            data = await deleteItem(data);
        }

        public APIDeleteJSONAsyncSimple(string deleteurl, string req, T item)
        {
            data = item;
            request = req;
            url = deleteurl;
            RunConsumer().Wait();
        }

        private async Task<T> deleteItem(T item)
        {
            HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            client.BaseAddress = new Uri(url);
            var response = await client.DeleteAsync(request);
            response.EnsureSuccessStatusCode();
            T result = await response.Content.ReadFromJsonAsync<T>(); //Get response back from server 


           
            //client.BaseAddress = new Uri(url);
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //using (HttpResponseMessage response = client.DeleteAsync(request).Result)
            //{
            //    if (response.IsSuccessStatusCode)
            //    {
            //        /*
            //         * Severity	Code	Description	Project	File	Line	Suppression StateWarning	CS0618	'JsonConvert.DeserializeObjectAsync<T>(string)' is obsolete: 
            //         * 'DeserializeObjectAsync is obsolete. Use the Task.Factory.StartNew method to deserialize JSON asynchronously: Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(value))'	DALRESTfulUtil	C:\Users\jrt\Source\Repos\ST3ComponetsAndConnections\DALRESTfulUtil\DalRESTFulUtilJSON.cs	194	Active
            //          */
            //        //result = await JsonConvert.DeserializeObjectAsync<T>(response.Content.ReadAsStringAsync().Result);
            //        //result = await JsonConvert.DeserializeObjectAsync<T>();
            //        result = await Task.Factory.StartNew(() => JsonSerializer.Deserialize<T>(response.Content.ReadAsStringAsync().Result));
            //    }
            //}
            return result;
        }
    }

}
