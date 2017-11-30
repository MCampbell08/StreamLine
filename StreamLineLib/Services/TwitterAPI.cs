using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreamLineLib
{
    public class TwitterApi
    {
        private JObject friendsData = new JObject();
        private List<JArray> apis = new List<JArray>();
        private List<string> friendNames = new List<string>();
        public List<JArray> PullApis(string screen_name)
        {
            string userTimeline = "https://api.twitter.com/1.1/statuses/user_timeline.json";
            string friendsList = "https://api.twitter.com/1.1/friends/list.json";
            //"https://api.twitter.com/1.1/friends/list.json?cursor=-1&screen_name=twitterapi&skip_status=true&include_user_entities=false"

            PullJson(screen_name, userTimeline);
            PullJson(screen_name, friendsList);
            PullFriendsNames();
            GetFriendsTweets(userTimeline);

            return apis;
        }
        private void PullJson(string screen_name, string resource_url)
        {
            var count = "2";
            bool friendsList = false;
            if (resource_url == "https://api.twitter.com/1.1/friends/list.json")
            {
                count = "200";
                friendsList = true;
            }
            // oauth application keys
            var oauth_token = "2356937689-TuLqoDDCMo6M645qHMekAeWkdDeXvXUEhOiPvu7";
            var oauth_token_secret = "oOImZMN0GsoF8U08ZULFM0yc1eOnBjVhtQPy8zmDKrRFn";
            var oauth_consumer_key = "ENflng3CcA7dQxwpKClXi4v3S";
            var oauth_consumer_secret = "o31FzuuOpRO668KX84rDnlFYf9B4pANzKokGVhHeGh0zD3xv5b";

            // oauth implementation details
            var oauth_version = "1.0";
            var oauth_signature_method = "HMAC-SHA1";

            // unique request details
            var oauth_nonce = Convert.ToBase64String(
                new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            var timeSpan = DateTime.UtcNow
                - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            // message api details
            //var status = "Updating status via REST API if this works";
            // create oauth signature
            var baseFormat = "count={7}&oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
                            "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&screen_name={6}";

            var baseString = string.Format(baseFormat, oauth_consumer_key, oauth_nonce, oauth_signature_method, oauth_timestamp, oauth_token, oauth_version, Uri.EscapeDataString(screen_name), Uri.EscapeDataString(count));

            baseString = string.Concat("GET&", Uri.EscapeDataString(resource_url), "&", Uri.EscapeDataString(baseString));

            var compositeKey = string.Concat(Uri.EscapeDataString(oauth_consumer_secret),
                                    "&", Uri.EscapeDataString(oauth_token_secret));

            string oauth_signature;
            using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
            {
                oauth_signature = Convert.ToBase64String(
                    hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
            }

            // create the request header
            var headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", " +
                               "oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", " +
                               "oauth_token=\"{4}\", oauth_signature=\"{5}\", " +
                               "oauth_version=\"{6}\"";

            var authHeader = string.Format(headerFormat,
                                    Uri.EscapeDataString(oauth_nonce),
                                    Uri.EscapeDataString(oauth_signature_method),
                                    Uri.EscapeDataString(oauth_timestamp),
                                    Uri.EscapeDataString(oauth_consumer_key),
                                    Uri.EscapeDataString(oauth_token),
                                    Uri.EscapeDataString(oauth_signature),
                                    Uri.EscapeDataString(oauth_version)
                            );
            // make the request

            ServicePointManager.Expect100Continue = false;

            var postBody = "screen_name=" + Uri.EscapeDataString(screen_name);//
            var postBody2 = "count=" + Uri.EscapeDataString(count);
            resource_url += "?" + postBody + "&" + postBody2;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resource_url);
            request.Headers.Add("Authorization", authHeader);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";


            WebResponse response = request.GetResponse();
            string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();

            if (friendsList)
            {
                friendsData = ToObject(responseData);
            }
            else
            {
                apis.Add(ToArray(responseData));
            }
        }
        private JArray ToArray(string file)
        {
            JArray arr = JArray.Parse(file);

            return arr;
        }
        private JObject ToObject(string file)
        {
            JObject obj = JObject.Parse(file);

            return obj;
        }
        private void PullFriendsNames()
        {
            int counter = 0;
            Random random = new Random();
            int num = 0;

            while (counter < 2)
            {
                num = random.Next(0, friendsData["users"].Count());
                if (!friendNames.Contains(friendsData["users"][num]["screen_name"].ToString()))
                {
                    friendNames.Add(friendsData["users"][num]["screen_name"].ToString());
                    counter++;
                }
            }
        }
        private void GetFriendsTweets(string link)
        {
            for (int i = 0; i < friendNames.Count; i++)
            {
                PullJson(friendNames[i], link);
            }
        }
    }
}