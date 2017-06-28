using Newtonsoft.Json;
using QDot.API.Client.BaseAPI.Request;
using QDot.API.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QDot.API.Client.BaseAPI
{
    public abstract class APIClient : IAPIClient
    {
        protected abstract string ServiceURL { get; }

        public async Task<T> Execute<T>(IAPIRequest<T> request) where T : class
        {
            request.Validate();
            string url = _GenerateUrl(request.GetUrl(), request.GetUrlParameters());
            HttpMethod httpMethod = request.GetHttpMethod();
            IDictionary<string, string> headers = request.GetRequestHeaders();

            HttpWebRequest webRequest = HttpWebRequest.Create(url) as HttpWebRequest;
            
            //Add http headers
            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    webRequest.Headers[header.Key] = header.Value;
                }
            }
            
            //Execute the request and get response body.
            string body = null;
            try
            {
                HttpWebResponse response;
                switch (httpMethod)
                {
                    case HttpMethod.GET:
                        webRequest.Method = "GET";
                        response = (HttpWebResponse)(await webRequest.GetResponseAsync());
                        break;
                    case HttpMethod.POST:
                        webRequest.Method = "POST";
                        webRequest.ContentType = "application/json; charset=utf-8";
                        var forms = request.GetRequestStream();
                        using (Stream stream = await webRequest.GetRequestStreamAsync())
                        {
                            if (forms != null)
                            {
                                foreach (byte b in forms)
                                {
                                    stream.WriteByte(b);
                                }
                            }
                            response = (HttpWebResponse)(await webRequest.GetResponseAsync());
                        }
                        break;
                    case HttpMethod.PUT:
                        webRequest.Method = "PUT";
                        response = (HttpWebResponse)(await webRequest.GetResponseAsync());
                        break;
                    case HttpMethod.DELETE:
                        webRequest.Method = "DELETE";
                        response = (HttpWebResponse)(await webRequest.GetResponseAsync());
                        break;
                    default:
                        throw new QDotAPIClientException(string.Format("Invalid http method '{0}'", httpMethod.ToString()));
                }

                using (response)
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        body = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new QDotAPIClientException(string.Format("An error occured while {0} {1}.", httpMethod, url), exception);
            }

            //Parse response body
            if (string.IsNullOrEmpty(body))
            {
                return null;
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(body);
            }
            catch (Exception exception)
            {
                throw new QDotAPIClientException("An error occured while parsing json response to object.", exception);
            }
        }

        #region Private Methods

        private string _GenerateUrl(string url, IDictionary<string, string> urlParameters)
        {
            url = ServiceURL + url;
            if (urlParameters != null)
            {
                bool first = true;
                foreach (var urlParameter in urlParameters)
                {
                    url += string.Format("{0}{1}={2}", first ? "?" : "&", urlParameter.Key, WebUtility.UrlEncode(urlParameter.Value));
                    first = false;
                }
            }
            return url;
        }

        #endregion
    }
}
