using MySql.Data.MySqlClient.Memcached;
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

namespace Videolizer.Core.YouTube
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
            return $"https://accounts.google.com/o/oauth2/v2/auth?client_id={appDetails.ClientId}&redirect_uri={redirectUrl}&state={state}&response_type=code&scope={string.Join(" ", scopes)}&access_type=offline";
        }



        public void RedirectToProviderAuth(string redirectUrl, string[] scopes, string state)
        {
            HttpContext.Current.Response.Redirect(GetProviderAuthRedirect(redirectUrl, scopes, state));
        }


        public TokenSet GetAccessTokenFromAuthCode(string authCode, string redirectUrl)
        {
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["code"] = authCode;
                values["client_id"] = appDetails.ClientId;
                values["client_secret"] = appDetails.ClientSecret;
                values["grant_type"] = "authorization_code";
                values["redirect_uri"] = redirectUrl;

                try
                {
                    var responsedata = client.UploadValues("https://oauth2.googleapis.com/token", values);

                    var responseString = Encoding.Default.GetString(responsedata);
                    dynamic response = JsonConvert.DeserializeObject(responseString);

                    return new TokenSet()
                    {
                        AccessToken = response.access_token.ToString(),
                        RefreshToken = response.refresh_token.ToString(),
                        Expires = DateTime.Now.AddSeconds(long.Parse(response.expires_in.ToString()) - 30)
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




        public TokenSet RefreshAccessToken(TokenSet tokenSet)
        {

            using (var client = new WebClient())
            {

                var values = new NameValueCollection();
                values["client_id"] = appDetails.ClientId;
                values["client_secret"] = appDetails.ClientSecret;
                values["grant_type"] = "refresh_token";
                values["refresh_token"] = tokenSet.RefreshToken;

                try
                {
                    var responsedata = client.UploadValues("https://oauth2.googleapis.com/token", values);

                    var responseString = Encoding.Default.GetString(responsedata);
                    dynamic response = JsonConvert.DeserializeObject(responseString);

                    return new TokenSet()
                    {
                        AccessToken = response.access_token.ToString(),
                        RefreshToken = tokenSet.RefreshToken, //This refresh token stays the same
                        Expires = DateTime.Now.AddSeconds(int.Parse(response.expires_in.ToString()) - 30)
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
    }
}
