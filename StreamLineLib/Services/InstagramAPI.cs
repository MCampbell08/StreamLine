using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RapidAPISDK;

namespace StreamLineLib.Services
{

    public class InstagramAPI
    {
        private static RapidAPI RapidApi = new RapidAPI("NUStreamLine", "c910c74b-7dd8-4933-bd46-110927f5e2fc");

        List<Parameter> body = new List<Parameter>();
        JObject dataofinsta = new JObject();

        public JObject getCurrentUser()
        {
            body.Add(new DataParameter("accessToken", "3883859036.1677ed0.6fd4d9681b074f02ad55a0aed7dab08f"));

            try
            {
                Dictionary<string, object> response = RapidApi.Call("Instagram", "getCurrentUser", body.ToArray()).Result;
                object payload;
                if (response.TryGetValue("success", out payload))
                {

                    JObject o = JObject.Parse(payload.ToString());
                    string profilepicture = (string)o.SelectToken("data.profile_picture");
                    string bio = (string)o.SelectToken("data.bio");
                    string username = (string)o.SelectToken("data.username");
                    string fullname = (string)o.SelectToken("data.full_name");
                    // int follows = (int)o.SelectToken("counts.follows");

                    dataofinsta.Add(profilepicture);
                    dataofinsta.Add(bio);
                    dataofinsta.Add(username);
                    dataofinsta.Add(fullname);
                    // dataofinsta.Add(follows);



                    Console.WriteLine(dataofinsta[0]);
                    Console.WriteLine(dataofinsta[1]);
                    Console.WriteLine(dataofinsta[2]);
                    Console.WriteLine(dataofinsta[3]);
                    //Console.WriteLine(dataofinsta[4]);


                }
                else
                {

                }

            }
            catch (RapidAPIServerException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return dataofinsta;
        }

        public List<JObject> PullApi()
        {
            List<JObject> apis = new List<JObject>();

            apis.Add(getUsersRecentMedia());
            //apis.Add(getCurrentUser());

            return apis;
        }

        public JObject getUsersRecentMedia()
        {

            List<Parameter> body = new List<Parameter>();

            body.Add(new DataParameter("accessToken", "3883859036.1677ed0.6fd4d9681b074f02ad55a0aed7dab08f"));
            body.Add(new DataParameter("userId", "3883859036"));
            body.Add(new DataParameter("count", "5"));
            body.Add(new DataParameter("minId", ""));
            body.Add(new DataParameter("maxId", ""));

            try
            {
                Dictionary<string, object> response = RapidApi.Call("Instagram", "getUsersRecentMedia", body.ToArray()).Result;
                object payload;
                if (response.TryGetValue("success", out payload))
                {
                    JObject o = JObject.Parse(payload.ToString());
                    string images = (string)o.SelectToken("data[0].images.low_resolution.url");
                    string caption = (string)o.SelectToken("data[0].caption.text");
                    string likes = (string)o.SelectToken("data[0].likes.count");

                    dataofinsta = o;
                    //dataofinsta.Add(images);
                    //dataofinsta.Add(caption);
                    //dataofinsta.Add(likes);

                        

                    //Console.WriteLine(dataofinsta[0]);
                    //Console.WriteLine(dataofinsta[1]);
                    //Console.WriteLine(dataofinsta[2]);


                }
                else
                {

                }
            }
            catch (RapidAPIServerException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return dataofinsta;

        }








    }
}
