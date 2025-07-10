using Azure;
using Microsoft.AspNetCore.Http;

namespace MvcDnevnik.Cookies
{
    public class Cookie
    {
        private const String CookieKey = "Word";
        private const string delimiter = "-";


        //Response - Create edit
        //Request  - Get


        private IRequestCookieCollection RequestCookies;

        private IResponseCookies ResponseCookies;

        
        public Cookie(IRequestCookieCollection cookies)
        {
            RequestCookies = cookies;
        }

        public Cookie(IResponseCookies cookies)
        {
            ResponseCookies = cookies;
        }

        public void SetSomething_IDK(List<string> Words)
        {
            string cookieValue = String.Join(delimiter, Words);
            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(7)
            };

            Clear();

            ResponseCookies.Append(CookieKey, cookieValue, cookieOptions);

        }

        public string[] GetWords()
        {
            string cookieValue = RequestCookies[CookieKey] ?? String.Empty;
            if (string.IsNullOrEmpty(cookieValue))
            {
                return Array.Empty<string>();
            }

            return cookieValue.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);

        }

        public void Clear()
        {
            
            ResponseCookies.Delete(CookieKey);
        }


    }
}
