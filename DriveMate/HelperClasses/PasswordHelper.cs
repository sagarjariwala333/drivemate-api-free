using DevOne.Security.Cryptography.BCrypt;
using DriveMate.Entities;
using DriveMate.HelperClasses.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DriveMate.HelperClasses
{
    public class PasswordHelper : IPasswordHelper
    {
        public string EncryptPassword(string password)
        {
            string salt = BCryptHelper.GenerateSalt();
            string hashedPassword = BCryptHelper.HashPassword(password,salt);
            return hashedPassword+"|"+salt;
        }

        public bool VerifyPassword(string password, string encryptedpassword)
        {
            string[] strarr = encryptedpassword.Split('|');
            string pass = BCryptHelper.HashPassword(password, strarr[1]);

            if(encryptedpassword == pass + "|" + strarr[1])
            {
                return true;
            }
            return false;
        }
    }
}
