using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tima.DAL;
using Tima.BLL;


namespace ProjectPassword
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в менеджер паролей!");
            Console.Write("Установите свой мастер-пароль: ");
            string masterPassword = Console.ReadLine();
            PasswordManager manager = new PasswordManager(masterPassword);

            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Добавить ввод пароля");
                Console.WriteLine("2. Просмотреть все записи");
                Console.WriteLine("3. Просмотр расшифрованного пароля");
                Console.WriteLine("4. Поиск записей");
                Console.WriteLine("5. Выход");
                Console.Write("Введите свой выбор: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Вебсайт: ");
                        string website = Console.ReadLine();
                        Console.Write("Имя пользователя: ");
                        string username = Console.ReadLine();
                        Console.Write("Пароль: ");
                        string password = Console.ReadLine();
                        manager.AddPasswordEntry(website, username, password);
                        Console.WriteLine("Запись добавлена.");
                        break;
                    case "2":
                        var entries = manager.GetAllEntries();
                        DisplayEntries(entries);
                        break;
                    case "3":
                        Console.Write("Вебсайт: ");
                        string site = Console.ReadLine();
                        Console.Write("Имя пользователя: ");
                        string user = Console.ReadLine();
                        Console.Write("Введите мастер-пароль: ");
                        string inputMasterPassword = Console.ReadLine();
                        string decryptedPassword = manager.GetDecryptedPassword(site, user, inputMasterPassword);
                        Console.WriteLine($"Расшифрованный пароль: {decryptedPassword}");
                        break;
                    case "4":
                        Console.Write("Введите ключевое слово для поиска: ");
                        string keyword = Console.ReadLine();
                        var searchResults = manager.SearchEntries(keyword);
                        DisplayEntries(searchResults);
                        break;
                    case "5":
                        Console.WriteLine("До свидания!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неверный ответ. Пробовать снова.");
                        break;
                }
            }
        }
        // Метод для отображения записей паролей
        static void DisplayEntries(List<PasswordEntry> entries)
        {
            if (entries.Count == 0)
            {
                Console.WriteLine("No entries found.");
            }
            else
            {
                foreach (var entry in entries)
                {
                    Console.WriteLine($"Website: {entry.Website}, Username: {entry.Username}");
                }
            }
        }
    }
}
