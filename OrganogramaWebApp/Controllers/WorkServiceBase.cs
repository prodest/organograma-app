using Newtonsoft.Json;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class WorkServiceBase
    {

        public static RetornoAjaxModel Get(string url, string token, int i = 0)
        {
            using (MiniProfiler.Current.Step($"url{i}: {url}"))
            {
                using (var client = new HttpClient())
                {
                    //TODO: Está pulando a validação de certificado por causa do certificado inválido de DES e HMG
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                    
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var result = client.GetAsync(url).Result;

                    RetornoAjaxModel retornoAjax = new RetornoAjaxModel();


                    if (result.IsSuccessStatusCode)
                    {
                        retornoAjax.result =  result.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        if ((result.StatusCode.Equals(HttpStatusCode.BadGateway) || result.StatusCode.Equals(HttpStatusCode.ServiceUnavailable) || result.StatusCode.Equals(HttpStatusCode.GatewayTimeout)) && i < 5)
                        {
                            return Get(url, token, i++);
                        }                            
                    }

                    retornoAjax.IsSuccessStatusCode = result.IsSuccessStatusCode;
                    retornoAjax.statusCode = result.StatusCode.ToString();
                    retornoAjax.content = result.Content.ReadAsStringAsync().Result;

                    return retornoAjax;
                }
            }
        }
        
        public static RetornoAjaxModel Post(object jsonString, string urlPost, string token, int i = 0)
        {
            var Json = JsonConvert.SerializeObject(jsonString);
            var httpContent = new StringContent(Json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Do the actual request and await the response                
                var result = client.PostAsync(urlPost, httpContent).Result;
                RetornoAjaxModel retornoAjax = new RetornoAjaxModel();

                if (result.IsSuccessStatusCode)
                {
                    retornoAjax.result = result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    if ((result.StatusCode.Equals(HttpStatusCode.BadGateway) || result.StatusCode.Equals(HttpStatusCode.ServiceUnavailable) || result.StatusCode.Equals(HttpStatusCode.GatewayTimeout)) && i < 5)
                    {
                        return Post(jsonString, urlPost, token, i++);
                    }                    
                }

                retornoAjax.IsSuccessStatusCode = result.IsSuccessStatusCode;
                retornoAjax.statusCode = result.StatusCode.ToString();
                retornoAjax.content = result.Content.ReadAsStringAsync().Result;

                return retornoAjax;
            }
        }

        public static RetornoAjaxModel Put(object jsonString, string urlPut, string token, int i=0)
        {
            var Json = JsonConvert.SerializeObject(jsonString);
            var httpContent = new StringContent(Json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Do the actual request and await the response
                var result  = client.PutAsync(urlPut, httpContent).Result;
                RetornoAjaxModel retornoAjax = new RetornoAjaxModel();

                if (result.IsSuccessStatusCode)
                {
                    retornoAjax.result = result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    if ((result.StatusCode.Equals(HttpStatusCode.BadGateway) || result.StatusCode.Equals(HttpStatusCode.ServiceUnavailable) || result.StatusCode.Equals(HttpStatusCode.GatewayTimeout)) && i < 5)
                    {
                        return Put(jsonString, urlPut, token, i++);
                    }
                    
                }

                retornoAjax.IsSuccessStatusCode = result.IsSuccessStatusCode;
                retornoAjax.statusCode = result.StatusCode.ToString();
                retornoAjax.content = result.Content.ReadAsStringAsync().Result;

                return retornoAjax;

            }
        }

        public static RetornoAjaxModel Delete(string url, string token, int i = 0)
        {
            using (MiniProfiler.Current.Step($"url{i}: {url}"))
            {
                using (var client = new HttpClient())
                {
                    //TODO: Está pulando a validação de certificado por causa do certificado inválido de DES e HMG
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var result = client.DeleteAsync(url).Result;
                    RetornoAjaxModel retornoAjax = new RetornoAjaxModel();

                    if (result.IsSuccessStatusCode)
                    {
                        retornoAjax.result = result.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        if ((result.StatusCode.Equals(HttpStatusCode.BadGateway) || result.StatusCode.Equals(HttpStatusCode.ServiceUnavailable) || result.StatusCode.Equals(HttpStatusCode.GatewayTimeout)) && i < 5)
                        {
                            return Delete(url, token, i++);
                        }
                    }

                    retornoAjax.IsSuccessStatusCode = result.IsSuccessStatusCode;
                    retornoAjax.statusCode = result.StatusCode.ToString();
                    retornoAjax.content = result.Content.ReadAsStringAsync().Result;

                    return retornoAjax;
                }
            }
        }

        public RetornoAjaxModel GetMunicipios(string uf, string token)
        {               
            var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "municipios?uf=" + uf;
            var retorno_ws = Get(url, token);

            RetornoAjaxModel retorno = new RetornoAjaxModel()
            {
                IsSuccessStatusCode = retorno_ws.IsSuccessStatusCode,
                content = retorno_ws.content,
                statusCode = retorno_ws.statusCode
            };

            return retorno;
        }
    }
}
