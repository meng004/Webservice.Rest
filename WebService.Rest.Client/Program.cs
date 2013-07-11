using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WebService.Rest.Client
{
    class Program
    {

        #region 字段

        /// <summary>
        /// 主机地址
        /// </summary>
        const string _baseAddress = "http://localhost:1206/";
        /// <summary>
        /// 请求Uri
        /// </summary>
        const string _requestUri = "api/Provinces";
        #endregion
        

        #region 同步

        /// <summary>
        /// 同步
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //调用HttpClient
            var provinces = GetProvincesByHttpClient();
            //调用RestSharp
            //var provinces = GetProvincesByRestSharp();
            //数据类型转换，排序
            var data = JArray.Parse(provinces).OrderBy(t => t["ProSort"]);
            //显示结果
            foreach (var item in data)
            {
                Console.WriteLine("{0}\t{1}\t\t{2}",
                    item["ProvinceId"],
                    item["ProName"],
                    item["ProRemark"]);
            }
            Console.ReadKey();
        }

        /// <summary>
        /// HttpClient 同步调用
        /// </summary>
        /// <returns></returns>
        static string GetProvincesByHttpClient()
        {
            //创建HttpClient，指定主机地址
            var client = new HttpClient { BaseAddress = new Uri(_baseAddress) };
            //修改请求标头，声明使用Json格式进行数据传输
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            //使用Http Get方法，向请求地址发出请求
            var response = client.GetAsync(_requestUri);
            //取出响应结果
            var provinces = response.Result.Content.ReadAsStringAsync().Result;
            return provinces;
        }

        /// <summary>
        /// RestSharp 同步调用
        /// </summary>
        /// <returns></returns>
        static string GetProvincesByRestSharp()
        {
            //创建RestClient，指定主机地址
            var client = new RestClient(_baseAddress);
            //创建请求，指定请求地址、Http Method、数据传输格式
            var request = new RestRequest(_requestUri, Method.GET);
            //修改请求标头，声明使用Json格式进行数据传输
            request.AddHeader("Accept", "application/json");
            //执行请求
            var response = client.Execute(request);
            //返回结果
            return response.Content;
        }

        #endregion
        

        /*
        #region 异步

        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //调用HttpClient
            GetProvincesByHttpClientAsync();
            //调用RestSharp
            //GetProvincesByRestSharpAsync();
            Console.ReadKey();
        }

        /// <summary>
        /// HttpClient 异步
        /// </summary>
        /// <returns></returns>
        static async void GetProvincesByHttpClientAsync()
        {
            //创建HttpClient，指定主机地址
            var client = new HttpClient { BaseAddress = new Uri(_baseAddress) };
            //修改请求标头，声明使用Json格式进行数据传输
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            //使用Http Get方法，向请求地址发出请求
            var response = await client.GetAsync(_requestUri);
            //取出响应结果
            var content = await response.Content.ReadAsStringAsync();
            //返回结果
            var array = JArray.Parse(content).OrderBy(t => t["ProSort"]);
            //显示结果
            array.ToList().ForEach(item =>
            {
                Console.WriteLine("{0}\t{1}\t\t{2}",
                    item["ProvinceId"],
                    item["ProName"],
                    item["ProRemark"]);
            });
        }

        /// <summary>
        /// RestSharp 异步调用
        /// </summary>
        static void GetProvincesByRestSharpAsync()
        {
            //创建RestClient，指定主机地址
            var client = new RestClient(_baseAddress);
            //创建请求，指定请求地址、Http Method、数据传输格式
            var request = new RestRequest(_requestUri, Method.GET);
            //修改请求标头，声明使用Json格式进行数据传输
            request.AddHeader("Accept", "application/json");
            //异步执行
            client.ExecuteAsync(request, response =>
            {
                //返回结果
                var array = JArray.Parse(response.Content).OrderBy(t => t["ProSort"]);
                //显示结果
                array.ToList().ForEach(item =>
                {
                    Console.WriteLine("{0}\t{1}\t\t{2}",
                        item["ProvinceId"],
                        item["ProName"],
                        item["ProRemark"]);
                });
            });
        }
        #endregion
        */

    }
}
