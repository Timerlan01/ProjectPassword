using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tima.DAL
{
    public class PasswordEntry
    {
        public string Website { get; set; } // Название веб-сайта
        public string Username { get; set; } // Имя пользователя
        public string EncryptedPassword { get; set; } // Зашифрованный пароль

        // Конструктор для создания записи пароля
        public PasswordEntry(string website, string username, string encryptedPassword)
        {
            Website = website;
            Username = username;
            EncryptedPassword = encryptedPassword;
        }
    }
}
