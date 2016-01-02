using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace WPDevelopmentLibs.Helpers
{
    public class StringHelper
    {
        protected static string GetMd5Hash(string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = new MD5().ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static string MD5(string _str, int _repeats)
        {
            string result = _str;
            for (int i = 0; i < _repeats; i++)
            {
                result = GetMd5Hash(result);
            }
            return result;
        }

        public static string MD5(string _str)
        {
            return MD5(_str, 1);
        }

        public static Dictionary<string, string> ParseQueryString(string uri)
        {
            Dictionary<string, string> output = new Dictionary<string, string>();

            if (uri != "")
            {
                string substring = uri.Substring(((uri.LastIndexOf('?') == -1) ? 0 : uri.LastIndexOf('?') + 1));

                string[] pairs = substring.Split('&');

                foreach (string piece in pairs)
                {
                    string[] pair = piece.Split('=');
                    output.Add(pair[0], pair[1]);
                }
            }

            return output;
        }
    }
}
