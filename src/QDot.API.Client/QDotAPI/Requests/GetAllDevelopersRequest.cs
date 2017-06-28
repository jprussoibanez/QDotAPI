using QDot.API.Client.BaseAPI.Request;
using System;
using System.Collections.Generic;
using System.Text;
using QDot.API.Client.BaseAPI;
using QDot.Infraestructure.Models;

namespace QDot.API.Client.QDotAPI.Requests
{
    public class GetAllDevelopersRequest : IAPIRequest<List<Developer>>
    {
        public HttpMethod GetHttpMethod()
        {
            return HttpMethod.GET;
        }

        public IDictionary<string, string> GetRequestHeaders()
        {
            return new Dictionary<string, string>();
        }

        public byte[] GetRequestStream()
        {
            return null;
        }

        public string GetUrl()
        {
            return "developer";
        }

        public IDictionary<string, string> GetUrlParameters()
        {
            return new Dictionary<string, string>();
        }

        public void Validate()
        {
            //No parameters to validate
        }
    }
}
