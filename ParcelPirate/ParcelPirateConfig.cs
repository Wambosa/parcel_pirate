using System;
using Newtonsoft.Json.Linq;
using ParcelPirate.Extensions;
using System.Collections.Generic;


namespace ParcelPirate {

    public class ParcelPirateConfig {

        public bool isValidConfiguration;

        public string token;
        public string myUserId;
        public string channel;
        public string lastRunTime;

        public ParcelPirateConfig(string fileLocation) {

            string raw = System.IO.File.ReadAllText(@fileLocation);

            if (raw.Length > 10) {//shallow validation

                JToken json_config = JToken.Parse(raw);

                token = (string)json_config["token"];
                myUserId = json_config.Value<string>("myUserId");
                channel = (string)json_config["channel"];

                var temp = (string)json_config["lastRunTime"] ?? "";

                lastRunTime = temp.Length > 0 ?  temp : DateTime.UtcNow.ToSlackEpoch();

                isValidConfiguration = true;

            }else{
                //todo: this will trigger a conf file recreation and questioning to repopulate
                isValidConfiguration = false;
            }
        }

        public override string ToString() {

            var cerealized = new JObject();
            cerealized["token"] = token;
            cerealized["myUserId"] = myUserId;
            cerealized["channel"] = channel;
            cerealized["lastRunTime"] = lastRunTime;
            return cerealized.ToString(Newtonsoft.Json.Formatting.Indented);
        }

        private string ValidateConfigurationFileAtLocation() {
            
            
            return "error if file length too short or if a property is found that is not supported";
        }
    }
}
