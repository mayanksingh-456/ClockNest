using ClockNest.Enum;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ClockNest.Helpers
{
    public class Security
    {
        private readonly IConfiguration _config;

        public Security(IConfiguration config)
        {
            _config = config;
        }


        public string HashPassword(string password, string salt)
        {
            // Combine password + salt to form the final input
            var passwordByteArray = Encoding.UTF8.GetBytes(password + salt);
            var _pKey = _config["pKey"];
            var keyByteArray = Convert.FromBase64String(_pKey);

            using (var hmac = new HMACSHA256(keyByteArray))
            {
                var hmacPasswordByteArray = hmac.ComputeHash(passwordByteArray);
                var hashedPassword = Convert.ToBase64String(hmacPasswordByteArray);
                return hashedPassword;
            }
        }
        public bool DoesPasswordMatchPolicy(PasswordPolicyType passwordPolicyType, string password, string userNameOrEmail)
        {

            var passwordMatchesPolicy = false;
            string regularExpression = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*(_|[^\w])).+$";

            switch (passwordPolicyType)
            {
                case PasswordPolicyType.Basic:
                    passwordMatchesPolicy = (password.Length > 7);
                    break;
                case PasswordPolicyType.Intermediate:
                    passwordMatchesPolicy = Regex.IsMatch(password, regularExpression);
                    break;
                case PasswordPolicyType.Advance:
                    passwordMatchesPolicy = Regex.IsMatch(password, regularExpression) & !password.ToLower().Contains(userNameOrEmail.ToLower());
                    break;

                default:
                    break;
            }

            return passwordMatchesPolicy;
        }
    }
}
