namespace Boilerplate.Contracts.IServices.Services.EncryptionAndDecryption
{
    public interface IEncryptionAndDecryptionService
    {
        public string EncryptData(string plainText);
        public string DecryptData(string plainText);
    }
}
