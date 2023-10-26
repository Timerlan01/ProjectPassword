using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tima.DAL;

namespace Tima.BLL
{
    public class PasswordManager
    {
        private List<PasswordEntry> passwordEntries = new List<PasswordEntry>(); // Список записей паролей
        private string masterPassword; // Мастер-пароль

        // Конструктор класса PasswordManager
        public PasswordManager(string masterPassword)
        {
            this.masterPassword = masterPassword;
        }

        // Метод для добавления записи пароля
        public void AddPasswordEntry(string website, string username, string password)
        {
            string encryptedPassword = EncryptPassword(password);
            passwordEntries.Add(new PasswordEntry(website, username, encryptedPassword));
        }

        // Метод для получения всех записей паролей 
        public List<PasswordEntry> GetAllEntries()
        {
            return passwordEntries
                .Select(entry => new PasswordEntry(entry.Website, entry.Username, "********"))
                .ToList();
        }

        // Метод для получения расшифрованного пароля
        public string GetDecryptedPassword(string website, string username, string masterPassword)
        {
            var entry = passwordEntries.FirstOrDefault(e => e.Website == website && e.Username == username);
            if (entry != null && masterPassword == this.masterPassword)
            {
                return DecryptPassword(entry.EncryptedPassword);
            }
            return "Access Denied";
        }

        // Метод для поиска записей паролей по ключевому слову
        public List<PasswordEntry> SearchEntries(string keyword)
        {
            return passwordEntries
                .Where(entry => entry.Website.Contains(keyword) || entry.Username.Contains(keyword))
                .Select(entry => new PasswordEntry(entry.Website, entry.Username, "********"))
                .ToList();
        }

        public string EncryptPassword(string password)
        {
            char[] passwordChars = password.ToCharArray();
            char[] encryptedChars = new char[passwordChars.Length];
            for (int i = 0; i < passwordChars.Length; i++)
            {
                encryptedChars[i] = (char)(passwordChars[i] ^ masterPassword[i % masterPassword.Length]);
            }
            return new string(encryptedChars);
        }

        public string DecryptPassword(string encryptedPassword)
        {
            char[] encryptedChars = encryptedPassword.ToCharArray();
            char[] decryptedChars = new char[encryptedChars.Length];
            for (int i = 0; i < encryptedChars.Length; i++)
            {
                decryptedChars[i] = (char)(encryptedChars[i] ^ masterPassword[i % masterPassword.Length]);
            }
            return new string(decryptedChars);
        }
    }
}
