using System;
using System.Text.RegularExpressions;

namespace PracticalWork2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== ЗАВДАННЯ 1: Перевірка коректності логіна ===");
            TestLogin("admin");       
            TestLogin("123admin");    
            TestLogin("a1");          
            TestLogin("u");           
            TestLogin("user1234567"); 
            TestLogin("користувач1"); 

            Console.WriteLine("\n=== ЗАВДАННЯ 2: Фільтр виразів та їх форм ===");
            string text = "Це повідомлення містить спам, а також спаму не повинно бути в чаті, уникайте спаму та спам-повідомлень!";
            string filteredText = FilterSpam(text);

            Console.WriteLine("Оригінальний текст:");
            Console.WriteLine(text);
            Console.WriteLine("\nВідфільтрований текст:");
            Console.WriteLine(filteredText);

            Console.ReadKey();
        }

        // Завдання 1: Перевірка коректності логіна (від 2 до 10 символів, перша — літера)
        static void TestLogin(string login)
        {
            string pattern = @"^[a-zA-Zа-яА-ЯіІїЇєЄґҐ][a-zA-Zа-яА-ЯіІїЇєЄґҐ0-9]{1,9}$";

            bool isValid = Regex.IsMatch(login, pattern);
            Console.WriteLine($"Логін \"{login}\": {(isValid ? "Коректний" : "Некоректний")}");
        }

        // Завдання 2: Фільтр слів та їх різних форм (на прикладі слова 'спам')
        static string FilterSpam(string input)
        {
            string pattern = @"\bспам(-повідомлень|[уаиіем]*)\b";

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return regex.Replace(input, "[ЦЕНЗУРА]");
        }
    }
}