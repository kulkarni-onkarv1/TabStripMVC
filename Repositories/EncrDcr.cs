using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TabStripDemo.Repositories
{
    internal class EncryptorDecryptor
    {
        public static string EncryptAsync(string message)
        {
            var textToEncrypt = message;
            string toReturn = string.Empty;
            string publicKey = "87654321";
            string secretKey = "12345678";
            byte[] secretkeyByte;
            secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretKey);
            byte[] publickeybyte;
            publickeybyte = System.Text.Encoding.UTF8.GetBytes(publicKey);
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                ms = new MemoryStream();
                var res = des.CreateEncryptor(publickeybyte, secretkeyByte);
                cs = new CryptoStream(ms,res , CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                toReturn = Convert.ToBase64String(ms.ToArray());
            }
            return toReturn;
        }

        public static string DecryptAsync(string text)
        {
            if(text == null)
            {
                return null;
            }
            try
            {
                var textToDecrypt = text;
                string toReturn = "";
                string publicKey = "87654321";
                string secretKey = "12345678";
                byte[] privatekeyByte = { };
                privatekeyByte = System.Text.Encoding.UTF8.GetBytes(secretKey);
                byte[] publickeybyte = { };
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publicKey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
                inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    var res = des.CreateDecryptor(publickeybyte, privatekeyByte);
                    cs = new CryptoStream(ms, res, CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    toReturn = encoding.GetString(ms.ToArray());
                }
                return toReturn;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
