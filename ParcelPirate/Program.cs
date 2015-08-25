using System;
using System.Text;
using ParcelPirate;
using Newtonsoft.Json.Linq;
using ParcelPirate.Extensions;
using System.Collections.Generic;

namespace ParcelPirate {

    class Program {

        private static string THIS_FOLDER;
        private static int VERBOSITY = 1; //todo: honor. use the cyclops log level for verbosity
        private const string CONF_NAME = "parcelpirate.conf";

        public static void Main(string[] args) {

            THIS_FOLDER = GetThisFolder();
            var config = LoadConfig(THIS_FOLDER + CONF_NAME);

            var sc = new SlackCrawler(config);

            if(sc.IsSlackApiAvailable()) {

                //todo: split this out into a messenger
                if (args.Length > 0) {
                    Console.WriteLine("sending message " + args[0]);

                    //var message = string.Format("<@{0}> {1}", config.myUserId, args[0]);
                    var message = args[0];


                    Console.WriteLine(sc.PostMessageToChannel(message).ToString(Newtonsoft.Json.Formatting.Indented));
                }

                //Console.WriteLine(sc.GetChannels().PrintR());

                //Console.WriteLine("All Messages on Channel " + config.channel);
                //Console.WriteLine(sc.GetAllChannelMessages().PrintR());

                //Console.WriteLine("\n\n\n\n");

                Console.WriteLine("Messages Since..." + config.lastRunTime);

                var new_messages = sc.GetChannelMessagesSince(config.channel);//sort in chrono order.. and get last timestamp

                Console.WriteLine(new_messages.PrintR());

                if(new_messages.Count > 0) {

                    config.lastRunTime = new_messages[0].Value<string>("ts");//THIS IS IN REVERSE ORDER!!!!! 
                    System.IO.File.WriteAllText(THIS_FOLDER + "slack\\" + config.lastRunTime, new_messages.Serialize());

                }else{

                    config.lastRunTime = DateTime.UtcNow.ToSlackEpoch();
                }


                Console.WriteLine("\n\n\n\n");

                 //
                System.IO.File.WriteAllText(THIS_FOLDER + CONF_NAME, config.ToString());
                

            }else {

                Console.WriteLine("It seems that the Slack API is not available. nooooooOo! slain sascii art");
            }

            Console.WriteLine(config.lastRunTime + " done");
        }


        private static string GetThisFolder() {
        
            var path_bits = System.Reflection.Assembly.GetEntryAssembly().Location.Split('\\');

            string simple_path_i_have_to_build_because_you_suck_microsoft = "";

            for(int i=0; i<(path_bits.Length-1); ++i) {
                simple_path_i_have_to_build_because_you_suck_microsoft += path_bits[i]+'\\';}

            return simple_path_i_have_to_build_because_you_suck_microsoft;
        }

        private static ParcelPirateConfig LoadConfig(string fileLocation = "youfool.conf") {

            //todo: check if the file exists, if it does not, then create one
            //todo: encode the password or some other method. ask ross (if there is a plaintext password then erase it and write the back the encoded version as a different serialized property name)

            ParcelPirateConfig conf = new ParcelPirateConfig(@fileLocation);

            if(!conf.isValidConfiguration) {
               
            }

            return conf;
        }


    }
}