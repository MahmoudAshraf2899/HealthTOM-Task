using AutoMapper;
using Boilerplate.Application.Services.EncryptionAndDecryption;
using Boilerplate.Contracts.DTOs;
using Boilerplate.Contracts.Interfaces.Custom;
using Boilerplate.Contracts.IServices.Services.EncryptionAndDecryption;
using Boilerplate.Core.Bases;
using Boilerplate.Core.IServices.Custom;
using Boilerplate.Shared.Consts;
using Boilerplate.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Cryptography;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Boilerplate.Application.Services.Agency
{
    public class EncryptionAndDecryptionService : BaseService<EncryptionAndDecryptionService>, IEncryptionAndDecryptionService
    {
        private readonly Aes _Aes;
        private const string _Key = "rN+5cvndgnIYh2Oaql83C7IPZ6oyfd3Rq61v7yS+bkk=";
        private const string _IV = "CKzuyZIwJyN98iejjcDr3Q==";
        private const string _splitter = "$@$";
        public EncryptionAndDecryptionService(IUnitOfWork unitOfWork, IMapper mapper, IHolderOfDTO holderOfDTO, ILogger<EncryptionAndDecryptionService> logger = null, ICulture culture = null, IHostEnvironment environment = null, IHttpContextAccessor HttpContextAccessor = null) : base(unitOfWork, mapper, holderOfDTO, logger, culture, environment, HttpContextAccessor)
        {
            _Aes = Aes.Create();
        }


        public string EncryptData(string plainText)
        {
            var randomID = Guid.NewGuid().ToString() + DateTime.Now.Ticks.ToString();
            var StartGuId = Guid.NewGuid().ToString();

            int len = new Random().Next(20, randomID.Length),
            randomStart = new Random().Next(0, randomID.Length - len),
            keyStart = new Random().Next(0, plainText.Length - 1);

            randomID = randomID.Substring(randomStart, len);
            plainText = plainText.Insert(keyStart, randomID);
            string fullKey = $"{StartGuId}{_splitter}{keyStart}{_splitter}{plainText}{_splitter}{randomID.Length}";
            //string fullKey = $"We are here to secure your data{_splitter}{keyStart}{_splitter}{plainText}{_splitter}{randomID.Length}";

            ConfigureAES();
            byte[] encryptedData = EncryptStringToBytes_Aes(fullKey, _Aes.Key, _Aes.IV);
            return Base36.ByteArrayToBase36String(encryptedData);
        }

        public string DecryptData(string encryptedText)
        {
            var encryptedKey = Base36.Base36StringToByteArray(encryptedText);
            string decryptedKey;

            ConfigureAES();
            decryptedKey = DecryptStringFromBytes_Aes(encryptedKey, _Aes.Key, _Aes.IV);

            var splitedKey = decryptedKey.Split(_splitter).ToList();

            int len = int.Parse(splitedKey[3]),
            start = int.Parse(splitedKey[1]);
            decryptedKey = splitedKey[2].Remove(start, len);
            return decryptedKey;
        }
        public Key DecryptKeyModel(string Data, string PrivateKey)
        {
            try
            {
                var DecryptedData = DecryptData(Data);
                var Bytes = JsonConvert.DeserializeObject<Byte[]>(DecryptedData);
                var KeyModelJson = DecryptDataByPrivateKey(Bytes, PrivateKey);
                Key originalKey = JsonConvert.DeserializeObject<Key>(KeyModelJson);

                if (originalKey == null)
                    return new Key()
                    {
                        Status = Res.InvalidLincence,
                        ExpirationDate = null,
                    };

                if (originalKey.ExpirationDate > DateTime.Now)
                    originalKey.Status = Res.ValidLincence;
                else
                    originalKey.Status = Res.InvalidLincence;

                return originalKey;
            }
            catch (Exception ex)
            {
                return new Key()
                {
                    Status = "Unrecognized",
                    ExpirationDate = null,
                };
            }

        }

        #region Helper Method
        private void ConfigureAES()
        {
            _Aes.KeySize = 256;
            _Aes.BlockSize = 128;
            _Aes.Padding = PaddingMode.Zeros;
            _Aes.Mode = CipherMode.CBC;
            _Aes.Key = Convert.FromBase64String(_Key);
            _Aes.Key = Convert.FromBase64String(_IV);
        }
        private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        private string DecryptDataByPrivateKey(Byte[] data, string Privatencrypted)
        {
            var JsonPrivateKey = DecryptData(Privatencrypted);
            var PrivateKey = DeSerializeRSAParameters(JsonPrivateKey);
            //var Bytes = data.ToByes();
            var decryptedMessage = AsymmetricEncryption.Decrypt(data, PrivateKey);
            return decryptedMessage;
        }

        private RSAParameters DeSerializeRSAParameters(string jsonString)
        {
            var RSAParameters = JsonSerializer.Deserialize<RSAParametersData>(jsonString);
            var RSAParametersObj = new RSAParameters()
            {
                D = RSAParameters.D,
                DQ = RSAParameters.DQ,
                DP = RSAParameters.DP,
                Exponent = RSAParameters.Exponent,
                InverseQ = RSAParameters.InverseQ,
                Modulus = RSAParameters.Modulus,
                P = RSAParameters.P,
                Q = RSAParameters.Q
            };
            return RSAParametersObj;
        }
        #endregion
    }
}
