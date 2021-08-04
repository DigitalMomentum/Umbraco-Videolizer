using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Videolizer.Core.Models;
using static Videolizer.Core.Enums;

namespace Videolizer.Core.Vimeo
{
    public class Auth : IAuth
    {

        private static ProviderAppDetails appDetails;

        public Auth(string clientId, string clientSecret)
        {
            appDetails = new ProviderAppDetails()
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            };
        }
        public Auth(ProviderAppDetails providerAppDetails)
        {
            appDetails = providerAppDetails;
        }

        public string GetProviderAuthRedirect(string redirectUrl, string[] scopes, string state)
        {
            return $"https://api.vimeo.com/oauth/authorize?response_type=code&client_id={appDetails.ClientId}&redirect_uri={redirectUrl}&state={state}&scope={string.Join(" ", scopes)}";
        }



        public void RedirectToProviderAuth(string redirectUrl, string[] scopes, string state)
        {
            HttpContext.Current.Response.Redirect(GetProviderAuthRedirect(redirectUrl, scopes, state));
        }


        public TokenSet GetAccessTokenFromAuthCode(string authCode, string redirectUrl)
        {
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

			ServicePointManager.ServerCertificateValidationCallback = delegate { return true; }; //Dont know if I need this?

			using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["code"] = authCode;
                values["grant_type"] = "authorization_code";
                values["redirect_uri"] = redirectUrl;

                client.Headers.Add("Authorization", GetAuthenticationHeader());

                client.Headers.Add("Accept", "application/vnd.vimeo.*+json;version=3.4");

                try
                {
                    var responsedata = client.UploadValues("https://api.vimeo.com/oauth/access_token", values);

                    var responseString = Encoding.Default.GetString(responsedata);
                    dynamic response = JsonConvert.DeserializeObject(responseString);

                    return new TokenSet()
                    {
                        AccessToken = response.access_token.ToString(),
                        RefreshToken = null,
                        Expires = DateTime.MaxValue
                    };

                }
                catch (WebException e)
                {
                    string responseFromServer = "";
                    if (e.Response != null)
                    {
                        using (WebResponse response = e.Response)
                        {
                            System.IO.Stream dataRs = response.GetResponseStream();
                            using (StreamReader reader = new StreamReader(dataRs))
                            {
                                responseFromServer += reader.ReadToEnd();
                            }
                        }
                    }

                    throw new Exception(responseFromServer, e);
                }
            }
        }

        public static string GetAuthenticationHeader()
        {

            return "basic " + System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{appDetails.ClientId}:{appDetails.ClientSecret}"));

        }

        public TokenSet RefreshAccessToken(TokenSet expiredTokenSet)
        {
            return expiredTokenSet;
        }

    }
}
