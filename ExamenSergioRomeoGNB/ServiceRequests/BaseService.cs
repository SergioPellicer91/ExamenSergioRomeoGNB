using Newtonsoft.Json;
using NLog;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace ExamenSergioRomeoGNB.ServiceRequests
{
    public abstract class BaseService<T> where T : class
    {
        public IQueryable<T> GetAll(string url, Logger Log)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    return JsonConvert.DeserializeObject<IQueryable<T>>(reader.ReadToEnd());
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    Log.Error(errorText);
                }
                throw;
            }
        }
    }
}
