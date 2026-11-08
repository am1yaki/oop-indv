using System;
using System.IO;

namespace Lab1_PositiveCount
{
    class Program
    {
        static void Main(string[] args)
        {
            // Налаштування для коректного відображення української мови в консолі
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            int n = 20; // Розмір масиву за завданням
            int[] a = new int[n];
            bool isFilled = false;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("   ЛАБОРАТОРНА РОБОТА №1 - ВАРІАНТ: КІЛЬКІСТЬ ДОДАТНИХ");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Заповнити масив випадковими числами (від -50 до 50)");
                Console.WriteLine("2. Ввести елементи масиву вручну з клавіатури");
                Console.WriteLine("3. Зчитати елементи масиву з файлу (dat.txt)");
                Console.WriteLine("4. Визначити кількість додатних елементів");
                Console.WriteLine("5. Вихід");
                Console.WriteLine("--------------------------------------------------");
                Console.Write("Оберіть пункт меню (1-5): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Random rnd = new Random();
                        for (int i = 0; i < n; i++)
                        {
                            a[i] = rnd.Next(-50, 51); // випадкові числа від -50 до 50
                        }
                        isFilled = true;
                        Console.WriteLine(" Масив успішно заповнено випадковими числами!");
                        PrintArray(a);
                        break;

                    case "2":
                        Console.WriteLine($"Введіть {n} цілих чисел (кожної разу натискаючи Enter):");
                        for (int i = 0; i < n; i++)
                        {
                            Console.Write($"  Елемент [{i}]: ");
                            while (!int.TryParse(Console.ReadLine(), out a[i]))
                            {
                                Console.Write("  Помилка! Введіть ціле число: ");
                            }
                        }
                        isFilled = true;
                        Console.WriteLine("\n Масив успішно введено з клавіатури!");
                        PrintArray(a);
                        break;

                    case "3":
                        try
                        {
                            // Очікується файл "dat.txt" у папці з програмою, де записано 20 чисел (кожне з нового рядка)
                            if (File.Exists("dat.txt"))
                            {
                                string[] lines = File.ReadAllLines("dat.txt");
                                int countToRead = Math.Min(n, lines.Length);
                                for (int i = 0; i < countToRead; i++)
                                {
                                    a[i] = Convert.ToInt32(lines[i]);
                                }
                                isFilled = true;
                                Console.WriteLine(" Масив успішно зчитано з файлу 'dat.txt'!");
                                PrintArray(a);
                            }
                            else
                            {
                                Console.WriteLine(" Помилка: файл 'dat.txt' не знайдено в папці програми!");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($" Помилка при читанні файлу: {ex.Message}");
                        }
                        break;

                    case "4":
                        if (!isFilled)
                        {
                            Console.WriteLine(" Спочатку заповніть масив (пункти 1, 2 або 3)!");
                            break;
                        }

                        // Головна логіка завдання: підрахунок додатних елементів
                        int positiveCount = 0;
                        for (int i = 0; i < n; i++)
                        {
                            if (a[i] > 0)
                            {
                                positiveCount++;
                            }
                        }

                        Console.WriteLine(" Поточний масив:");
                        PrintArray(a);
                        Console.WriteLine($"\n Кількість додатних елементів у масиві: **{positiveCount}**");
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine(" Невірний вибір. Спробуйте ще раз.");
                        break;
                }

                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб продовжити...");
                Console.ReadKey();
            }
        }

        // Допоміжний метод для виведення масиву
        static void PrintArray(int[] arr)
        {
            Console.Write("Масив A = [ ");
            foreach (int item in arr)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine("]");
        }
    }
}