using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace LivrariaJulio.Mvc.Util
{
    public class Requisicao
    {
        private string api = "http://localhost:53564";

        public HttpWebResponse Get(string endPoint)
        {
            var request = (HttpWebRequest)WebRequest.Create(api + endPoint);
            request.Method = "GET";
            request.ContentType = "application/json;charset=UTF-8";
            request.Accept = "application/json";
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");

            return (HttpWebResponse)request.GetResponse();
        }

        public HttpWebResponse Post(string endPoint, byte[] data)
        {
            var request = (HttpWebRequest)WebRequest.Create(api + endPoint);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            request.ContentLength = data.Length;
            request.Accept = "application/json";
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            request.GetRequestStream().Write(data, 0, data.Length);

            return (HttpWebResponse)request.GetResponse();
        }

        public HttpWebResponse Put(string endPoint, byte[] data)
        {
            var request = (HttpWebRequest)WebRequest.Create(api + endPoint);
            request.Method = "PUT";
            request.ContentType = "application/json;charset=UTF-8";
            request.ContentLength = data.Length;
            request.Accept = "application/json";
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            request.GetRequestStream().Write(data, 0, data.Length);

            return (HttpWebResponse)request.GetResponse();
        }

        public HttpWebResponse Delete(string endPoint)
        {
            var request = (HttpWebRequest)WebRequest.Create(api + endPoint);
            request.Method = "DELETE";
            request.ContentType = "application/json;charset=UTF-8";
            request.Accept = "application/json";
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");

            return (HttpWebResponse)request.GetResponse();
        }

        public HttpWebResponse Delete2(string endPoint, byte[] data)
        {
            var request = (HttpWebRequest)WebRequest.Create(api + endPoint);
            request.Method = "DELETE";
            request.ContentType = "application/json;charset=UTF-8";
            request.ContentLength = data.Length;
            request.Accept = "application/json";
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            request.GetRequestStream().Write(data, 0, data.Length);

            return (HttpWebResponse)request.GetResponse();
        }
    }
}