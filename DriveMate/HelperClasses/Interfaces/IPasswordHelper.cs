namespace DriveMate.HelperClasses.Interfaces
{
    public interface IPasswordHelper
    {
        public string EncryptPassword(string password);
        public bool VerifyPassword(string password, string encryptedpassword);
    }
}
