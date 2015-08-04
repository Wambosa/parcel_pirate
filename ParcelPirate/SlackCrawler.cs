using System;
using System.Net;
using Newtonsoft.Json.Linq;
using ParcelPirate.Extensions;
using System.Collections.Generic;

namespace ParcelPirate {

    public class SlackCrawler {

        private string BASE_URL = "https://slack.com/api/";
        private ParcelPirateConfig CONFIG;
        private Dictionary<string, string> METHODS;


        public SlackCrawler(ParcelPirateConfig config) {

            CONFIG = config;

            METHODS = new Dictionary<string, string>() {
            
                {"GetChannels", string.Format("channels.list?token={0}", CONFIG.token)},
                {"GetAllChannelMessages", string.Format("channels.history?token={0}&channel=", CONFIG.token)},
                {"GetAllDefaultChannelMessages", string.Format("channels.history?token={0}&channel={1}", CONFIG.token, CONFIG.channel)},
                {"GetChannelMessagesSince", string.Format("channels.history?token={0}&channel={1}&oldest=", CONFIG.token, CONFIG.channel)},
                {"GetChannelMessagesSinceLastRun", string.Format("channels.history?token={0}&channel={1}&oldest={2}", CONFIG.token, CONFIG.channel, CONFIG.lastRunTime)},
                {"PostMessageToDefaultChannel", string.Format("chat.postMessage?token={0}&username=ChOPS&channel={1}&text=", CONFIG.token, CONFIG.channel)},
                //todo, try to make one for posting to another channel
            };
        }

        public bool IsSlackApiAvailable() {
            //hit the test method
            var test_api =  GetJsonObject("https://slack.com/api/api.test");

            return test_api.Value<bool>("ok");
        }

        private JToken GetJsonObject(string url) {

            var uri = new Uri(url);

            return JToken.Parse(HttpService.GetRawJson(uri));
        }

        public List<JToken> GetChannels() {
        
            /* https://slack.com/api/channels.list */

            var json = GetJsonObject(BASE_URL + METHODS["GetChannels"]);

            var channels = new List<JToken>();

            channels.AddRange(json["channels"]);

            return channels;
        }

        public List<JToken> GetAllChannelMessages(string channelId = "") {

            /* https://slack.com/api/channels.history */

            var method = channelId == "" ? "GetAllDefaultChannelMessages" : "GetAllChannelMessages";

            var json = GetJsonObject(BASE_URL + METHODS[method] + channelId);

            var messages = new List<JToken>();

            messages.AddRange(json["messages"]);

            return messages;
        }

        public List<JToken> GetChannelMessagesSince(string channelId, string epochTimeStamp = "") {

            /* https://slack.com/api/channels.history */

            var method = epochTimeStamp == "" ? "GetChannelMessagesSinceLastRun" : "GetChannelMessagesSince";

            var json = GetJsonObject(BASE_URL + METHODS[method] + epochTimeStamp);

            var messages = new List<JToken>();

            messages.AddRange(json["messages"]);

            return messages;
        }


        public JToken PostMessageToChannel(string text) {
            //todo: add in a direct im later
            /* https://slack.com/api/chat.postMessage */

            return JToken.Parse(HttpService.PostRawJson(new Uri(BASE_URL + METHODS["PostMessageToDefaultChannel"] + @text)));
        }

    }
}