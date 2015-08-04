
namespace ParcelPirate.Extensions {

    public static class CustomExtensions {

        public static int ToEpoch(this System.DateTime thisDate) {
            return (int)(thisDate.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static string ToSlackEpoch(this System.DateTime thisDate) {
            var epoch = (int)(thisDate.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
            return string.Format("{0}.00000", epoch);
        }

        public static string PrintR(this System.Collections.Generic.List<Newtonsoft.Json.Linq.JToken> list) {

            var sb = new System.Text.StringBuilder();
            foreach(Newtonsoft.Json.Linq.JToken json in list) {

                sb.AppendLine(json.ToString(Newtonsoft.Json.Formatting.Indented));
                sb.AppendLine("\n==========\n");
            }

            return sb.ToString();
        }

        public static string Serialize(this System.Collections.Generic.List<Newtonsoft.Json.Linq.JToken> list, string listName) {

            var sb = new System.Text.StringBuilder();

            sb.AppendLine("{"+listName+": [");

            for (int i = 0; i < list.Count; ++i ){

                var json = list[i];

                sb.Append(json.ToString(Newtonsoft.Json.Formatting.Indented));
                if(i != list.Count-1){
                    sb.AppendLine(",");}
            }

            sb.AppendLine("]}");

            return sb.ToString();
        }

        public static int Sum(this int[] arr) {

            int total = 0;

            for (int i = 0; i < arr.Length; ++i ) {
                total += arr[i];}

            return total;
        }


        public static bool Contains(this string hayStack, string[] needles) {

            bool isFound = false;

            foreach (string needle in needles) {

                isFound = hayStack.ToLower().Contains(needle.ToLower());

                if (isFound){
                    break;}
            }

            return isFound;
        }

        public static string Contains(this string hayStack, string[] needles, bool returnMatch = true) {

            string firstWord    = string.Empty;
            bool isFound        = false;

            if (hayStack != null) {

                foreach (string needle in needles) {

                    isFound = hayStack.ToLower().Contains(needle.ToLower());

                    if (isFound){
                        firstWord = needle;
                        break;
                    }
                }
            }

            return firstWord;
        }

        public static string GetPrettyDate(this string uglyDate) {/*don't hate me ross*/

            var prettyDate = "";

            System.DateTime realDate = System.Convert.ToDateTime(uglyDate);
            //todo: lower min acceptable date for kicks. try 01/01/0002
            prettyDate = realDate > System.DateTime.Parse("01/01/1989") ? realDate.ToString("MMMM dd yyyy") : prettyDate;

            return prettyDate;
        }
    }
}
