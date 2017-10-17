using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DataAccess.Utilities
{
    public class Util
    {
        static string matchEmailPattern = @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" +
                                  @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." +
                                  @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" +
                                  @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z0-9]{2,10})";

        public static bool IsValid(string emailaddress)
        {
            try
            {
                new MailAddress(emailaddress);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string LimparEndEmail(string html)
        {
            var value = string.Empty;

            if (string.IsNullOrEmpty(html)) return value;

            var reg     = new Regex(matchEmailPattern).IsMatch(html);
            var reg2    = new Regex(matchEmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase).IsMatch(html);

            foreach (Match match in new Regex(matchEmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase).Matches(html))
            {
                if (value == string.Empty)
                    value = match.Value;
                else
                    value += "," + match.Value;
            }

            var emails = value.Split(',');
            if (emails.Count() > 0)
                return emails[0];

            return value;
        }
    }
}
