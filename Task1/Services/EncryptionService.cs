using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Task1.Services
{

    public class EncryptionService
    {
        private string _encodeUrl(byte[] param)
        {
            return Convert.ToBase64String(param);
        }
        private byte[] _decodeUrl(string param)
        {
            return Convert.FromBase64String(param);
        }


        public string Encrypt(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            using (Aes encryptionAlgorithm = Aes.Create())
            {

                encryptionAlgorithm.GenerateKey();
                encryptionAlgorithm.GenerateIV();

                ICryptoTransform encryptor = encryptionAlgorithm.CreateEncryptor(encryptionAlgorithm.Key, encryptionAlgorithm.IV);

                byte[] encryptedDataBytes = encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length);

                string encryptedData = _encodeUrl(encryptedDataBytes);
                string keyBase64 = _encodeUrl(encryptionAlgorithm.Key);
                string ivBase64 = _encodeUrl(encryptionAlgorithm.IV);

                return HttpUtility.UrlEncode($"{encryptedData}:{keyBase64}:{ivBase64}");
            }
        }

        public string Decrypt(string urlParam)
        {
            string encryptedDataUrl = HttpUtility.UrlDecode(urlParam).Replace(' ','+');
            string[] parts = encryptedDataUrl.Split(':');
                if (parts.Length != 3)
                {
                    throw new ArgumentException("Invalid encrypted data format.");
                }

                byte[] encryptedDataBytes = _decodeUrl(parts[0]);
                byte[] keyBytes = _decodeUrl(parts[1]);
                byte[] ivBytes = _decodeUrl(parts[2]);

                using (Aes decryptionAlgorithm = Aes.Create())
                {
                    decryptionAlgorithm.Key = keyBytes;
                    decryptionAlgorithm.IV = ivBytes;
                    ICryptoTransform decryptor = decryptionAlgorithm.CreateDecryptor(decryptionAlgorithm.Key, decryptionAlgorithm.IV);
                    byte[] decryptedDataBytes = decryptor.TransformFinalBlock(encryptedDataBytes, 0, encryptedDataBytes.Length);

                    return Encoding.UTF8.GetString(decryptedDataBytes);
                }
            
        }
    }

}