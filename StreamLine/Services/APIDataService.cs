using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamLine.Models;
using StreamLineLib.Services;
using StreamLineLib; 
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace StreamLine.Services
{
    class APIDataService
    {
        public IList<object> GetAllInfo(string twitterScreenName, string instaScreenName)
        {
            IList<object> stuff = new List<object>();
            PostUserTweets tweets = new PostUserTweets();
            //PostUserInstaFeed instaFeed = new PostUserInstaFeed();
            tweets.Tweets = TwitterData(twitterScreenName);
            //instaFeed.InstaFeed = InstaData();

            stuff.Add(tweets);
            //stuff.Add(instaFeed);

            return stuff;
        }
        private IList<PostInstagramDataModel> InstaData()
        {
            List<PostInstagramDataModel> pics = new List<PostInstagramDataModel>();
            InstagramAPI api = new InstagramAPI();
            List<JObject> data = api.PullApi();
            PostInstagramDataModel instaModel = new PostInstagramDataModel();

            //string images = (string)o.SelectToken("data[0].images.low_resolution.url");
            //string caption = (string)o.SelectToken("data[0].caption.text");
            //string likes = (string)o.SelectToken("data[0].likes.count");

            foreach (var pic in data)
            {
                instaModel = new PostInstagramDataModel();
                instaModel.ImageURL = (string)pic.SelectToken("images.low_resolution.url");
                instaModel.Caption = (string)pic.SelectToken("caption.text");
                instaModel.Likes = Int32.Parse((string)pic.SelectToken("likes.count"));

                pics.Add(instaModel);
            }

            return pics;
        }
        private IList<PostTwitterDataModel> TwitterData(string screenName)
        {
            List<PostTwitterDataModel> tweets = new List<PostTwitterDataModel>();
            TwitterApi api = new TwitterApi();
            List<JArray> data = api.PullApis(screenName);
            PostTwitterDataModel tweetModel = new PostTwitterDataModel();
            //int countAmount = 0;

            //for (int i = 0; i < data.Count; i++)
            //{
            //    countAmount += data[i].Count;
            //}
            foreach (var user in data)
            {
                foreach (var tweet in user)
                {
                    tweetModel = new PostTwitterDataModel();
                    tweetModel.Text = tweet["text"].ToString();
                    tweetModel.ReTweets = (int)tweet["retweet_count"];
                    tweetModel.Favorites = (int)tweet["favorite_count"];
                    tweetModel.ImageURL = tweet["user"]["profile_image_url_https"].ToString();
                    tweetModel.DateTime = tweet["created_at"].ToString();

                    tweets.Add(tweetModel);
                }
            }

            List<PostTwitterDataModel> sortedTweets = tweets.OrderByDescending(t => DateTime.ParseExact(t.DateTime, "ddd MMM dd HH:mm:ss +0000 yyyy", null)).ToList();

            return sortedTweets;
        }
    }
}
