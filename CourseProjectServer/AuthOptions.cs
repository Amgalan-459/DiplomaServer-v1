using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CourseProjectServer {
    public class AuthOptions {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient";
        const string KEY = "mysupersecret_secretkey!1234567890";
        public const int LIFETIME = 10;
        public static SymmetricSecurityKey GetSymmetricSecurityKey () {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }

    public class AuthResponse {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
