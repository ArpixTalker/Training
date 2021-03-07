using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Components
{
    public static class Encryptor
    {
        private static SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
        private static MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        private static RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        private static SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
        private static SHA384CryptoServiceProvider sha384 = new SHA384CryptoServiceProvider();


        public static string HashString(string input) {

            byte[] bytes = Encoding.Unicode.GetBytes(input);
            var hashBytes = sha1.ComputeHash(bytes);
            return BuildHashFromBytes(hashBytes,"x2");
        }

        public static string HashString(string input, string cipher)
        {
            byte[] bytes = Encoding.BigEndianUnicode.GetBytes(input);

            switch (cipher){

                case "SHA1": return BuildHashFromBytes(sha1.ComputeHash(bytes),"X2");
                case "SHA256": return BuildHashFromBytes(sha256.ComputeHash(bytes), "X2");
                case "SHA384": return BuildHashFromBytes(sha384.ComputeHash(bytes), "X2");
                case "MD5": return BuildHashFromBytes(md5.ComputeHash(bytes), "X2");
                default: throw new Exception($"Unrecogniyet cipher: {cipher}");
            }
        }

        private static string BuildHashFromBytes(byte[] hashBytes, string format) {

            var sb = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.Append(b.ToString(format));
            }
            return sb.ToString();
        }
        
        /*
        public static string SymetricEncription(string input, string key, string cipher) {

            var bytes = Encoding.ASCII.GetBytes(input);
            switch (cipher) {
                case "AES":

                    using (var aes = new AesCryptoServiceProvider()) {

                        aes.Key = Encoding.ASCII.GetBytes(key);
                        aes.IV = new byte[16];
                        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key,aes.IV);

                        using (var memStream = new MemoryStream()) {

                            using (var crStream = new CryptoStream(memStream)) {

                                using (var sw = new StreamWriter()) {

                                }
                            }
                        }
                    }
                    break;
            }
        }*/
    }
}
