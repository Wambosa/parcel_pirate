using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;

namespace ParcelPirate {

    internal static class HttpService {

        public static string GetRawJson(Uri uri) {

            string result = "emtpy";
            var request = (HttpWebRequest)WebRequest.Create(uri);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK) {

                result = new StreamReader(response.GetResponseStream()).ReadToEnd();

            }else{

                result = "no response? status code :" + response.StatusCode;
            }

            return result;
        }

        public static string PostRawJson(Uri uri) {
            // the post to slack can be the same for now. later we may want to change it to a real post. no time

            string result = "emtpy";
            var request = (HttpWebRequest)WebRequest.Create(uri);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK){

                result = new StreamReader(response.GetResponseStream()).ReadToEnd();

            }else{

                result = "no response? status code :" + response.StatusCode;
            }

            return result;
        }
    }
}