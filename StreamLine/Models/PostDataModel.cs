using StreamLine.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StreamLine.Models

{
    public interface IPostable { }
    public class PostUserTweets
    {
        public IList<PostTwitterDataModel> Tweets { get; set; }
    }
    public class PostUserInstaFeed
    {
        public IList<PostInstagramDataModel> InstaFeed { get; set; }
    }
    public class PostTwitterDataModel : IPostable
    {   
        
        public string Text { get; set; }
        
        public string ImageURL { get; set; }
        
        public string DateTime { get; set; }
        
        public int ReTweets { get; set; }
        
        public int Favorites { get; set; }
    }

    public class PostInstagramDataModel :IPostable
    {
        
        public string ImageURL { get; set; }
        
        public int Likes { get; set; }
        
        public string Caption { get; set; }
    }
    public class DataModels
    {
        public IList<object> DataModel { get; set; }
        public SpotifyPlaylist WebPlayer { get; set; }
    }
}