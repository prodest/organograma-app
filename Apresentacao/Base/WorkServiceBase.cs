using Newtonsoft.Json;
using OrganogramaApp.Apresentacao.Models;
using StackExchange.Profiling;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace OrganogramaApp.Apresentacao.Base
{
    public class WorkServiceBase
    {
        protected string urlOrganogramaApiBase;

        public WorkServiceBase(string urlOrganogramaApiBase)
        {
            this.urlOrganogramaApiBase = urlOrganogramaApiBase;
        }

        public static RetornoAjaxModel Get(string url, string accessToken, int i = 0)
        {
            using (MiniProfiler.Current.Step($"url{i}: {url}"))
            {
                using (var client = new HttpClient())
                {
                    //TODO: Está pulando a validação de certificado por causa do certificado inválido de DES e HMG
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                    
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

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
                            return Get(url, accessToken, i++);
                        }                            
                    }

                    retornoAjax.IsSuccessStatusCode = result.IsSuccessStatusCode;
                    retornoAjax.statusCode = result.StatusCode.ToString();
                    retornoAjax.content = result.Content.ReadAsStringAsync().Result;

                    return retornoAjax;
                }
            }
        }
        
        public static RetornoAjaxModel Post(object jsonString, string urlPost, string accessToken, int i = 0)
        {
            var Json = JsonConvert.SerializeObject(jsonString);
            var httpContent = new StringContent(Json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

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
                        return Post(jsonString, urlPost, accessToken, i++);
                    }                    
                }

                retornoAjax.IsSuccessStatusCode = result.IsSuccessStatusCode;
                retornoAjax.statusCode = result.StatusCode.ToString();
                retornoAjax.content = result.Content.ReadAsStringAsync().Result;

                return retornoAjax;
            }
        }

        public static RetornoAjaxModel Put(object jsonString, string urlPut, string accessToken, int i=0)
        {
            var Json = JsonConvert.SerializeObject(jsonString);
            var httpContent = new StringContent(Json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

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
                        return Put(jsonString, urlPut, accessToken, i++);
                    }
                    
                }

                retornoAjax.IsSuccessStatusCode = result.IsSuccessStatusCode;
                retornoAjax.statusCode = result.StatusCode.ToString();
                retornoAjax.content = result.Content.ReadAsStringAsync().Result;

                return retornoAjax;

            }
        }

        public static RetornoAjaxModel Delete(string url, string accessToken, int i = 0)
        {
            using (MiniProfiler.Current.Step($"url{i}: {url}"))
            {
                using (var client = new HttpClient())
                {
                    //TODO: Está pulando a validação de certificado por causa do certificado inválido de DES e HMG
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

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
                            return Delete(url, accessToken, i++);
                        }
                    }

                    retornoAjax.IsSuccessStatusCode = result.IsSuccessStatusCode;
                    retornoAjax.statusCode = result.StatusCode.ToString();
                    retornoAjax.content = result.Content.ReadAsStringAsync().Result;

                    return retornoAjax;
                }
            }
        }

        public RetornoAjaxModel GetMunicipios(string uf, string accessToken)
        {               
            var url = urlOrganogramaApiBase + "municipios?uf=" + uf;
            var retorno_ws = Get(url, accessToken);

            RetornoAjaxModel retorno = new RetornoAjaxModel()
            {
                IsSuccessStatusCode = retorno_ws.IsSuccessStatusCode,
                content = retorno_ws.content,
                statusCode = retorno_ws.statusCode
            };

            return retorno;
        }

        public RetornoAjaxModel GetUnidadesPorOrganizacao(string guidOrganizacao, string accessToken)
        {
            var url = urlOrganogramaApiBase + "unidades/organizacao/" + guidOrganizacao;
            var retorno_ws = Get(url, accessToken);

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
