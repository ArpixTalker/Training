using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Components
{
    public class Authenticator
    {
        private MySqlConnector connector;

        public Authenticator()
        {

            this.connector = new MySqlConnector();
        }

        public bool ProcessAuthorization(string auth, out string method) {

            var data = auth.Split(' ');

            switch (data[0]) {
                case "":
                    method = AuthMethod.NoAuth;
                    return false;
                case "Basic":
                    method = AuthMethod.Basic;
                    return this.BasicAuth(data[1]);
                case "Bearer":
                    method = AuthMethod.BearerToken;
                    return this.BearerAuth(data[1]);
                default:
                    method = "Unsuported Method: " + data[0];
                    return false;
            }
        }

        private bool BearerAuth(string token) {

            if (token.Trim() == this.GetCurrentBearerToken()) {

                return true;
            }
            return false;
        }

        private bool BasicAuth(string basic64) {

                var bytes = Convert.FromBase64String(basic64);
                var creds = Encoding.UTF8.GetString(bytes).Split(':');
                return this.AuthenticateUser(creds[0],Encryptor.HashString(creds[1],"SHA256"), out string message, out int state);
        }

        private bool NoAuth() {

            return true;
        }

        private string GetCurrentBearerToken() {

            return "BEARER123456789";
        }

        private bool AuthenticateUser(string username, string pwHash, out string message, out int state)
        {
            var select = this.connector.GetUser(username, pwHash);
            try
            {
                if (select.Item1 == username && select.Item2 == pwHash)
                {
                    message = "Login Succesfull";
                    state = 0;
                    return true;
                }
                else
                {
                    state = 1;
                    message = "Invalid username or password";
                    return false;
                }
            }
            catch (Exception e)
            {

                state = 2;
                message = $"An error occured while Authenticating: {e.Message}";
                return false;
            }
        }
    }

    public static class AuthMethod {

        public static string Inherit { get; set; }
        public static string NoAuth { get; set; }
        public static string Basic { get; set; }
        public static string BearerToken { get; set; }
        public static string Oauth_1 { get; set; }
        public static string Oauth_2 { get; set; }
        public static string ApiKey { get; set; }
        public static string Digest { get; set; }
        public static string Hawk { get; set; }
        public static string AWS_Signature { get; set; }
        public static string NTLM { get; set; }
        public static string Akamai { get; set; }

        static AuthMethod(){

            Inherit = "Inherit";
            NoAuth = "NoAuth";
            Basic = "Basic";
            BearerToken = "BearerToken";
            Oauth_1 = "Oauth_1";
            Oauth_2 = "Oauth_2";
            ApiKey = "ApiKey";
            Digest = "Digest";
            Hawk = "Hawk";
            AWS_Signature = "AWS_Signature";
            NTLM = "NTLM";
            Akamai = "Akamai";
        }
    }
}
