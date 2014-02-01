using System;
using System.Linq;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebService.Controllers
{
    [EnableCors(origins: "http://localhost:49700", headers: "*", methods: "*")]
    public class TestController : ApiController
    {
        public HttpResponseMessage Post()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("POST: Test message")
            };
        }

        public HttpResponseMessage Get()
        {            
            string pwd = string.Empty;
            string userID = string.Empty;

            var values = Request.GetQueryNameValuePairs().ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);
          
            values.TryGetValue("userID", out userID);
            values.TryGetValue("pwd", out pwd);

            var validUserId = WebConfigurationManager.AppSettings["UserID"];
            var validPWd = WebConfigurationManager.AppSettings["Pwd"];

            var httpResponseMsg = new HttpResponseMessage();

            if (validUserId.Equals(userID) && validPWd.Equals(pwd))
            {
                httpResponseMsg.Content = new StringContent("Valid user info");
            }
            else
            {
                httpResponseMsg.Content = new StringContent("ERROR: Invalid user info");
            }

            return httpResponseMsg;
        }

        public HttpResponseMessage Put()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("PUT: Test message")
            };
        }
    }
}