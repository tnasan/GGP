using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace GGP
{
    public static class EncryptionHelper
    {
        static SHA512 sha512 = new SHA512Managed();
        static RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider();

        public static Tuple<string,string> GetHashPassword(string password)
        {
            byte[] salt = GetSalt();
            byte[] hashPassword = sha512.ComputeHash(salt.Union(ConvertStringToByteArray(password)).ToArray());

            return new Tuple<string,string>(String.Join("", hashPassword.Select(x=>x.ToString("X2"))), String.Join("", salt.Select(x=>x.ToString("X2"))));
        }

        public static string GetHashPassword(string password, string salt)
        {
            byte[] byteSalt = ConvertHexStringToByteArray(salt);
            byte[] hashPassword = sha512.ComputeHash(byteSalt.Union(ConvertStringToByteArray(password)).ToArray());

            return String.Join("", hashPassword.Select(x => x.ToString("X2")));
        }
        
        private static byte[] GetSalt()
        {
            byte[] salt = new byte[64];
            rngCrypto.GetBytes(salt);

            return salt;
        }

        private static byte[] ConvertStringToByteArray(string str)
        {
            return str.ToCharArray().Select(x => Convert.ToByte(x)).ToArray();
        }

        private static byte[] ConvertHexStringToByteArray(string hexString)
        {
            return Enumerable.Range(0, hexString.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
                .ToArray();
        }
    }
}