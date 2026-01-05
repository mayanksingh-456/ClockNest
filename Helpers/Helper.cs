using System.Security.Cryptography;
using System.Text;

namespace ClockNest.Helpers
{
    public static class Helper
    {
        public static int GetYears(this TimeSpan timespan)
        {
            return (int)((double)timespan.Days / 365.2425);
        }
        public static int GetMonths(this TimeSpan timespan)
        {
            return (int)((double)timespan.Days / 30.436875);
        }

        //get date time now
        public static DateTime GetDateTimeNow()
        {
            TimeZoneInfo destinationTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, destinationTimeZone);
        }

        //hash password
        public static string HashPassword(string password, string salt)
        {
            var key = "5dd04f9b91034b66be84fb807ccf4df1";
            //var key = _configuration["pKey"];
            var passwordByteArray = Encoding.UTF8.GetBytes((password + salt));

            var keyByteArray = Convert.FromBase64String(key);

            var hmac = new HMACSHA256(keyByteArray);
            var hmacPasswordByteArray = hmac.ComputeHash(passwordByteArray);

            var hashedPassword = Convert.ToBase64String(hmacPasswordByteArray);

            return hashedPassword;
        }

        //public static string HashPassword(string password, string salt, string key)
        //{
        //    var passwordByteArray = Encoding.UTF8.GetBytes(password + salt);
        //    var keyByteArray = Encoding.UTF8.GetBytes(key);  // use UTF8 instead of Base64 if key is plain text

        //    using var hmac = new HMACSHA256(keyByteArray);
        //    var hashBytes = hmac.ComputeHash(passwordByteArray);

        //    return Convert.ToBase64String(hashBytes);
        //}

    }
}
