namespace KrakmApp.Core.Repositories.Base
{
    public interface IEncryptionService
    {
        string CreateSalt();

        string EncryptPassword(string password, string salt);
    }
}
