using System;
using System.Threading;

namespace PracticalWork8
{
    class Program
    {
        static void Main(string[] args)
        {
            // Встановлюємо кодування для коректного виведення українських літер
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("==================================================");
            Console.WriteLine("  ПРАКТИЧНА РОБОТА №8: ВИКОРИСТАННЯ ПОТОКІВ В C# ");
            Console.WriteLine("==================================================\n");

            // 1. Створюємо та ініціалізуємо масив із 10 цілих чисел у проміжку (100..500)
            int[] array = new int[10];
            Random rand = new Random();
            
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(101, 500); // генерує числа в діапазоні від 101 до 499
            }

            // Виводимо згенерований масив на екран для наочності
            Console.WriteLine("Згенерований масив із 10 чисел:");
            Console.WriteLine(string.Join(", ", array));
            Console.WriteLine("\n--------------------------------------------------\n");

            // 2. Створюємо потік T0
            Thread threadT0 = new Thread(() =>
            {
                Console.WriteLine("[Потік T0] Початок виконання...");
                Console.WriteLine("[Потік T0] Числа, менші за 200, але більші за 100:");
                
                bool found = false;
                foreach (int num in array)
                {
                    if (num > 100 && num < 200)
                    {
                        Console.WriteLine($"  -> {num}");
                        found = true;
                    }
                }
                
                if (!found)
                {
                    Console.WriteLine("  (У масиві немає чисел у заданому діапазоні)");
                }
                Console.WriteLine("[Потік T0] Завершено.\n");
            });

            // 3. Створюємо потік T1
            Thread threadT1 = new Thread(() =>
            {
                Console.WriteLine("[Потік T1] Початок виконання...");
                Console.WriteLine("[Потік T1] Квадрати всіх елементів масиву:");
                
                foreach (int num in array)
                {
                    long square = (long)num * num;
                    Console.WriteLine($"  -> {num}^2 = {square}");
                }
                Console.WriteLine("[Потік T1] Завершено.\n");
            });

            // 4. Запускаємо обидва потоки
            threadT0.Start();
            threadT1.Start();

            // Очікуємо завершення обох потоків перед завершенням програми
            threadT0.Join();
            threadT1.Join();

            Console.WriteLine("==================================================");
            Console.WriteLine("Усі потоки завершили роботу. Натисніть клавішу...");
            Console.ReadKey();
        }
    }
}